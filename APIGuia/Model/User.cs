using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGuia.Model;

[Table("Usuarios")]
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    [StringLength(80, ErrorMessage = "O nome não pode ter mais de 80 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email não é válido.")]
    [StringLength(100, ErrorMessage = "O email não pode ter mais de 100 caracteres.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(255)]
    public string? password { get; set; }

    [Required(ErrorMessage = "O tipo de funcionário é obrigatório.")]
    [StringLength(30, ErrorMessage = "O tipo de funcionário não pode ter mais de 30 caracteres.")]
    public string? TipoFuncionario { get; set; }

}
