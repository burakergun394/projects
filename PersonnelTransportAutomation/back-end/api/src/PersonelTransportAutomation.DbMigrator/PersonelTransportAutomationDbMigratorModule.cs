using PersonelTransportAutomation.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PersonelTransportAutomation.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PersonelTransportAutomationEntityFrameworkCoreModule),
    typeof(PersonelTransportAutomationApplicationContractsModule)
    )]
public class PersonelTransportAutomationDbMigratorModule : AbpModule
{
}
