using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using Microsoft.ML.Data;
using GoalRoad.Application.DTOs;
using GoalRoad.Application.Mappers;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Application.UseCases
{
    public class FeedUseCase : IFeedUseCase
    {
        private readonly IFeedRepository _repo;
        private readonly ApplicationContext _db;
        private readonly MLContext _ml;
        private readonly string _caminhoModelo;

        public FeedUseCase(IFeedRepository repo, ApplicationContext db)
        {
            _repo = repo;
            _db = db;
            _ml = new MLContext(seed: 0);
            _caminhoModelo = Path.Combine(Environment.CurrentDirectory, "Treinamento", "ModeloFeedRanking.zip");
        }

        public async Task<PageResultModel<IEnumerable<FeedDto>>> ObterTodasAsync(int offset = 0, int limit = 10)
        {
            var result = await _repo.ObterTodasAsync(offset, limit);
            return new PageResultModel<IEnumerable<FeedDto>> { Data = result.Data?.Select(f => f.ToDto()).ToList(), Total = result.Total };
        }

        public async Task<FeedDto?> ObterPorIdAsync(int id)
        {
            var f = await _repo.ObterPorIdAsync(id);
            return f?.ToDto();
        }

        public async Task<FeedDto?> SalvarAsync(FeedDto dto)
        {
            var entity = dto.ToEntity();
            var saved = await _repo.SalvarAsync(entity);
            return saved?.ToDto();
        }

        public async Task<FeedDto?> AtualizarAsync(FeedDto dto)
        {
            var entity = dto.ToEntity();
            var updated = await _repo.AtualizarAsync(entity);
            return updated?.ToDto();
        }

        public async Task<FeedDto?> DeletarAsync(int id)
        {
            var deleted = await _repo.DeletarAsync(id);
            return deleted?.ToDto();
        }

        public async Task<FeedDto> GerarESalvarFeedParaUsuarioAsync(int userId, int idCarreira, int top = 20)
        {
            var rankedItems = await GerarFeedParaRoadMapAsync(idCarreira, top);

            // Remove existing feed items for user (and feed if exists)
            var existingFeed = await _db.Feeds
                .Include(f => f.Items)
                .FirstOrDefaultAsync(f => f.IdUsuario == userId);

            if (existingFeed != null)
            {
                if (existingFeed.Items != null && existingFeed.Items.Any())
                {
                    _db.FeedItems.RemoveRange(existingFeed.Items);
                }
                _db.Feeds.Remove(existingFeed);
                await _db.SaveChangesAsync();
            }

            // Create new feed and attach items (as new rows)
            var feed = new FeedEntity { IdUsuario = userId };
            feed.Items = new List<FeedItemEntity>();

            foreach (var src in rankedItems)
            {
                var newItem = new FeedItemEntity
                {
                    IdUsuario = userId,
                    TipoItem = src.TipoItem,
                    FonteId = src.FonteId,
                    Titulo = src.Titulo,
                    Descricao = src.Descricao,
                    Url = src.Url,
                    IdTecnologia = src.IdTecnologia,
                    Relevancia = src.Relevancia,
                    DataCriacao = src.DataCriacao
                };

                feed.Items.Add(newItem);
            }

            _db.Feeds.Add(feed);
            await _db.SaveChangesAsync();

            return feed.ToDto();
        }

        public async Task TreinarModeloAsync()
        {
            // Load all feed items for training
            var items = await _db.FeedItems
                .Include(fi => fi.Tecnologia)
                .ToListAsync();

            // Get all unique technology IDs from roadmaps
            var allTechIds = await _db.RoadMapTecnologias
                .Select(rt => rt.IdTecnologia)
                .Distinct()
                .ToListAsync();

            TreinarModeloRanking(items, allTechIds);
        }

        private async Task<List<FeedItemEntity>> GerarFeedParaRoadMapAsync(int idCarreira, int top = 20)
        {
            // Get technologies for the roadmap
            var techIds = await _db.RoadMapTecnologias
                .Where(rt => rt.IdRoadMap == idCarreira)
                .Select(rt => rt.IdTecnologia)
                .ToListAsync();

            if (techIds == null || techIds.Count == 0)
            {
                return new List<FeedItemEntity>();
            }

            // Load candidate feed items (all or recent subset)
            var items = await _db.FeedItems
                .Include(fi => fi.Tecnologia)
                .OrderByDescending(fi => fi.DataCriacao)
                .Take(1000)
                .ToListAsync();

            // Load or train model
            ITransformer model;
            if (File.Exists(_caminhoModelo))
            {
                using (var stream = new FileStream(_caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    model = _ml.Model.Load(stream, out var modeloSchema);
                }
            }
            else
            {
                // If model doesn't exist, train a new one
                model = TreinarModeloRanking(items, techIds);
            }

            // Score items
            var scored = items.Select(i => new
            {
                Item = i,
                Score = PreverScore(model, i, techIds)
            })
            .OrderByDescending(x => x.Score)
            .Take(top)
            .Select(x => x.Item)
            .ToList();

            return scored;
        }

        // Input model for ML.NET
        private class InputModel
        {
            public float HasTechnologyMatch { get; set; }
            public float TitleLength { get; set; }
            public float DescriptionLength { get; set; }
        }

        private class OutputModel
        {
            [ColumnName("Score")]
            public float PontuacaoRelevancia { get; set; }
        }

        private ITransformer TreinarModeloRanking(IEnumerable<FeedItemEntity> items, IEnumerable<int> roadmapTecnologiaIds)
        {
            var data = items.Select(i => new InputModel
            {
                HasTechnologyMatch = i.IdTecnologia.HasValue && roadmapTecnologiaIds.Contains(i.IdTecnologia.Value) ? 1f : 0f,
                TitleLength = i.Titulo?.Length ?? 0,
                DescriptionLength = i.Descricao?.Length ?? 0
            }).ToList();

            if (!data.Any())
            {
                // Return a dummy model if no data
                var emptyData = new List<InputModel> { new InputModel { HasTechnologyMatch = 0f, TitleLength = 0f, DescriptionLength = 0f } };
                var ds = _ml.Data.LoadFromEnumerable(emptyData);
                var dummyPipeline = _ml.Transforms.Concatenate("Features", nameof(InputModel.HasTechnologyMatch), nameof(InputModel.TitleLength), nameof(InputModel.DescriptionLength))
                    .Append(_ml.Transforms.Conversion.MapValueToKey("Label", nameof(InputModel.HasTechnologyMatch)))
                    .Append(_ml.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "Label", featureColumnName: "Features"));
                return dummyPipeline.Fit(ds);
            }

            var dadosTreinados = _ml.Data.LoadFromEnumerable(data);

            // Create label from HasTechnologyMatch (1 = relevant, 0 = not relevant)
            var pipeline = _ml.Transforms
                .CopyColumns(outputColumnName: "Label", inputColumnName: nameof(InputModel.HasTechnologyMatch))
                .Append(_ml.Transforms.Concatenate("Features", nameof(InputModel.HasTechnologyMatch), nameof(InputModel.TitleLength), nameof(InputModel.DescriptionLength)))
                .Append(_ml.Regression.Trainers.FastTree());

            var modelo = pipeline.Fit(dadosTreinados);

            // Ensure directory exists
            var directory = Path.GetDirectoryName(_caminhoModelo);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Save model
            _ml.Model.Save(modelo, dadosTreinados.Schema, _caminhoModelo);

            return modelo;
        }

        private float PreverScore(ITransformer model, FeedItemEntity item, IEnumerable<int> roadmapTecnologiaIds)
        {
            var engine = _ml.Model.CreatePredictionEngine<InputModel, OutputModel>(model);
            var input = new InputModel
            {
                HasTechnologyMatch = item.IdTecnologia.HasValue && roadmapTecnologiaIds.Contains(item.IdTecnologia.Value) ? 1f : 0f,
                TitleLength = item.Titulo?.Length ?? 0,
                DescriptionLength = item.Descricao?.Length ?? 0
            };

            var output = engine.Predict(input);
            return output.PontuacaoRelevancia;
        }
    }
}


