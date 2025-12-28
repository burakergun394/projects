using PersonnelTransportAutomation.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PersonnelTransportAutomation.Permissions;

public class PersonnelTransportAutomationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PersonnelTransportAutomationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PersonnelTransportAutomationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PersonnelTransportAutomationResource>(name);
    }
}
