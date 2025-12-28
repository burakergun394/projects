using Volo.Abp.Modularity;

namespace PersonnelTransportAutomation;

[DependsOn(
    typeof(PersonnelTransportAutomationDomainModule),
    typeof(PersonnelTransportAutomationTestBaseModule)
)]
public class PersonnelTransportAutomationDomainTestModule : AbpModule
{

}
