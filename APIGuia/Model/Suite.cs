using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGuia.Model;

// Essa classe é um model para a entity Suite
[Table("Suites")]
public class Suite
{
    public Suite()
    {
        Reservas = new Collection<Reserva>();
    }

    [Key]
    public int SuiteId { get; set; }

    [Required(ErrorMessage = "O tipo de suíte é obrigatório.")]
    [StringLength(50, ErrorMessage = "O tipo não pode ter mais de 50 caracteres.")]
    public string? Tipo { get; set; }

    [Required(ErrorMessage = "O preço da suíte é obrigatório.")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Preco { get; set; }

    public int MotelId { get; set; }
    public Motel? Motel { get; set; } 

    public ICollection<Reserva>? Reservas { get; set; }
}
