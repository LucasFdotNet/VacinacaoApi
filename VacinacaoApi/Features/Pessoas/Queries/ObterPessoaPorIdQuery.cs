using MediatR;

using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Pessoas.Queries;

// Query de requisição para obter os detalhes de uma pessoa. O retorno esperado é um PessoaDetalhesDto ou null se não encontrar nada.
public class ObterPessoaPorIdQuery : IRequest<PessoaDetalhesDto?>
{
    public Guid Id { get; set; }
}