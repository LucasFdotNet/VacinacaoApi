using System.ComponentModel.DataAnnotations;

namespace VacinacaoApi.Models;

public class Pessoa
{
    [Key]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Senha { get; set; } 
    public string NumeroIdentificacao { get; set; }

    // Lista de registros, considerando que uma pessoa pode ter vários registros de vacinação
    public virtual ICollection<RegistroVacinacao> CartaoDeVacinacao { get; set; } = new List<RegistroVacinacao>();
}