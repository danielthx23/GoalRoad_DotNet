using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class FeedItemRepository : IFeedItemRepository
    {
        private readonly ApplicationContext _context;

        public FeedItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<FeedItemEntity?> AtualizarAsync(FeedItemEntity entity)
        {
            _context.FeedItems.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<FeedItemEntity?> DeletarAsync(int id)
        {
            var item = await _context.FeedItems.FindAsync(id);
            if (item == null) return null;
            _context.FeedItems.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<FeedItemEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.FeedItems.CountAsync();
            var data = await _context.FeedItems
                .Include(fi => fi.Tecnologia)
                .Include(fi => fi.Feed)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();
            return new PageResultModel<IEnumerable<FeedItemEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<FeedItemEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<FeedItemEntity?> ObterPorIdAsync(int id)
        {
            return await _context.FeedItems
                .Include(fi => fi.Tecnologia)
                .Include(fi => fi.Feed)
                .FirstOrDefaultAsync(fi => fi.Id == id);
        }

        public async Task<FeedItemEntity?> SalvarAsync(FeedItemEntity entity)
        {
            _context.FeedItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
