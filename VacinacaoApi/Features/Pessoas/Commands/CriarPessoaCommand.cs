using MediatR;

namespace VacinacaoApi.Features.Pessoas.Commands;
// Ao executar IRequest<Guid>, o MediatR irá tratar essa requisição e retornar o Id da nova pessoa criada.
public class CriarPessoaCommand : IRequest<Guid>
{
    public string Nome { get; set; }
    public string NumeroIdentificacao { get; set; }
}