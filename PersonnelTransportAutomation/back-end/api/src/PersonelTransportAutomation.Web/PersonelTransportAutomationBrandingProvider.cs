using Microsoft.Extensions.Localization;
using PersonelTransportAutomation.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PersonelTransportAutomation.Web;

[Dependency(ReplaceServices = true)]
public class PersonelTransportAutomationBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PersonelTransportAutomationResource> _localizer;

    public PersonelTransportAutomationBrandingProvider(IStringLocalizer<PersonelTransportAutomationResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
