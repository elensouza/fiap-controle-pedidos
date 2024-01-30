﻿using Core.Entities;
using Core.Enums;
using Core.Interfaces.Application.Services;
using Core.Interfaces.Infra.Database;

namespace Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _pedidoRepository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _pedidoRepository = pedidoRepository;
    }

    public IEnumerable<Pedido> ObtemPedidos()
    {
        return _pedidoRepository.ObtemPedidos();
    }

    public IEnumerable<Pedido> ObtemPedidosPorStatus(StatusPedido statusPedido)
    {
        return _pedidoRepository.ObtemPedidosPorStatus(statusPedido);
    }

    public Guid CheckoutPedido(Pedido pedido)
    {
        return _pedidoRepository.CheckoutPedido(pedido);
    }

    public void AtualizaPedidoStatus(Guid id, StatusPedido status)
    {
        _pedidoRepository.AtualizaPedidoStatus(id, status);
    }
    
    public void UpdatePagamento(Guid id, int status)
    {
        _pedidoRepository.UpdatePagamento(id, status);
    }
}