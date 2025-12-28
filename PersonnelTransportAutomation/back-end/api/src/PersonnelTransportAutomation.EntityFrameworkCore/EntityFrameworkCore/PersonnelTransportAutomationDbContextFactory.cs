using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace PersonnelTransportAutomation.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class PersonnelTransportAutomationDbContextFactory : IDesignTimeDbContextFactory<PersonnelTransportAutomationDbContext>
{
    public PersonnelTransportAutomationDbContext CreateDbContext(string[] args)
    {
        PersonnelTransportAutomationEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<PersonnelTransportAutomationDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new PersonnelTransportAutomationDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../PersonnelTransportAutomation.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
