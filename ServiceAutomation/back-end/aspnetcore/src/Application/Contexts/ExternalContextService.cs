using Domain.Shared.Contexts;
using Domain.Shared.Enums;

namespace Application.Contexts;

public class ExternalContextService : IExternalContextService
{
    private readonly static AsyncLocal<ContextMetadaData> _asyncLocal = new();
    private readonly static AsyncLocal<Language> _asyncLocalLanguage = new();

    public ContextMetadaData GetContext() => _asyncLocal.Value;

    public void SetContext(ContextMetadaData context) => _asyncLocal.Value = context;

    public Language GetLanguage() => _asyncLocalLanguage.Value;

    public void SetLanguage(Language language) => _asyncLocalLanguage.Value = language;
}
