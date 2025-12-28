using Volo.Abp.Modularity;

namespace PersonelTransportAutomation;

[DependsOn(
    typeof(PersonelTransportAutomationApplicationModule),
    typeof(PersonelTransportAutomationDomainTestModule)
)]
public class PersonelTransportAutomationApplicationTestModule : AbpModule
{

}
