using BancoChu.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BancoChu.App.Extensions;

public static class MigrationExtensions
{
	public static void ApplyMigrations(this IApplicationBuilder app)
	{
		using IServiceScope scope = app.ApplicationServices.CreateScope();
		using BancoChuContext context =
			scope.ServiceProvider.GetRequiredService<BancoChuContext>();

		context.Database.Migrate();
	}
}
