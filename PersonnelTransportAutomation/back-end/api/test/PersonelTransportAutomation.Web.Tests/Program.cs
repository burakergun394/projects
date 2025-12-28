using Microsoft.AspNetCore.Builder;
using PersonelTransportAutomation;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("PersonelTransportAutomation.Web.csproj");
await builder.RunAbpModuleAsync<PersonelTransportAutomationWebTestModule>(applicationName: "PersonelTransportAutomation.Web" );

public partial class Program
{
}
