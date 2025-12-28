using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace PersonelTransportAutomation.Pages;

public class Index_Tests : PersonelTransportAutomationWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
