namespace APIGuia.DTO;

public class PaginationDTO<T>
{
    public IEnumerable<T>? Dados { get; set; }
    public int PaginaAtual { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
