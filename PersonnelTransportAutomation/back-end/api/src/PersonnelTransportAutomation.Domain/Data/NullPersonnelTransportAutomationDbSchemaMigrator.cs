using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PersonnelTransportAutomation.Data;

/* This is used if database provider does't define
 * IPersonnelTransportAutomationDbSchemaMigrator implementation.
 */
public class NullPersonnelTransportAutomationDbSchemaMigrator : IPersonnelTransportAutomationDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
