using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class TecnologiaRepository : ITecnologiaRepository
    {
        private readonly ApplicationContext _context;

        public TecnologiaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<TecnologiaEntity?> AtualizarAsync(TecnologiaEntity entity)
        {
            _context.Tecnologias.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TecnologiaEntity?> DeletarAsync(int id)
        {
            var item = await _context.Tecnologias.FindAsync(id);
            if (item == null) return null;
            _context.Tecnologias.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<TecnologiaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Tecnologias.CountAsync();
            var data = await _context.Tecnologias.Skip(Deslocamento).Take(RegistrosRetornado).ToListAsync();
            return new PageResultModel<IEnumerable<TecnologiaEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<TecnologiaEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<TecnologiaEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Tecnologias.FirstOrDefaultAsync(t => t.IdTecnologia == id);
        }

        public async Task<TecnologiaEntity?> SalvarAsync(TecnologiaEntity entity)
        {
            _context.Tecnologias.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
