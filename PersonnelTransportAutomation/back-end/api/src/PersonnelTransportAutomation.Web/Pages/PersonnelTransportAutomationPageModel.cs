using PersonnelTransportAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace PersonnelTransportAutomation.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PersonnelTransportAutomationPageModel : AbpPageModel
{
    protected PersonnelTransportAutomationPageModel()
    {
        LocalizationResourceType = typeof(PersonnelTransportAutomationResource);
    }
}
