namespace PersonnelTransport.Infrastructure.GoogleMaps;

/// <summary>
/// Strongly-typed configuration for Google Maps API.
/// </summary>
public class GoogleMapsSettings
{
    public const string SectionName = "GoogleMaps";
    
    public string ApiKey { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://maps.googleapis.com/maps/api";
}
