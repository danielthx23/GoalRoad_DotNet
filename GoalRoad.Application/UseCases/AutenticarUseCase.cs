using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using GoalRoad.Application.UseCases.Interfaces;
using GoalRoad.Domain.Interfaces;

namespace GoalRoad.Application.UseCases
{
    public class AutenticarUseCase : IAutenticarUseCase
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguration _config;

        public AutenticarUseCase(IUsuarioRepository usuarioRepo, IConfiguration config)
        {
            _usuarioRepo = usuarioRepo;
            _config = config;
        }

        public async Task<string?> AutenticarAsync(string email, string senha)
        {
            var user = await _usuarioRepo.ObterPorEmailAsync(email);
            if (user == null) return null;
            if (user.SenhaUsuario != senha) return null;
            
            string? key = _config["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(key)) key = Environment.GetEnvironmentVariable("Jwt__Key");
            if (string.IsNullOrWhiteSpace(key)) key = _config["Jwt:SecretKey"];
            if (string.IsNullOrWhiteSpace(key)) key = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            if (string.IsNullOrWhiteSpace(key)) key = Environment.GetEnvironmentVariable("JWT_KEY");
            if (string.IsNullOrWhiteSpace(key)) key = "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm";

            var issuer = _config["Jwt:Issuer"] ?? Environment.GetEnvironmentVariable("Jwt__Issuer") ?? string.Empty;
            var audience = _config["Jwt:Audience"] ?? Environment.GetEnvironmentVariable("Jwt__Audience") ?? string.Empty;
            var expiresMinutesStr = _config["Jwt:ExpiresMinutes"] ?? "120";
            int.TryParse(expiresMinutesStr, out var expiresMinutes);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, user.EmailUsuario),
                new Claim(ClaimTypes.Name, user.NomeUsuario)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(key!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
