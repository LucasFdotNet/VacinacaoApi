using FluentValidation;

namespace VacinacaoApi.Features.Vacinas.Commands.CriarVacina;

public class CriarVacinaCommandValidator : AbstractValidator<CriarVacinaCommand>
{
    public CriarVacinaCommandValidator()
    {
        RuleFor(v => v.Nome)
            .NotEmpty().WithMessage("O nome da vacina é obrigatório.")
            .MinimumLength(2).WithMessage("O nome da vacina deve ter no mínimo 2 caracteres.")
            .MaximumLength(100).WithMessage("O nome da vacina deve ter no máximo 100 caracteres.");
    }
}