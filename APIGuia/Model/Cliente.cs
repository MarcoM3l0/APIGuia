using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGuia.Model;

// Essa classe é um model para a entity Cliente
[Table("Clientes")]
public class Cliente
{
    public Cliente()
    {
        Reservas = new Collection<Reserva>();
    }
    [Key]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O telefone é obrigatório.")]
    [StringLength(15, ErrorMessage = "O telefone não pode ter mais de 15 caracteres.")]
    public string? Telefone { get; set; }

    public ICollection<Reserva>? Reservas { get; set; }
}
