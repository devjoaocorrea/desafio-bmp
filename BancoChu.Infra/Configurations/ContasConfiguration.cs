using BancoChu.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoChu.Infra.Configurations;

public class ContasConfiguration : IEntityTypeConfiguration<Conta>
{
	public void Configure(EntityTypeBuilder<Conta> builder)
	{
		builder.HasKey(m => m.Id);

		builder.Property(t => t.NumeroConta)
			.IsRequired();

		builder.Property(t => t.Agencia)
			.IsRequired();

		builder.Property(t => t.Titular)
			.HasMaxLength(250)
			.IsRequired();

		builder.Property(t => t.Documento)
			.IsRequired();

		builder.Property(t => t.ChavePix);
	}
}
