using FluentValidation;

namespace VacinacaoApi.Features.Pessoas.Commands;

public class CriarPessoaCommandValidator : AbstractValidator<CriarPessoaCommand>
{
    public CriarPessoaCommandValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");

        RuleFor(p => p.NumeroIdentificacao)
            .NotEmpty().WithMessage("O número de identificação é obrigatório.")
            .Length(11, 14).WithMessage("O número de identificação deve ser válido (ex: CPF ou CNPJ).");

        RuleFor(p => p.Senha)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(6).WithMessage("A senha deve ter no mínimo 6 caracteres.");
    }
}