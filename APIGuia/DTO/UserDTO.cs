using APIGuia.Model;

namespace APIGuia.DTO;

public class UserDTO
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? TipoFuncionario { get; set; }
    public string? password { get; set; }

    public UserDTO() { }

    public UserDTO(User user)
    {
        Nome = user.Nome;
        Email = user.Email;
        TipoFuncionario = user.TipoFuncionario;
    }
}
