using PersonelTransportAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PersonelTransportAutomation.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PersonelTransportAutomationController : AbpControllerBase
{
    protected PersonelTransportAutomationController()
    {
        LocalizationResource = typeof(PersonelTransportAutomationResource);
    }
}
