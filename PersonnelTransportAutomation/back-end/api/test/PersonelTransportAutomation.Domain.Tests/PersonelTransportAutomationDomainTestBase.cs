using Volo.Abp.Modularity;

namespace PersonelTransportAutomation;

/* Inherit from this class for your domain layer tests. */
public abstract class PersonelTransportAutomationDomainTestBase<TStartupModule> : PersonelTransportAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
