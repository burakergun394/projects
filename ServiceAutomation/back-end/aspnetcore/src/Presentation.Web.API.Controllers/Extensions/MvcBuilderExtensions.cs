using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Web.API.Controller.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddPresentationControllers(this IMvcBuilder builder)
    {
        Assembly currentAssembly = Assembly.GetExecutingAssembly();

        builder
            .AddApplicationPart(currentAssembly);

        return builder;
    }
}