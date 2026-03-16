using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Features.Veiculos.Commands.AtualizarVeiculo;
using Estacionamento.Application.Features.Veiculos.Commands.CadastrarVeiculo;
using Estacionamento.Application.Features.Veiculos.Commands.ExcluirVeiculo;
using Estacionamento.Application.Features.Veiculos.Queries.ListarVeiculos;
using Estacionamento.Application.Features.Veiculos.Queries.ObterVeiculoPorId;
using Estacionamento.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("veiculos")]
public class VeiculosController : ControllerBase
{
    private readonly IMediator _mediator;

    public VeiculosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(VeiculoResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<VeiculoResponse>> Cadastrar([FromBody] CadastrarVeiculoRequest request, CancellationToken cancellationToken)
    {
        VeiculoModel veiculo;
        veiculo = await _mediator.Send(new CadastrarVeiculoCommand
        {
            Descricao = request.Descricao,
            Marca = request.Marca,
            Modelo = request.Modelo,
            Opcionais = request.Opcionais,
            Valor = request.Valor
        }, cancellationToken);
        return CreatedAtAction(nameof(ObterPorId), new { id = veiculo.Id }, MapVeiculo(veiculo));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(VeiculoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculoResponse>> Atualizar(Guid id, [FromBody] AtualizarVeiculoRequest request, CancellationToken cancellationToken)
    {
        VeiculoModel veiculo;
        veiculo = await _mediator.Send(new AtualizarVeiculoCommand
        {
            Id = id,
            Descricao = request.Descricao,
            Marca = request.Marca,
            Modelo = request.Modelo,
            Opcionais = request.Opcionais,
            Valor = request.Valor
        }, cancellationToken);
        return Ok(MapVeiculo(veiculo));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VeiculoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeiculoResponse>> ObterPorId(Guid id, CancellationToken cancellationToken)
    {
        VeiculoModel veiculo;
        veiculo = await _mediator.Send(new ObterVeiculoPorIdQuery
        {
            Id = id
        }, cancellationToken);
        return Ok(MapVeiculo(veiculo));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VeiculoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<VeiculoResponse>>> Listar(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<VeiculoModel> veiculos;
        veiculos = await _mediator.Send(new ListarVeiculosQuery(), cancellationToken);
        return Ok(veiculos.Select(MapVeiculo).ToList());
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Excluir(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ExcluirVeiculoCommand
        {
            Id = id
        }, cancellationToken);
        return NoContent();
    }

    private static VeiculoResponse MapVeiculo(VeiculoModel veiculo)
    {
        return new VeiculoResponse
        {
            Id = veiculo.Id,
            Descricao = veiculo.Descricao,
            Marca = veiculo.Marca,
            Modelo = veiculo.Modelo,
            Opcionais = veiculo.Opcionais,
            Valor = veiculo.Valor
        };
    }

}
