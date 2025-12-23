using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DrinkManagement.Data;

public class DrinkDbContextFactory : IDesignTimeDbContextFactory<DrinkDbContext>
{
    public DrinkDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DrinkDbContext>();
        
        // Use environment variable or configuration for design-time operations
        // Set via: export DB_CONNECTION_STRING="Server=localhost;..."
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
            ?? "Server=localhost;Port=3306;Database=drinkmanagement;User=drinkuser;Password=YourSecurePassword123!;";
        
        optionsBuilder.UseMySql(
            connectionString,
            new MySqlServerVersion(new Version(8, 0, 26)),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        );

        return new DrinkDbContext(optionsBuilder.Options);
    }
}
