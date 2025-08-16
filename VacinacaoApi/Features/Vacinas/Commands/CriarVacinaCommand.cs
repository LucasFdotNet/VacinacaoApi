using MediatR;

namespace VacinacaoApi.Features.Vacinas.Commands;

public class CriarVacinaCommand : IRequest<Guid>
{
    public string Nome { get; set; }
}