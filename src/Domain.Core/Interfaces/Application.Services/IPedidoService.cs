using Core.Entities;
using Core.Enums;

namespace Core.Interfaces.Application.Services;

public interface IPedidoService
{
    IEnumerable<Pedido> ObtemPedidos();
    IEnumerable<Pedido> ObtemPedidosPorStatus(StatusPedido statusPedido);
    Guid CheckoutPedido(Pedido pedido);
    void AtualizaPedidoStatus(Guid id, StatusPedido status);
    void UpdatePagamento(Guid id, int status);
}