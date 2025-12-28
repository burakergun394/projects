using PersonnelTransportAutomation.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace PersonnelTransportAutomation.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PersonnelTransportAutomationEntityFrameworkCoreModule),
    typeof(PersonnelTransportAutomationApplicationContractsModule)
    )]
public class PersonnelTransportAutomationDbMigratorModule : AbpModule
{
}
