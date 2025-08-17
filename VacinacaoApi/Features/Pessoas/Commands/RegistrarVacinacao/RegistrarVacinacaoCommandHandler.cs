using MediatR;
using Microsoft.EntityFrameworkCore;
using VacinacaoApi.Data;
using VacinacaoApi.Models;

namespace VacinacaoApi.Features.Pessoas.Commands.RegistrarVacinacao;

public class RegistrarVacinacaoCommandHandler : IRequestHandler<RegistrarVacinacaoCommand, Guid>
{
    private readonly VacinacaoContext _context;

    public RegistrarVacinacaoCommandHandler(VacinacaoContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(RegistrarVacinacaoCommand request, CancellationToken cancellationToken)
    {
        // 1: Verificar se a Pessoa existe
        var pessoaExiste = await _context.Pessoas.AnyAsync(p => p.Id == request.PessoaId, cancellationToken);
        if (!pessoaExiste)
        {
            // Lança uma exceção se a pessoa não for encontrada.
            // Um middleware de tratamento de exceções poderia capturar isso e retornar um 404.
            throw new KeyNotFoundException($"Pessoa com ID {request.PessoaId} não encontrada.");
        }

        // 2: Verificar se a Vacina existe
        var vacinaExiste = await _context.Vacinas.AnyAsync(v => v.Id == request.VacinaId, cancellationToken);
        if (!vacinaExiste)
        {
            throw new KeyNotFoundException($"Vacina com ID {request.VacinaId} não encontrada.");
        }

        // Se ambos existem, cria o novo registro
        var registro = new RegistroVacinacao
        {
            Id = Guid.NewGuid(),
            PessoaId = request.PessoaId,
            VacinaId = request.VacinaId,
            Dose = request.Dose,
            DataAplicacao = request.DataAplicacao
        };

        _context.RegistrosVacinacao.Add(registro);
        await _context.SaveChangesAsync(cancellationToken);

        return registro.Id;
    }
}