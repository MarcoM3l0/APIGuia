using APIGuia.Model;
using Microsoft.EntityFrameworkCore;

namespace APIGuia.Context;

public class APIGuiaDBContext : DbContext
{
	public APIGuiaDBContext(DbContextOptions<APIGuiaDBContext> options) : base(options)
    {
    }

    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Suite> Suites { get; set; }
    public DbSet<Motel> Moteis { get; set; }
    public DbSet<User> Usuarios { get; set; }


    // Configuração de relacionamentos e restrições
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Cliente)
            .WithMany(c => c.Reservas)
            .HasForeignKey(r => r.ClienteId);

        modelBuilder.Entity<Reserva>()
            .HasOne(r => r.Suite)
            .WithMany(s => s.Reservas)
            .HasForeignKey(r => r.SuiteId);

        modelBuilder.Entity<Suite>()
            .HasOne(s => s.Motel)
            .WithMany(m => m.Suites)
            .HasForeignKey(s => s.MotelId);
    }

}
