using FluentValidation;

namespace VacinacaoApi.Features.Pessoas.Commands.RegistrarVacinacao;

public class RegistrarVacinacaoCommandValidator : AbstractValidator<RegistrarVacinacaoCommand>
{
    public RegistrarVacinacaoCommandValidator()
    {
        RuleFor(r => r.PessoaId).NotEmpty();
        RuleFor(r => r.VacinaId).NotEmpty();

        RuleFor(r => r.Dose)
            .GreaterThan(0).WithMessage("A dose deve ser um número positivo.");

        RuleFor(r => r.DataAplicacao)
            .NotEmpty().WithMessage("A data de aplicação é obrigatória.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de aplicação não pode ser no futuro.");
    }
}