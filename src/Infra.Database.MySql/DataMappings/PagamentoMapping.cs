using Infra.Database.MySql.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Database.MySql.DataMappings;

public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamento");
        builder.HasKey(p => p.Id);
        builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
        builder.Property(p => p.Valor).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Status).IsRequired();
    }
}