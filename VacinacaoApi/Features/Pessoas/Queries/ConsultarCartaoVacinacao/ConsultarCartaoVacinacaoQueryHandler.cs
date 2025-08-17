using MediatR;
using Microsoft.EntityFrameworkCore;
using VacinacaoApi.Data;
using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Pessoas.Queries.ConsultarCartaoVacinacao;

public class ConsultarCartaoVacinacaoQueryHandler : IRequestHandler<ConsultarCartaoVacinacaoQuery, IEnumerable<CartaoVacinacaoDto>>
{
    private readonly VacinacaoContext _context;

    public ConsultarCartaoVacinacaoQueryHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CartaoVacinacaoDto>> Handle(ConsultarCartaoVacinacaoQuery request, CancellationToken cancellationToken)
    {
  
        var pessoaExiste = await _context.Pessoas.AnyAsync(p => p.Id == request.PessoaId, cancellationToken);
        if (!pessoaExiste)
        {
            throw new KeyNotFoundException($"Pessoa com ID {request.PessoaId} não encontrada.");
        }

        var registros = await _context.RegistrosVacinacao
            // .Include() carrega a entidade 'Vacina' relacionada para cada registro.
            .Include(r => r.Vacina)
            .Where(r => r.PessoaId == request.PessoaId)
            // .Select() projeta o resultado no DTO.
            .Select(r => new CartaoVacinacaoDto
            {
                IdDoRegistro = r.Id,
                NomeVacina = r.Vacina.Nome, 
                Dose = r.Dose,
                DataAplicacao = r.DataAplicacao
            })
            .ToListAsync(cancellationToken);

        return registros;
    }
}