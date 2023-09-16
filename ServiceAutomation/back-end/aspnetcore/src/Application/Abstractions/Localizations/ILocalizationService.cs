using Domain.Shared.Enums;

namespace Application.Abstractions.Localizations;

public interface ILocalizationService
{

    string this[string name] { get; }
    string this[string name, params object[] args] { get; }

    string this[Language language, string name] { get; }
    string this[Language language, string name, params object[] args] { get; }
}
