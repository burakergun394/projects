using Volo.Abp.Modularity;

namespace PersonelTransportAutomation;

[DependsOn(
    typeof(PersonelTransportAutomationDomainModule),
    typeof(PersonelTransportAutomationTestBaseModule)
)]
public class PersonelTransportAutomationDomainTestModule : AbpModule
{

}
