using APIGuia.Context;
using APIGuia.DTO;
using APIGuia.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIGuia.Controllers;

[ApiController]
[Route("[controller]")]
public class CadastroController : ControllerBase
{
    private readonly APIGuiaDBContext _context;

    public CadastroController(APIGuiaDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<User>> PostUser(UserDTO userDto)
    {
        if (string.IsNullOrWhiteSpace(userDto.Nome) || string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.password))
        {
            return BadRequest("Nome, Email e Senha são obrigatórios.");
        }

        if (await _context.Usuarios.AnyAsync(u => u.Email == userDto.Email))
        {
            return Conflict("Já existe um usuário com este email.");
        }

        var user = new User
        {
            Nome = userDto.Nome,
            Email = userDto.Email,
            TipoFuncionario = userDto.TipoFuncionario,
            password = BCrypt.Net.BCrypt.HashPassword(userDto.password) // Gera hash seguro da senha
        };

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetUser", new { id = user.UserId }, user);
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(new UserDTO(user));
    }


}
