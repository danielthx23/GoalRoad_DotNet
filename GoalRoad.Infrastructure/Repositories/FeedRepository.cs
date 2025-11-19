using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class FeedRepository : IFeedRepository
    {
        private readonly ApplicationContext _context;

        public FeedRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FeedEntity?> AtualizarAsync(FeedEntity entity)
        {
            _context.Feeds.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FeedEntity?> DeletarAsync(int id)
        {
            var item = await _context.Feeds.FindAsync(id);
            if (item == null) return null;
            _context.Feeds.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<FeedEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Feeds.CountAsync();
            var data = await _context.Feeds
                .Include(f => f.Items)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();
            return new PageResultModel<IEnumerable<FeedEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<FeedEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<FeedEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Feeds
                .Include(f => f.Items)
                    .ThenInclude(i => i.Tecnologia)
                .FirstOrDefaultAsync(f => f.IdUsuario == id);
        }

        public async Task<FeedEntity?> SalvarAsync(FeedEntity entity)
        {
            _context.Feeds.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
