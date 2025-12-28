using PersonnelTransportAutomation.Samples;
using Xunit;

namespace PersonnelTransportAutomation.EntityFrameworkCore.Domains;

[Collection(PersonnelTransportAutomationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<PersonnelTransportAutomationEntityFrameworkCoreTestModule>
{

}
