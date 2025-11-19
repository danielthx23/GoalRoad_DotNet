using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class RoadMapTecnologiaRepository : IRoadMapTecnologiaRepository
    {
        private readonly ApplicationContext _context;

        public RoadMapTecnologiaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RoadMapTecnologiaEntity?> AtualizarAsync(RoadMapTecnologiaEntity entity)
        {
            _context.RoadMapTecnologias.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RoadMapTecnologiaEntity?> DeletarAsync(int id1, int id2)
        {
            var item = await _context.RoadMapTecnologias.FindAsync(id1, id2);
            if (item == null) return null;
            _context.RoadMapTecnologias.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<RoadMapTecnologiaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.RoadMapTecnologias.CountAsync();
            var data = await _context.RoadMapTecnologias
                .Include(rt => rt.RoadMap)
                .Include(rt => rt.Tecnologia)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();
            return new PageResultModel<IEnumerable<RoadMapTecnologiaEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<RoadMapTecnologiaEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<RoadMapTecnologiaEntity?> ObterPorIdAsync(int id1, int id2)
        {
            return await _context.RoadMapTecnologias
                .Include(rt => rt.RoadMap)
                .Include(rt => rt.Tecnologia)
                .FirstOrDefaultAsync(rt => rt.IdRoadMap == id1 && rt.IdTecnologia == id2);
        }

        public async Task<RoadMapTecnologiaEntity?> SalvarAsync(RoadMapTecnologiaEntity entity)
        {
            _context.RoadMapTecnologias.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
