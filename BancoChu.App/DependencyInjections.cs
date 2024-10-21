using BancoChu.Domain.Entidades;
using BancoChu.Domain.Entidades.Validators;
using BancoChu.Domain.Handlers;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Infra.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BancoChu.App;

public static class DependencyInjections
{
	public static void AddServices(this IServiceCollection services, IConfiguration configuration)
	{
		AddHandlers(services);
		AddRepositories(services);
		AddValidators(services);
		AddJwt(services, configuration);
	}

	private static void AddValidators(IServiceCollection services)
	{
		services.AddScoped<IValidator<Conta>, ContaValidator>();
		services.AddScoped<IValidator<TransacaoCommand>, TransacaoValidator>();
	}

	private static void AddRepositories(IServiceCollection services)
	{
		services.AddScoped<ITransacoesRepository, TransacoesRepository>();
		services.AddScoped<IContasRepository, ContasRepository>();
	}

	private static void AddHandlers(IServiceCollection services)
	{
		services.AddScoped<ITransacoesHandler, TransacoesHandler>();
	}

	private static void AddJwt(IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(opt =>
		{
			opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,

				ValidIssuer = configuration["jwt:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:SecretKey"])),
				ClockSkew = TimeSpan.Zero
			};
		});
	}
}
