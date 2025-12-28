using Microsoft.AspNetCore.Builder;
using PersonnelTransportAutomation;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("PersonnelTransportAutomation.Web.csproj");
await builder.RunAbpModuleAsync<PersonnelTransportAutomationWebTestModule>(applicationName: "PersonnelTransportAutomation.Web" );

public partial class Program
{
}
