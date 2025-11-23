using Swashbuckle.AspNetCore.Filters;
using GoalRoad.Application.DTOs;

namespace GoalRoad.Doc.Samples
{
    public class UsuarioResponseListSample : IExamplesProvider<IEnumerable<UsuarioDto>>
    {
        public IEnumerable<UsuarioDto> GetExamples()
        {
            return new List<UsuarioDto>
            {
                new UsuarioDto
                {
                    IdUsuario = 1,
                    NomeUsuario = "João Silva",
                    SenhaUsuario = "P@ssw0rd!",
                    EmailUsuario = "joao.silva@example.com",
                    CriadoEmUsuario = DateTime.UtcNow
                },
                new UsuarioDto
                {
                    IdUsuario = 2,
                    NomeUsuario = "Maria Santos",
                    SenhaUsuario = "P@ssw0rd!",
                    EmailUsuario = "maria.santos@example.com",
                    CriadoEmUsuario = DateTime.UtcNow
                }
            };
        }
    }

    public class UsuarioResponseSample : IExamplesProvider<UsuarioDto>
    {
        public UsuarioDto GetExamples()
        {
            return new UsuarioDto
            {
                IdUsuario = 1,
                NomeUsuario = "João Silva",
                SenhaUsuario = "P@ssw0rd!",
                EmailUsuario = "joao.silva@example.com",
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
                Localizacao = new LocalizacaoDto
                {
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
