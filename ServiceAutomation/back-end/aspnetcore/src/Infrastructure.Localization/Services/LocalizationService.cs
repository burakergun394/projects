using Application.Abstractions.Localizations;
using Domain.Shared.Contexts;
using Domain.Shared.Enums;
using Newtonsoft.Json;
using System.Text;

namespace Infrastructure.Localization.Services;

internal class LocalizationService : ILocalizationService
{
    private readonly IExternalContextService contextService;
    private readonly JsonSerializer _serializer = new();
    private readonly Dictionary<Language, Dictionary<string, string>> localizations = new();

    public LocalizationService(IExternalContextService contextService)
    {
        this.contextService = contextService;
        InitializeLocalizations();
    }

    public string this[string name] => GetString(name);

    public string this[string name, params object[] args] => GetString(name, args);

    public string this[Language language, string name] => GetString(language, name);

    public string this[Language language, string name, params object[] args] => GetString(language, name, args);

    private string GetString(Language language, string name, params object[] args)
    {
        var dictionary = localizations[language];

        if (!dictionary.ContainsKey(name))
            return name;

        var value = dictionary[name];

        if (string.IsNullOrWhiteSpace(value))
            return name;

        return string.Format(value, args);
    }

    private string GetString(string name, params object[] args)
    {
        var language = contextService.GetLanguage();
        return GetString(language, name, args);
    }

    private void InitializeLocalizations()
    {
        var resourcesFolderPath = Path.GetFullPath($"Resources");
        if (!Directory.Exists(resourcesFolderPath))
            return;

        var files = Directory.GetFiles(resourcesFolderPath, "*.json", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            var language = GetLanguage(fileName);

            if (!localizations.ContainsKey(language))
                localizations[language] = new Dictionary<string, string>();

            LoadLocalizationFile(file, localizations[language]);
        }
    }

    private void LoadLocalizationFile(string filePath, Dictionary<string, string> localizationDictionary)
    {
        using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var streamReader = new StreamReader(fileStream, Encoding.UTF8);
        using var reader = new JsonTextReader(streamReader);
        while (reader.Read())
        {
            if (reader.TokenType != JsonToken.PropertyName)
                continue;

            string? key = reader.Value as string;
            if (string.IsNullOrWhiteSpace(key))
                continue;

            reader.Read();
            var value = _serializer.Deserialize<string>(reader);
            if (!localizationDictionary.ContainsKey(key))
                localizationDictionary[key] = value;
        }
    }

    private static Language GetLanguage(string language)
    {
        return language switch
        {
            "tr" => Language.Turkish,
            "en" => Language.English,
            _ => Language.Turkish
        };
    }
}
