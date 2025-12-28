using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace PersonnelTransportAutomation.Pages;

public class Index_Tests : PersonnelTransportAutomationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
