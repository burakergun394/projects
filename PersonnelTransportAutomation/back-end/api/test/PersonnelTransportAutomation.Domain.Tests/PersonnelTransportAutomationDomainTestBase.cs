using Volo.Abp.Modularity;

namespace PersonnelTransportAutomation;

/* Inherit from this class for your domain layer tests. */
public abstract class PersonnelTransportAutomationDomainTestBase<TStartupModule> : PersonnelTransportAutomationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
