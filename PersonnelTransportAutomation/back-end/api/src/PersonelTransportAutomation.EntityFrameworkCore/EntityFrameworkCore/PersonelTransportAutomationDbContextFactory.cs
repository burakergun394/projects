using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PersonelTransportAutomation.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class PersonelTransportAutomationDbContextFactory : IDesignTimeDbContextFactory<PersonelTransportAutomationDbContext>
{
    public PersonelTransportAutomationDbContext CreateDbContext(string[] args)
    {
        PersonelTransportAutomationEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PersonelTransportAutomationDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new PersonelTransportAutomationDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PersonelTransportAutomation.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
