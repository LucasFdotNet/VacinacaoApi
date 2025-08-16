using MediatR;
using Microsoft.EntityFrameworkCore;
using VacinacaoApi.Data;
using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Vacinas.Queries;

public class ListarVacinasQueryHandler : IRequestHandler<ListarVacinasQuery, IEnumerable<VacinaDto>>
{
    private readonly VacinacaoContext _context;

    public ListarVacinasQueryHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<VacinaDto>> Handle(ListarVacinasQuery request, CancellationToken cancellationToken)
    {
        // Usar .Select para projetar diretamente para o DTO 
        return await _context.Vacinas
            .AsNoTracking()
            .Select(v => new VacinaDto
            {
                Id = v.Id,
                Nome = v.Nome
            })
            .ToListAsync(cancellationToken);
    }
}