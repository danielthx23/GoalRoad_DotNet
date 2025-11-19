using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class CarreiraRepository : ICarreiraRepository
    {
        private readonly ApplicationContext _context;

        public CarreiraRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CarreiraEntity?> AtualizarAsync(CarreiraEntity entity)
        {
            _context.Carreiras.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CarreiraEntity?> DeletarAsync(int id)
        {
            var item = await _context.Carreiras.FindAsync(id);
            if (item == null) return null;
            _context.Carreiras.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<CarreiraEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Carreiras.CountAsync();
            var data = await _context.Carreiras.Skip(Deslocamento).Take(RegistrosRetornado).ToListAsync();
            return new PageResultModel<IEnumerable<CarreiraEntity>> { Data = data, Total = total };
        }

        // parameterless overload returning all
        public async Task<PageResultModel<IEnumerable<CarreiraEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<CarreiraEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Carreiras.Include(c => c.RoadMap).ThenInclude(r => r.Tecnologias).FirstOrDefaultAsync(c => c.IdCarreira == id);
        }

        public async Task<CarreiraEntity?> SalvarAsync(CarreiraEntity entity)
        {
            _context.Carreiras.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
