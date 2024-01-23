using Infra.Database.MySql.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.MySql.DataMappings;

public class ItemPedidoMapping : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");
    builder.HasKey(p => p.Id);
    builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
    builder.Property(p => p.Quantidade).HasColumnType("int").IsRequired();
    builder.Property(p => p.PedidoId).HasColumnType("uniqueidentifier").IsRequired();
    builder.Property(p => p.ProdutoId).HasColumnType("uniqueidentifier").IsRequired();

    // Configuração do relacionamento com a entidade Pedido
    builder.HasOne(p => p.Pedido)
           .WithMany(p => p.ItensPedido) // Assume que há uma propriedade de navegação "ItensPedido" na entidade Pedido
           .HasForeignKey(p => p.PedidoId)
           .OnDelete(DeleteBehavior.Restrict);

    // Configuração do relacionamento com a entidade Produto
    builder.HasOne(p => p.Produto)
           .WithMany() // Assume que não há propriedade de navegação inversa na entidade Produto
           .HasForeignKey(p => p.ProdutoId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}