namespace Application.Abstractions.Localizations;

public interface ILocalizationService
{

    string this[string name] { get; }
    string this[string name, params object[] args] { get; }
}
