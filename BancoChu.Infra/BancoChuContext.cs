using BancoChu.Domain.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BancoChu.Infra;

public class BancoChuContext : DbContext
{
	public BancoChuContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Conta> Contas { get; set; }
	public DbSet<Transacao> Transacoes { get; set; }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Conta>()
			.HasKey(m => m.Id);

		builder.Entity<Transacao>()
			.HasKey(m => m.Id);

		builder.Entity<Transacao>()
			.HasIndex(t => new { t.ContaOrigemId, t.ContaDestinoId });

		builder.Entity<Transacao>()
			.HasOne(t => t.ContaOrigem)
			.WithMany()
			.HasForeignKey(t => t.ContaOrigemId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Transacao>()
			.Property(t => t.Data)
			.HasColumnType("timestamp with time zone");

		builder.Entity<Transacao>()
			.HasOne(t => t.ContaDestino)
			.WithMany()
			.HasForeignKey(t => t.ContaDestinoId)
			.OnDelete(DeleteBehavior.Restrict);

		base.OnModelCreating(builder);
	}
}
