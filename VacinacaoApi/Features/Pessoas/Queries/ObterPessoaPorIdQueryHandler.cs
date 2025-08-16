using MediatR;

using Microsoft.EntityFrameworkCore;

using VacinacaoApi.Data;
using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Pessoas.Queries;

public class ObterPessoaPorIdQueryHandler : IRequestHandler<ObterPessoaPorIdQuery, PessoaDetalhesDto?>
{
    private readonly VacinacaoContext _context;

    public ObterPessoaPorIdQueryHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<PessoaDetalhesDto?> Handle(ObterPessoaPorIdQuery request, CancellationToken cancellationToken)
    {
        var pessoa = await _context.Pessoas
            .AsNoTracking() 
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (pessoa is null)
        {
            return null; // Retorna nulo se a pessoa não for encontrada
        }

        // Mapeia a entidade para o DTO
        return new PessoaDetalhesDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            NumeroIdentificacao = pessoa.NumeroIdentificacao
        };
    }
}