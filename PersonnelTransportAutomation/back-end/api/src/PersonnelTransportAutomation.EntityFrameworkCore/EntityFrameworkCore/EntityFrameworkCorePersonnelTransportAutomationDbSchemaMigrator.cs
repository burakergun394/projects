using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonnelTransportAutomation.Data;
using Volo.Abp.DependencyInjection;

namespace PersonnelTransportAutomation.EntityFrameworkCore;

public class EntityFrameworkCorePersonnelTransportAutomationDbSchemaMigrator
    : IPersonnelTransportAutomationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePersonnelTransportAutomationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the PersonnelTransportAutomationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PersonnelTransportAutomationDbContext>()
            .Database
            .MigrateAsync();
    }
}
