using MediatR;

using VacinacaoApi.DTOs;

namespace VacinacaoApi.Features.Vacinas.Queries;

public class ListarVacinasQuery : IRequest<IEnumerable<VacinaDto>>
{
}