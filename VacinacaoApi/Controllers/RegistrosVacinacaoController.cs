using MediatR;

using Microsoft.AspNetCore.Mvc;

using VacinacaoApi.Features.RegistrosVacinacao.Commands.ExcluirRegistro;

namespace VacinacaoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrosVacinacaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegistrosVacinacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirRegistroVacinacao(Guid id)
    {
        var command = new ExcluirRegistroVacinacaoCommand { Id = id };
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}