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

    // Injeção de dependência do contexto do banco de dados
    public CadastroController(APIGuiaDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "webmaster")]
    public async Task<ActionResult<User>> PostUser(UserDTO userDto)
    {

        // Validação dos campos obrigatórios
        if (string.IsNullOrWhiteSpace(userDto.Nome) || string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.password))
        {
            return BadRequest("Nome, Email e Senha são obrigatórios.");
        }

        // Verifica se o email já está cadastrado
        if (await _context.Usuarios.AnyAsync(u => u.Email == userDto.Email))
        {
            return Conflict("Já existe um usuário com este email.");
        }


        // Cria um novo usuário com os dados do DTO e criptografa a senha
        var user = new User
        {
            Nome = userDto.Nome,
            Email = userDto.Email.ToLower(),
            TipoFuncionario = userDto.TipoFuncionario?.ToLower(), 
            password = BCrypt.Net.BCrypt.HashPassword(userDto.password) // Criptografa a senha
        };

        // Adiciona o usuário ao banco de dados e salva as alterações
        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();

        // Retorna uma mensagem de sucesso
        return Ok(new { message = "Usuário cadastrado com sucesso!" });
    }


}
