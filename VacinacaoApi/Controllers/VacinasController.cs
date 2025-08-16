using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacinacaoApi.Features.Vacinas.Commands;
using VacinacaoApi.Features.Vacinas.Queries;

namespace VacinacaoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VacinasController : ControllerBase
{
    private readonly IMediator _mediator;

    public VacinasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CriarVacina([FromBody] CriarVacinaCommand command)
    {
        var vacinaId = await _mediator.Send(command);
        return Ok(new { Id = vacinaId });
    }

    [HttpGet]
    public async Task<IActionResult> ListarVacinas()
    {
        var vacinas = await _mediator.Send(new ListarVacinasQuery());
        return Ok(vacinas);
    }
}