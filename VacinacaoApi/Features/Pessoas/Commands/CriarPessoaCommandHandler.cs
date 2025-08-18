using MediatR;

using VacinacaoApi.Data;
using VacinacaoApi.Models;

namespace VacinacaoApi.Features.Pessoas.Commands;

public class CriarPessoaCommandHandler : IRequestHandler<CriarPessoaCommand, Guid>
{
    private readonly VacinacaoContext _context;

    public CriarPessoaCommandHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CriarPessoaCommand request, CancellationToken cancellationToken)
    {

        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            NumeroIdentificacao = request.NumeroIdentificacao,
            Senha = request.Senha 
        };

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync(cancellationToken);

        return pessoa.Id;
    }
}