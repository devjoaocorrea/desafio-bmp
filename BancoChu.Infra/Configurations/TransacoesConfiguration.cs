using BancoChu.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BancoChu.Infra.Configurations;

public class TransacoesConfiguration : IEntityTypeConfiguration<Transacao>
{
	public void Configure(EntityTypeBuilder<Transacao> builder)
	{
		builder.HasKey(m => m.Id);

		builder.HasIndex(t => new { t.ContaOrigemId, t.ContaDestinoId });

		builder.Property(t => t.Mensagem)
			.HasMaxLength(100);

		builder
			.HasOne(t => t.ContaOrigem)
			.WithMany()
			.HasForeignKey(t => t.ContaOrigemId)
			.OnDelete(DeleteBehavior.Restrict);

		builder
			.Property(t => t.Data)
			.HasColumnType("timestamp with time zone");

		builder
			.HasOne(t => t.ContaDestino)
			.WithMany()
			.HasForeignKey(t => t.ContaDestinoId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}
