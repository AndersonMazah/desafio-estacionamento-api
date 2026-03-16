using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Features.Usuarios.Commands.CadastrarUsuario;
using Estacionamento.Application.Features.Usuarios.Queries.ObterUsuarioAutenticado;
using Estacionamento.WebApi.Extensions;
using Estacionamento.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebApi.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UsuarioResponse>> Cadastrar([FromBody] CadastrarUsuarioRequest request, CancellationToken cancellationToken)
    {
        UsuarioModel usuario;
        usuario = await _mediator.Send(new CadastrarUsuarioCommand
        {
            Nome = request.Nome,
            Login = request.Login,
            Senha = request.Senha
        }, cancellationToken);
        return CreatedAtAction(nameof(ObterUsuarioAutenticado), null, MapUsuario(usuario));
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(UsuarioResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UsuarioResponse>> ObterUsuarioAutenticado(CancellationToken cancellationToken)
    {
        UsuarioModel usuario;
        usuario = await _mediator.Send(new ObterUsuarioAutenticadoQuery
        {
            UsuarioId = User.GetUsuarioId()
        }, cancellationToken);
        return Ok(MapUsuario(usuario));
    }

    private static UsuarioResponse MapUsuario(UsuarioModel usuario)
    {
        return new UsuarioResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Login = usuario.Login
        };
    }

}
