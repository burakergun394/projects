using Volo.Abp.Modularity;

namespace PersonnelTransportAutomation;

public abstract class PersonnelTransportAutomationApplicationTestBase<TStartupModule> : PersonnelTransportAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
