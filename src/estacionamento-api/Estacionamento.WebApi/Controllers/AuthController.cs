using Estacionamento.Application.Common.Models;
using Estacionamento.Application.Features.Auth.Commands.Login;
using Estacionamento.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Estacionamento.WebApi.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginModel resultado;
        resultado = await _mediator.Send(new LoginCommand
        {
            Login = request.Login,
            Senha = request.Senha
        }, cancellationToken);
        return Ok(new LoginResponse
        {
            Token = resultado.Token
        });
    }

}
