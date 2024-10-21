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
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(BancoChuContext).Assembly);
	}
}
