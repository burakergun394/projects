using Domain.Shared.Enums;

namespace Domain.Shared.Contexts;

public interface IExternalContextService
{
    ContextMetadaData GetContext();
    void SetContext(ContextMetadaData context);
    Language GetLanguage();
    void SetLanguage(Language language);
}