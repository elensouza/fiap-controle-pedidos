using Core.Enums;
namespace Infra.Database.MySql.DataModels;

public sealed record Pagamento
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public StatusPagamento Status { get; set; }

    public Pagamento(Guid Id, decimal Valor)
    {
        this.Id = Id;
        this.Valor = Valor;
        Status = StatusPagamento.Pendente;
    }
}
