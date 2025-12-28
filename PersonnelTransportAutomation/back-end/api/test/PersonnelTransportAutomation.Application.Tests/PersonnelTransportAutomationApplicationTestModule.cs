using Volo.Abp.Modularity;

namespace PersonnelTransportAutomation;

[DependsOn(
    typeof(PersonnelTransportAutomationApplicationModule),
    typeof(PersonnelTransportAutomationDomainTestModule)
)]
public class PersonnelTransportAutomationApplicationTestModule : AbpModule
{

}
