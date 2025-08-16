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
        // Cria uma nova entidade Pessoa com os dados do comando
        var pessoa = new Pessoa
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            NumeroIdentificacao = request.NumeroIdentificacao
        };

        // Adiciona a nova pessoa ao contexto do EF Core
        _context.Pessoas.Add(pessoa);

        // Salva as mudanças no banco de dados
        await _context.SaveChangesAsync(cancellationToken);

        // Retorna o Id da pessoa recém-criada
        return pessoa.Id;
    }
}