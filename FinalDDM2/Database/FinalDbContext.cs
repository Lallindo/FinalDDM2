using FinalDDM2.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalDDM2.Database;

public class FinalDbContext : DbContext
{
    public FinalDbContext(DbContextOptions<FinalDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Clima> Climas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasMany(u => u.Buscas)
                .WithOne(b => b.Usuario)
                .IsRequired();
        });

        modelBuilder.Entity<Clima>(entity => {
            entity.HasOne(c => c.Usuario)
                .WithMany(u => u.Buscas)
                .IsRequired();

            entity.Ignore(c => c.TempFahrenheit);
            entity.Ignore(c => c.SensacaoTermFahrenheit);
            entity.Ignore(c => c.TempKelvin);
            entity.Ignore(c => c.SensacaoTermKelvin);
        });
    }

    public static string GetSqLiteConnection()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = Path.Join(path, "finaldb.db");
        return dbPath;
    }
}