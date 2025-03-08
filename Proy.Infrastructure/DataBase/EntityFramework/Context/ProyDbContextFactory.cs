using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Proy.Infrastructure.DataBase.EntityFramework.Context;

public class ProyDbContextFactory :IDesignTimeDbContextFactory<ProyDbContext>
{
    public ProyDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddUserSecrets<ProyDbContextFactory>()  
            .Build();

        string connectionString = "server=localhost;port=3306;database=proyPoa;user=root;password=mysql1234";
        
        var optionsBuilder = new DbContextOptionsBuilder<ProyDbContext>();
        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        return new ProyDbContext(optionsBuilder.Options);
    }
}