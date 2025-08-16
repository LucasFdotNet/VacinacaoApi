using MediatR;

using VacinacaoApi.Data;

namespace VacinacaoApi.Features.Pessoas.Commands;

public class DeletarPessoaCommandHandler : IRequestHandler<DeletarPessoaCommand, bool>
{
    private readonly VacinacaoContext _context;

    public DeletarPessoaCommandHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletarPessoaCommand request, CancellationToken cancellationToken)
    {
        // 1. Encontra a pessoa no banco de dados pelo ID 
        var pessoa = await _context.Pessoas.FindAsync(request.Id);

        // 2. Se a pessoa não for encontrada, retorna falso
        if (pessoa is null)
        {
            return false;
        }

        // 3. Remove a entidade 'pessoa' do contexto do EF.
        _context.Pessoas.Remove(pessoa);

        // 4. Executa o DELETE no banco.
        await _context.SaveChangesAsync(cancellationToken);

        // 5. Verdadeiro = exclusão bem-sucedida
        return true;
    }
}