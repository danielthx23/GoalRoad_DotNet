using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UsuarioEntity?> AtualizarAsync(UsuarioEntity entity)
        {
            _context.Usuarios.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UsuarioEntity?> DeletarAsync(int id)
        {
            var item = await _context.Usuarios.FindAsync(id);
            if (item == null) return null;
            _context.Usuarios.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Usuarios.CountAsync();
            var data = await _context.Usuarios.Skip(Deslocamento).Take(RegistrosRetornado).ToListAsync();
            return new PageResultModel<IEnumerable<UsuarioEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<UsuarioEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<UsuarioEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Localizacao).FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task<UsuarioEntity?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.EmailUsuario == email);
        }

        public async Task<UsuarioEntity?> SalvarAsync(UsuarioEntity entity)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
