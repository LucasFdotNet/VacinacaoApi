using MediatR;
using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Pessoas.Queries.ConsultarCartaoVacinacao;

// recebe o ID da Pessoa e espera retornar uma lista (IEnumerable) de DTOs.
public class ConsultarCartaoVacinacaoQuery : IRequest<IEnumerable<CartaoVacinacaoDto>>
{
    public Guid PessoaId { get; set; }
}