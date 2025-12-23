using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DrinkManagement.Data;

public class DrinkDbContextFactory : IDesignTimeDbContextFactory<DrinkDbContext>
{
    public DrinkDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DrinkDbContext>();
        
        // Use a connection string for design-time operations
        // This allows migrations to be created without connecting to an actual database
        optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=drinkmanagement;User=root;Password=password;",
            new MySqlServerVersion(new Version(8, 0, 26)),
            mySqlOptions => mySqlOptions.EnableRetryOnFailure()
        );

        return new DrinkDbContext(optionsBuilder.Options);
    }
}
