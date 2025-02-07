using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIGuia.Model;

[Table("Reservas")]
public class Reserva
{
    [Key]
    public int ReservaId { get; set; }

    [Required(ErrorMessage = "A data de entrada é obrigatória.")]
    public DateTime DataEntrada { get; set; }

    [Required(ErrorMessage = "A data de saída é obrigatória.")]
    public DateTime DataSaida { get; set; }

    [Required(ErrorMessage = "O valor da reserva é obrigatório.")]
    [Column(TypeName = "decimal(12, 2)")]
    public decimal Valor { get; set; }

    public int ClienteId { get; set; }

    public Cliente? Cliente { get; set; }

    public int SuiteId { get; set; }

    public Suite? Suite { get; set; }
}
