using APIGuia.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGuia.Services;

public static class TokenService
{
    // Método para gerar um token
    public static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler(); // Manipulador de token
        var key = Encoding.ASCII.GetBytes(Settings.secret); // Chave de segurança

        // Configuração do token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            // Define as claims do token
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Email), // Adiciona o email como claim
                new Claim(ClaimTypes.Role, user.TipoFuncionario) // Adiciona o tipo de funcionário como claim
            }),
            Expires = DateTime.UtcNow.AddHours(2), // Tempo de expiração do token
            SigningCredentials = new SigningCredentials(
                                                    new SymmetricSecurityKey(key), // Chave de segurança
                                                    SecurityAlgorithms.HmacSha256Signature) // Algoritmo de criptografia
        };

        // Criar o token com base nas informações passadas
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Retorna o token gerado como string
        return tokenHandler.WriteToken(token);
    }
}
