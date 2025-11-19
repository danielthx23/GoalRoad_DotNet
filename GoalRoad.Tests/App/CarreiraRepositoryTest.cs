using Microsoft.EntityFrameworkCore;
using GoalRoad.Domain.Entities;
using GoalRoad.Domain.Interfaces;
using GoalRoad.Domain.Models;
using GoalRoad.Infrastructure.Data.AppData;
using GoalRoad.Infrastructure.Data.Repositories;
using Xunit;

namespace GoalRoad.Tests.App
{
    public class CarreiraRepositoryTest : IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly ICarreiraRepository _repository;

        public CarreiraRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationContext(options);
            _repository = new CarreiraRepository(_context);

            // Seed data
            _context.Carreiras.Add(new CarreiraEntity
            {
                IdCarreira = 1,
                TituloCarreira = "Desenvolvedor Full Stack",
                DescricaoCarreira = "Desenvolvimento de aplicações web completas"
            });
            _context.SaveChanges();
        }

        [Fact(DisplayName = "ObterPorId - Retorna carreira existente")]
        [Trait("Repository", "Carreira")]
        public async Task ObterPorId_RetornaCarreiraExistente()
        {
            // Act
            var result = await _repository.ObterPorIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.IdCarreira);
            Assert.Equal("Desenvolvedor Full Stack", result.TituloCarreira);
        }

        [Fact(DisplayName = "ObterPorId - Retorna null para carreira inexistente")]
        [Trait("Repository", "Carreira")]
        public async Task ObterPorId_RetornaNullParaInexistente()
        {
            // Act
            var result = await _repository.ObterPorIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "ObterTodasAsync - Retorna lista paginada")]
        [Trait("Repository", "Carreira")]
        public async Task ObterTodasAsync_RetornaListaPaginada()
        {
            // Act
            var result = await _repository.ObterTodasAsync(0, 10);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.True(result.Total > 0);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

