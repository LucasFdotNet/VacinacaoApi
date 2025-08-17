namespace VacinacaoApi.DTOs;

public class CartaoVacinacaoDto
{
    public Guid IdDoRegistro { get; set; }
    public string NomeVacina { get; set; }
    public int Dose { get; set; }
    public DateTime DataAplicacao { get; set; }
}