using MediatR;
using VacinacaoApi.Data;
using VacinacaoApi.Models;

namespace VacinacaoApi.Features.Vacinas.Commands;

public class CriarVacinaCommandHandler : IRequestHandler<CriarVacinaCommand, Guid>
{
    private readonly VacinacaoContext _context;

    public CriarVacinaCommandHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CriarVacinaCommand request, CancellationToken cancellationToken)
    {
        var vacina = new Vacina
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome
        };

        _context.Vacinas.Add(vacina);
        await _context.SaveChangesAsync(cancellationToken);

        return vacina.Id;
    }
}