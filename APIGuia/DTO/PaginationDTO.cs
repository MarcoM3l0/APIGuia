namespace APIGuia.DTO;

// Essa classe é um DTO (Data Transfer Object) que representa a entity Pagination.
public class PaginationDTO<T>
{
    public IEnumerable<T>? Dados { get; set; }
    public int PaginaAtual { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
