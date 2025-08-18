using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VacinacaoApi.Features.Pessoas.Commands;
using VacinacaoApi.Features.Pessoas.Commands.RegistrarVacinacao;
using VacinacaoApi.Features.Pessoas.Queries;
using VacinacaoApi.Features.Pessoas.Queries.ConsultarCartaoVacinacao;

namespace VacinacaoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PessoasController : ControllerBase
{
    private readonly IMediator _mediator;

    public PessoasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CriarPessoa([FromBody] CriarPessoaCommand command)
    {
        var pessoaId = await _mediator.Send(command);
        // Retorna o status 201 (Created) com a identificação da nova pessoa criada
        return CreatedAtAction(nameof(ObterPessoaPorId), new { id = pessoaId }, command);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPessoaPorId(Guid id)
    {
        var query = new ObterPessoaPorIdQuery { Id = id };
        var pessoaDto = await _mediator.Send(query);

        if (pessoaDto is null)
        {
            return NotFound(); // Retorna 404 se a pessoa não for encontrada
        }

        return Ok(pessoaDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarPessoa(Guid id)
    {
        // Cria o comando com o ID recebido da rota.
        var command = new DeletarPessoaCommand { Id = id };

        // Envia o comando para o MediatR, que o direcionará para o Handler correto.
        var result = await _mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // Registro de vacinação.

    [HttpPost("{pessoaId}/vacinacoes")]
    public async Task<IActionResult> RegistrarVacinacao(Guid pessoaId, [FromBody] RegistrarVacinacaoCommand command)
    {
        try
        {
            command.PessoaId = pessoaId;

            var registroId = await _mediator.Send(command);

            return Ok(new { Id = registroId });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    [HttpGet("{pessoaId}/vacinacoes")]
    public async Task<IActionResult> ConsultarCartaoVacinacao(Guid pessoaId)
    {
        try
        {
            var query = new ConsultarCartaoVacinacaoQuery { PessoaId = pessoaId };
            var cartaoVacinacao = await _mediator.Send(query);

            return Ok(cartaoVacinacao);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }
}