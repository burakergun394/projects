using PersonelTransportAutomation.Samples;
using Xunit;

namespace PersonelTransportAutomation.EntityFrameworkCore.Domains;

[Collection(PersonelTransportAutomationTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<PersonelTransportAutomationEntityFrameworkCoreTestModule>
{

}
