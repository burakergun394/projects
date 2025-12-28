using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PersonelTransportAutomation.Data;

/* This is used if database provider does't define
 * IPersonelTransportAutomationDbSchemaMigrator implementation.
 */
public class NullPersonelTransportAutomationDbSchemaMigrator : IPersonelTransportAutomationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
