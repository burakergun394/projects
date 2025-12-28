using PersonelTransportAutomation.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace PersonelTransportAutomation.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PersonelTransportAutomationPageModel : AbpPageModel
{
    protected PersonelTransportAutomationPageModel()
    {
        LocalizationResourceType = typeof(PersonelTransportAutomationResource);
    }
}
