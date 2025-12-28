using PersonelTransportAutomation.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PersonelTransportAutomation.Permissions;

public class PersonelTransportAutomationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PersonelTransportAutomationPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PersonelTransportAutomationPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PersonelTransportAutomationResource>(name);
    }
}
