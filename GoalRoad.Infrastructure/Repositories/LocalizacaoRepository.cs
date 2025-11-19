using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;

namespace GoalRoad.Infrastructure.Data.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly ApplicationContext _context;

        public LocalizacaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<LocalizacaoEntity?> AtualizarAsync(LocalizacaoEntity entity)
        {
            _context.Enderecos.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<LocalizacaoEntity?> DeletarAsync(int id)
        {
            var item = await _context.Enderecos.FindAsync(id);
            if (item == null) return null;
            _context.Enderecos.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<PageResultModel<IEnumerable<LocalizacaoEntity>>> ObterTodasAsync(int Deslocamento = 0, int RegistrosRetornado = 3)
        {
            var total = await _context.Enderecos.CountAsync();
            var data = await _context.Enderecos
                .Skip(Deslocamento)
                .Take(RegistrosRetornado)
                .ToListAsync();
            return new PageResultModel<IEnumerable<LocalizacaoEntity>> { Data = data, Total = total };
        }

        public async Task<PageResultModel<IEnumerable<LocalizacaoEntity>>> ObterTodasAsync()
        {
            return await ObterTodasAsync(0, int.MaxValue);
        }

        public async Task<LocalizacaoEntity?> ObterPorIdAsync(int id)
        {
            return await _context.Enderecos.FindAsync(id);
        }

        public async Task<LocalizacaoEntity?> SalvarAsync(LocalizacaoEntity entity)
        {
            _context.Enderecos.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
