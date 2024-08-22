using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Content.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            
        var ev = Environment.GetEnvironmentVariable("SQLCONNSTR_TranscapeMVP");
        var connectionString = ev != null ? ev.Replace("\"", "").Replace("\\\\", "\\") : "environment is null";
        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}