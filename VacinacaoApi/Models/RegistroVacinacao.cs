using System.ComponentModel.DataAnnotations;

namespace VacinacaoApi.Models;

public class RegistroVacinacao
{
    [Key]
    public Guid Id { get; set; }
    public DateTime DataAplicacao { get; set; }
    public int Dose { get; set; }

    // Chaves estrangeiras
    public Guid PessoaId { get; set; }
    public Guid VacinaId { get; set; }

    // Props para relações do EF
    public virtual Pessoa Pessoa { get; set; }
    public virtual Vacina Vacina { get; set; }
}