using Infra.Database.MySql.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.MySql.DataMappings;

public class PedidoMapping : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        // builder.ToTable("Pedidos");
        // builder.HasKey(p => p.Id);
        // builder.Property(p => p.DataPedido).IsRequired();
        // builder.Property(p => p.Status).IsRequired();
        // builder.Property(p => p.ItensPedido).IsRequired();
        // builder.Property(p => p.ClienteId);

        // builder.HasOne(p => p.Cliente)
        //    .WithMany()
        //    .HasForeignKey(p => p.ClienteId)
        //    .OnDelete(DeleteBehavior.Restrict); 

        builder.ToTable("Pedidos");
    builder.HasKey(p => p.Id);
    builder.Property(p => p.DataPedido).IsRequired();
    builder.Property(p => p.Status).IsRequired();
    builder.Property(p => p.ClienteId).IsRequired();

    // Configuração do relacionamento com a entidade Cliente
    builder.HasOne(p => p.Cliente)
           .WithMany(c => c.Pedidos) // Assume que há uma propriedade de navegação "Pedidos" na entidade Cliente
           .HasForeignKey(p => p.ClienteId)
           .OnDelete(DeleteBehavior.Restrict);

    // Configuração do relacionamento com a entidade ItemPedido
    builder.HasMany(p => p.ItensPedido)
           .WithOne(ip => ip.Pedido) // Assume que há uma propriedade de navegação "Pedido" na entidade ItemPedido
           .HasForeignKey(ip => ip.PedidoId)
           .OnDelete(DeleteBehavior.Cascade); // Isso depende da lógica de negócios; pode ser necessário ajustar o comportamento de exclusão
}
    }
