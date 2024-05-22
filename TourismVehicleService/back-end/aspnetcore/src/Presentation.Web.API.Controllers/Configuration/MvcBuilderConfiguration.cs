using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Presentation.Web.API.Controllers.Configuration;

public static class MvcBuilderConfiguration
{
    public static IMvcBuilder AddPresentationControllers(this IMvcBuilder builder)
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        builder
            .AddApplicationPart(currentAssembly);

        return builder;
    }
}