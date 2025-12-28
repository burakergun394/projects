using Volo.Abp.Modularity;

namespace PersonelTransportAutomation;

public abstract class PersonelTransportAutomationApplicationTestBase<TStartupModule> : PersonelTransportAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
