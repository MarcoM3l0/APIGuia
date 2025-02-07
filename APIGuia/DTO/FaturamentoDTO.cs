namespace APIGuia.DTO;

// Essa classe é um DTO (Data Transfer Object) que representa a entity Faturamento.
public class FaturamentoDTO
{
    public int Ano { get; set; }
    public int Mes { get; set; }
    public int TotalReservas { get; set; }
    public decimal TotalFaturado { get; set; }
}
