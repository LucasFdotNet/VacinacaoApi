using MediatR;
using System.Text.Json.Serialization;

namespace VacinacaoApi.Features.Pessoas.Commands.RegistrarVacinacao;

public class RegistrarVacinacaoCommand : IRequest<Guid>
{
    // Esta propriedade será preenchida pela rota da API, não pelo conteúdo do JSON.
    [JsonIgnore]
    public Guid PessoaId { get; set; }


    public Guid VacinaId { get; set; }
    public int Dose { get; set; }
    public DateTime DataAplicacao { get; set; }
}