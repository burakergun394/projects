using Microsoft.Extensions.Localization;
using PersonnelTransportAutomation.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PersonnelTransportAutomation.Web;

[Dependency(ReplaceServices = true)]
public class PersonnelTransportAutomationBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<PersonnelTransportAutomationResource> _localizer;

    public PersonnelTransportAutomationBrandingProvider(IStringLocalizer<PersonnelTransportAutomationResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
