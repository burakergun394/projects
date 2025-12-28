using System;
using System.Collections.Generic;
using System.Text;
using PersonelTransportAutomation.Localization;
using Volo.Abp.Application.Services;

namespace PersonelTransportAutomation;

/* Inherit your application services from this class.
 */
public abstract class PersonelTransportAutomationAppService : ApplicationService
{
    protected PersonelTransportAutomationAppService()
    {
        LocalizationResource = typeof(PersonelTransportAutomationResource);
    }
}
