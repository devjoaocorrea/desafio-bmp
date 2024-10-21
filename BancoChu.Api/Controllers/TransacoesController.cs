using BancoChu.App.Caching;
using BancoChu.Domain.Interfaces.Handlers;
using BancoChu.Domain.Interfaces.Repositories;
using BancoChu.Dto.Commands;
using BancoChu.Dto.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
	private readonly IMemoryCache _cache;
	private readonly ITransacoesHandler _handler;
	private readonly ITransacoesRepository _repository;
	private readonly IValidator<TransacaoCommand> _validator;

	public TransacoesController(
		IMemoryCache cache,
		ITransacoesHandler handler,
		ITransacoesRepository repository,
		IValidator<TransacaoCommand> validator)

	{
		_cache = cache;
		_handler = handler;
		_validator = validator;
		_repository = repository;
	}

	[HttpPost]
	public async Task<IActionResult> RealizarTransacao([FromBody] TransacaoCommand command)
	{
		var validation = _validator.Validate(command);

		if (!validation.IsValid)
		{
			return BadRequest(validation.Errors);
		}

		var result = await _handler.Handle(command);
		return Ok(result);
	}

	[HttpGet("extrato")]
	public async Task<IActionResult> ObterTransacoesPorPeriodo(DateTime dataInicio, DateTime dataFim)
	{
		if (!_cache.TryGetValue(CacheKeys.Extrato, out List<TransacaoResponse> transacoes))
		{
			if (dataInicio > dataFim)
			{
				return BadRequest("A data inicial não pode ser maior que a final");
			}

			dataInicio = DateTime.SpecifyKind(dataInicio, DateTimeKind.Utc);
			dataFim = DateTime.SpecifyKind(dataFim, DateTimeKind.Utc);
			dataFim = dataFim.AddDays(1).AddTicks(-1);

			transacoes = await _repository.BuscarTransacoesPorPeriodo(dataInicio, dataFim);

			if (!transacoes.Any())
			{
				return NotFound("Nenhuma transação realizada nesse período");
			}

			transacoes.ForEach(x => x.FormatarMensagem());

			_cache.Set(CacheKeys.Extrato, transacoes,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(10)));
		}

		return Ok(transacoes);
	}
}