using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class RoadMapRepository : IRoadMapRepository
    {
        private readonly ApplicationContext _context;

        public RoadMapRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<RoadMapEntity?> AtualizarAsync(RoadMapEntity entity)
        {
            _context.RoadMaps.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RoadMapEntity?> DeletarAsync(int id)
        {
            var item = await _context.RoadMaps.FindAsync(id);
            if (item == null) return null;
            _context.RoadMaps.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<RoadMapEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.RoadMaps.CountAsync();
            var data = await _context.RoadMaps.Skip(Deslocamento).Take(RegistrosRetornado).ToListAsync();
            return new PageResultModel<IEnumerable<RoadMapEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<RoadMapEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<RoadMapEntity?> ObterPorIdAsync(int id)
        {
            return await _context.RoadMaps.Include(r => r.Tecnologias).ThenInclude(rt => rt.Tecnologia).FirstOrDefaultAsync(r => r.IdCarreira == id);
        }

        public async Task<RoadMapEntity?> SalvarAsync(RoadMapEntity entity)
        {
            _context.RoadMaps.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
