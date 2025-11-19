using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationContext _context;

        public CategoriaRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CategoriaEntity?> AtualizarAsync(CategoriaEntity entity)
        {
            _context.Categorias.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<CategoriaEntity?> DeletarAsync(int id)
        {
            var item = await _context.Categorias.FindAsync(id);
            if (item == null) return null;
            _context.Categorias.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<CategoriaEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Categorias.CountAsync();
            var data = await _context.Categorias
                .Include(c => c.Carreiras)
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();
            return new PageResultModel<IEnumerable<CategoriaEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<CategoriaEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<CategoriaEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Categorias
                .Include(c => c.Carreiras)
                .FirstOrDefaultAsync(c => c.IdCategoria == id);
        }

        public async Task<CategoriaEntity?> SalvarAsync(CategoriaEntity entity)
        {
            _context.Categorias.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
