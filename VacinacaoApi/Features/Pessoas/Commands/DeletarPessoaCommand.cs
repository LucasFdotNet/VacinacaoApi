using MediatR;

namespace VacinacaoApi.Features.Pessoas.Commands;

public class DeletarPessoaCommand : IRequest<bool>
{
    // Deleta pelo ID
    public Guid Id { get; set; }
}