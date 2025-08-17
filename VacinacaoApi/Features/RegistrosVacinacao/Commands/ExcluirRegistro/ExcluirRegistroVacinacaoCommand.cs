using MediatR;

namespace VacinacaoApi.Features.RegistrosVacinacao.Commands.ExcluirRegistro;

public class ExcluirRegistroVacinacaoCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}