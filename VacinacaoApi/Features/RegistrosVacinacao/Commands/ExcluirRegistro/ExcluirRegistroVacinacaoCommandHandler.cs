using MediatR;

using VacinacaoApi.Data;

namespace VacinacaoApi.Features.RegistrosVacinacao.Commands.ExcluirRegistro;

public class ExcluirRegistroVacinacaoCommandHandler : IRequestHandler<ExcluirRegistroVacinacaoCommand, bool>
{
    private readonly VacinacaoContext _context;

    public ExcluirRegistroVacinacaoCommandHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ExcluirRegistroVacinacaoCommand request, CancellationToken cancellationToken)
    {
        var registro = await _context.RegistrosVacinacao.FindAsync(request.Id);

        if (registro is null)
        {
            return false;
        }

        _context.RegistrosVacinacao.Remove(registro);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}