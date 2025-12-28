using System;
using System.Collections.Generic;
using System.Text;
using PersonnelTransportAutomation.Localization;
using Volo.Abp.Application.Services;

namespace PersonnelTransportAutomation;

/* Inherit your application services from this class.
 */
public abstract class PersonnelTransportAutomationAppService : ApplicationService
{
    protected PersonnelTransportAutomationAppService()
    {
        LocalizationResource = typeof(PersonnelTransportAutomationResource);
    }
}
