using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;
using GoalRoad.Domain.Entities;

namespace GoalRoad.Doc.Samples
{
    public class UsuarioResponseListSample : IExamplesProvider<IEnumerable<UsuarioEntity>>
    {
        public IEnumerable<UsuarioEntity> GetExamples()
        {
            return new List<UsuarioEntity>
            {
                new UsuarioEntity
                {
                    IdUsuario = 1,
                    NomeUsuario = "João Silva",
                    SenhaUsuario = "P@ssw0rd!",
                    EmailUsuario = "joao.silva@example.com",
                    TokenUsuario = string.Empty,
                    CriadoEmUsuario = DateTime.UtcNow
                },
                new UsuarioEntity
                {
                    IdUsuario = 2,
                    NomeUsuario = "Maria Santos",
                    SenhaUsuario = "P@ssw0rd!",
                    EmailUsuario = "maria.santos@example.com",
                    TokenUsuario = string.Empty,
                    CriadoEmUsuario = DateTime.UtcNow
                }
            };
        }
    }

    public class UsuarioResponseSample : IExamplesProvider<UsuarioEntity>
    {
        public UsuarioEntity GetExamples()
        {
            return new UsuarioEntity
            {
                IdUsuario = 1,
                NomeUsuario = "João Silva",
                SenhaUsuario = "P@ssw0rd!",
                EmailUsuario = "joao.silva@example.com",
                TokenUsuario = string.Empty,
                CriadoEmUsuario = DateTime.UtcNow
            };
        }
    }

    public class UsuarioRequestSample : IExamplesProvider<UsuarioDto>
    {  
        public UsuarioDto GetExamples()
        {
            return new UsuarioDto
            {
                NomeUsuario = "João Silva",
                SenhaUsuario = "P@ssw0rd!",
                EmailUsuario = "joao.silva@example.com",
                Localizacao = new LocalizacaoDto()
                {
                    IdEndereco = 1,
                    Logradouro = "Rua Exemplo",
                    Numero = "123",
                    Complemento = "Apto 45",
                    Bairro = "Centro",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    CEP = "01000-000",
                    Referencia = "Próximo ao marco"
                }
            };
        }
    }
}
