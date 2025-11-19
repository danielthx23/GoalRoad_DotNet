namespace GoalRoad.Application.DTOs;

public class UsuarioDto
{
    public int IdUsuario { get; set; }
    public required string NomeUsuario { get; set; } = string.Empty;
    public required string SenhaUsuario { get; set; } = string.Empty;
    public required string EmailUsuario { get; set; } = string.Empty;

    public DateTime CriadoEmUsuario { get; set; } = DateTime.UtcNow;

    public LocalizacaoDto? Localizacao { get; set; } = new LocalizacaoDto();
}