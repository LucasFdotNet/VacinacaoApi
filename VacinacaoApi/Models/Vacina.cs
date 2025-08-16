using System.ComponentModel.DataAnnotations;

namespace VacinacaoApi.Models;

public class Vacina
{
    [Key]
    public Guid Id { get; set; }
    public string Nome { get; set; }
}