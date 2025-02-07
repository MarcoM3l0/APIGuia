using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGuia.Model;

[Table("Moteis")]
public class Motel
{
    public Motel()
    {
        Suites = new Collection<Suite>();
    }

    [Key]
    public int MotelId { get; set; }

    [Required(ErrorMessage = "O nome do motel é obrigatório.")]
    [StringLength(70, ErrorMessage = "O nome não pode ter mais de 70 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O endereço do motel é obrigatório.")]
    [StringLength(200, ErrorMessage = "O endereço não pode ter mais de 200 caracteres.")]
    public string? Endereco { get; set; }

    public ICollection<Suite>? Suites { get; set; }
}
