using Microsoft.EntityFrameworkCore;
using VacinacaoApi.Models;

namespace VacinacaoApi.Data;

public class VacinacaoContext : DbContext
{
    public VacinacaoContext(DbContextOptions<VacinacaoContext> options) : base(options)
    {
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Vacina> Vacinas { get; set; }
    public DbSet<RegistroVacinacao> RegistrosVacinacao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Pessoa pode ter vários RegistrosVacinacao + quando pessoa for deletada, seus registros também serão deletados
        modelBuilder.Entity<Pessoa>()
            .HasMany(p => p.CartaoDeVacinacao)
            .WithOne(r => r.Pessoa)
            .HasForeignKey(r => r.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}