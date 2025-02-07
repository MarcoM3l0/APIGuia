namespace APIGuia.DTO;

// Essa classe é um DTO (Data Transfer Object) que representa a entity Reserva.
public class ReservaDTO
{
    public int ReservaId { get; set; }
    public DateTime DataEntrada { get; set; }
    public DateTime DataSaida { get; set; }
    public string? ClienteNome { get; set; }
    public string? SuiteTipo { get; set; }
    public decimal Valor { get; set; }
}
