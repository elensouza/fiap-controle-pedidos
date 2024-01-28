namespace Application.Dtos;
public class PagamentoDto
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public string Status { get; set; }
}