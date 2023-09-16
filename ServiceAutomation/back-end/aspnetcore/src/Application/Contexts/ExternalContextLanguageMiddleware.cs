using Domain.Shared.Contexts;
using Domain.Shared.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.Contexts;

public class ExternalContextLanguageMiddleware : IMiddleware
{
    private readonly IExternalContextService externalContextService;

    public ExternalContextLanguageMiddleware(IExternalContextService externalContextService)
    {
        this.externalContextService = externalContextService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Request.Headers.TryGetValue("Accept-Language", out var languageCode);
        externalContextService.SetLanguage(GetLanguage(languageCode));
        await next(context);
    }

    private static Language GetLanguage(string languageCode)
    {
        return languageCode switch
        {
            "tr-TR" => Language.Turkish,
            "en-EN" => Language.English,
            _ => Language.Turkish
        };
    }
}