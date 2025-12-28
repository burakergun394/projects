using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonelTransportAutomation.Data;
using Volo.Abp.DependencyInjection;

namespace PersonelTransportAutomation.EntityFrameworkCore;

public class EntityFrameworkCorePersonelTransportAutomationDbSchemaMigrator
    : IPersonelTransportAutomationDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePersonelTransportAutomationDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the PersonelTransportAutomationDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PersonelTransportAutomationDbContext>()
            .Database
            .MigrateAsync();
    }
}
