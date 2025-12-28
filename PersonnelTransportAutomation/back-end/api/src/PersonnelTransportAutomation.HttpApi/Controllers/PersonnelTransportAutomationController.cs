using PersonnelTransportAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PersonnelTransportAutomation.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PersonnelTransportAutomationController : AbpControllerBase
{
    protected PersonnelTransportAutomationController()
    {
        LocalizationResource = typeof(PersonnelTransportAutomationResource);
    }
}
