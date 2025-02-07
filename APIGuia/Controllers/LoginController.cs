using APIGuia.Context;
using APIGuia.DTO;
using APIGuia.Model;
using APIGuia.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIGuia.Controllers;


[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly APIGuiaDBContext _context;

    // Injeção de dependência do contexto do banco de dados
    public LoginController(APIGuiaDBContext context)
    {
        _context = context;
    }

    // Endpoint para autenticação de usuários
    [HttpPost]
    public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] LoginDTO loginDTO)
    {
        // Busca o usuário no banco de dados pelo email
        var userDB = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

        // Verifica se o email e a senha foram informados
        if (string.IsNullOrWhiteSpace(loginDTO.Email) || string.IsNullOrWhiteSpace(loginDTO.password))
        {
            return BadRequest("Email e senha são obrigatórios.");
        }

        // Verifica se o usuário existe e se a senha está correta
        if (userDB == null || !BCrypt.Net.BCrypt.Verify(loginDTO.password, userDB.password))
        {
            return Unauthorized("Email ou senha incorretos.");
        }

        // Gera um token JWT para o usuário autenticado
        var token = TokenService.GenerateToken(userDB);

        // Remove a senha do objeto de usuário antes de retorná-lo (por segurança)
        userDB.password = "";

        // Retorna o usuário e o token gerado
        return new
        {
            user = userDB,
            token
        };
    }
}
