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
        var pedidos = _context.Pedidos.Include(p => p.Cliente).Include(p => p.ItensPedido)
            .Include("ItensPedido.Produto")
            .Include(p => p.Pagamento)
            .Where(p => p.Status !=  StatusPedido.Finalizado)
            .OrderBy(p => p.Status)
            .ThenBy(p => p.DataPedido);

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
        pedidoInfra.Pagamento = new DataModels.Pagamento(pedido.PagamentoId, pedido.Valor);

        _context.Pedidos.Add(pedidoInfra);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        return _mapper.Map<Pedido>(pedidoInfra).Id;
    }

    public void AtualizaPedidoStatus(Guid id, StatusPedido status)
    {
        var pedidoContext = _context.Pedidos.AsNoTracking().FirstOrDefault(x => x.Id == id);
        if (pedidoContext == null)
            throw new Exception("Pedido não encontrado");
        
        var pedidoInfra = _mapper.Map<DataModels.Pedido>(pedidoContext);
        pedidoInfra.Status = status;
        _context.Entry(pedidoInfra).Property(x => x.Status).IsModified = true;
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }
    
    public Pagamento ObtemPagamentoPorId(Guid id)
    {   
        var pedido = _context.Pagamento.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return _mapper.Map<Pagamento>(pedido);
    }

    public void UpdatePagamento(Guid id, int status)
    {
        var pagamento = ObtemPagamentoPorId(id) ?? throw new Exception("Pagamento não encontrado");
        var pagamentoInfra = _mapper.Map<DataModels.Pagamento>(pagamento);
        pagamentoInfra.Status = (StatusPagamento)status;

        _context.Entry(pagamentoInfra).Property(x => x.Status).IsModified = true;
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
    }
}