using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinalDDM2.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinalDbContext>
    {
        public FinalDbContext CreateDbContext(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "finaldb.db");

            var optionsBuilder = new DbContextOptionsBuilder<FinalDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new FinalDbContext(optionsBuilder.Options);
        }
    }