using Core.Enums;

namespace Core.Entities;
public sealed record Pagamento
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public StatusPagamento Status { get; set; }
}