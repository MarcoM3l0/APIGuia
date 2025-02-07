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

    public LoginController(APIGuiaDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] LoginDTO loginDTO)
    {
        var userDB = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == loginDTO.Email.ToLower());

        if (userDB == null || !BCrypt.Net.BCrypt.Verify(loginDTO.password, userDB.password))
        {
            return Unauthorized("Email ou senha incorretos.");
        }

        var token = TokenService.GenerateToken(userDB);

        userDB.password = "";

        return new
        {
            user = userDB,
            token
        };
    }
}
