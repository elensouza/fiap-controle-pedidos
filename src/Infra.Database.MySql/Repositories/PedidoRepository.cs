using System.Data.Common;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Infra.Database;
using Infra.Database.MySql.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Database.MySql.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ControlePedidosContext _context;
    private readonly IMapper _mapper;

    public PedidoRepository(ControlePedidosContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<Pedido> ObtemPedidos()
    {
        var pedidos = _context.Pedidos.Include(p => p.Cliente).Include(p => p.ItensPedido).Include("ItensPedido.Produto").ToList();
        return _mapper.Map<IEnumerable<Pedido>>(pedidos);
    }

    public IEnumerable<Pedido> ObtemPedidosPorStatus(StatusPedido statusPedido)
    {
        var pedidos = _context.Pedidos.AsNoTracking().Where(p => p.Status == statusPedido).AsEnumerable();
        return _mapper.Map<IEnumerable<Pedido>>(pedidos);
    }

    public Guid CheckoutPedido(Pedido pedido)
    {
        var pedidoInfra = _mapper.Map<DataModels.Pedido>(pedido);
        
        _context.Pedidos.Add(pedidoInfra);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return _mapper.Map<Pedido>(pedidoInfra).Id;
    }

     public IEnumerable<Pedido> ObtemPedidosOrdenados()
    {
       var pedidos = _context.Pedidos.Include(p => p.Cliente).Include(p => p.ItensPedido).Include("ItensPedido.Produto")
            .Where(p => p.Status !=  StatusPedido.Finalizado)
            .OrderBy(p => p.Status)
            .ThenBy(p => p.DataPedido);

        return _mapper.Map<IEnumerable<Pedido>>(pedidos);
    }
}