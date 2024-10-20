using BancoChu.Domain.Entidades;
using BancoChu.Domain.Entidades.Validators;
using BancoChu.Domain.Handlers;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Infra.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BancoChu.App;

public static class Container
{
	public static void AddServices(this IServiceCollection services)
	{
		AddHandlers(services);
		AddRepositories(services);
		AddValidators(services);
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
}
