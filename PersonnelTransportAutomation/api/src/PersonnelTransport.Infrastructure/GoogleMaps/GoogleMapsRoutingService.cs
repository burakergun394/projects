using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using PersonnelTransport.Application.Contracts;
using PersonnelTransport.Domain.Common;

namespace PersonnelTransport.Infrastructure.GoogleMaps;

/// <summary>
/// Google Maps API implementation of IRoutingService using HttpClient.
/// </summary>
public class GoogleMapsRoutingService(
    HttpClient httpClient,
    IOptions<GoogleMapsSettings> settings) : IRoutingService
{
    private readonly GoogleMapsSettings _settings = settings.Value;

    public async Task<DistanceMatrixResult> GetDistanceMatrixAsync(
        IReadOnlyList<Location> origins,
        IReadOnlyList<Location> destinations,
        CancellationToken cancellationToken = default)
    {
        var originsParam = string.Join("|", origins.Select(o => $"{o.Latitude},{o.Longitude}"));
        var destinationsParam = string.Join("|", destinations.Select(d => $"{d.Latitude},{d.Longitude}"));

        var url = $"{_settings.BaseUrl}/distancematrix/json" +
                  $"?origins={Uri.EscapeDataString(originsParam)}" +
                  $"&destinations={Uri.EscapeDataString(destinationsParam)}" +
                  $"&key={_settings.ApiKey}";

        var response = await httpClient.GetFromJsonAsync<JsonElement>(url, cancellationToken);

        var rows = new List<List<DistanceMatrixElement>>();
        var originAddresses = new List<string>();
        var destinationAddresses = new List<string>();

        if (response.TryGetProperty("origin_addresses", out var origAddrs))
        {
            foreach (var addr in origAddrs.EnumerateArray())
            {
                originAddresses.Add(addr.GetString() ?? string.Empty);
            }
        }

        if (response.TryGetProperty("destination_addresses", out var destAddrs))
        {
            foreach (var addr in destAddrs.EnumerateArray())
            {
                destinationAddresses.Add(addr.GetString() ?? string.Empty);
            }
        }

        if (response.TryGetProperty("rows", out var rowsElement))
        {
            foreach (var row in rowsElement.EnumerateArray())
            {
                var rowElements = new List<DistanceMatrixElement>();
                if (row.TryGetProperty("elements", out var elements))
                {
                    foreach (var element in elements.EnumerateArray())
                    {
                        var status = element.GetProperty("status").GetString() ?? "UNKNOWN";
                        double distance = 0;
                        double duration = 0;

                        if (status == "OK")
                        {
                            distance = element.GetProperty("distance").GetProperty("value").GetDouble();
                            duration = element.GetProperty("duration").GetProperty("value").GetDouble();
                        }

                        rowElements.Add(new DistanceMatrixElement(distance, duration, status));
                    }
                }
                rows.Add(rowElements);
            }
        }

        return new DistanceMatrixResult(rows, originAddresses, destinationAddresses);
    }

    public async Task<DirectionsResult> GetDirectionsAsync(
        Location origin,
        Location destination,
        IReadOnlyList<Location>? waypoints = null,
        bool optimizeWaypoints = false,
        CancellationToken cancellationToken = default)
    {
        var url = $"{_settings.BaseUrl}/directions/json" +
                  $"?origin={origin.Latitude},{origin.Longitude}" +
                  $"&destination={destination.Latitude},{destination.Longitude}" +
                  $"&key={_settings.ApiKey}";

        if (waypoints is { Count: > 0 })
        {
            var waypointsParam = string.Join("|", waypoints.Select(w => $"{w.Latitude},{w.Longitude}"));
            if (optimizeWaypoints)
            {
                waypointsParam = "optimize:true|" + waypointsParam;
            }
            url += $"&waypoints={Uri.EscapeDataString(waypointsParam)}";
        }

        var response = await httpClient.GetFromJsonAsync<JsonElement>(url, cancellationToken);

        var legs = new List<DirectionLeg>();
        var overviewPolyline = string.Empty;
        double totalDistance = 0;
        double totalDuration = 0;

        if (response.TryGetProperty("routes", out var routes) && routes.GetArrayLength() > 0)
        {
            var route = routes[0];

            if (route.TryGetProperty("overview_polyline", out var polyline))
            {
                overviewPolyline = polyline.GetProperty("points").GetString() ?? string.Empty;
            }

            if (route.TryGetProperty("legs", out var legsElement))
            {
                foreach (var leg in legsElement.EnumerateArray())
                {
                    var legDistance = leg.GetProperty("distance").GetProperty("value").GetDouble();
                    var legDuration = leg.GetProperty("duration").GetProperty("value").GetDouble();
                    totalDistance += legDistance;
                    totalDuration += legDuration;

                    var startAddress = leg.GetProperty("start_address").GetString() ?? string.Empty;
                    var endAddress = leg.GetProperty("end_address").GetString() ?? string.Empty;
                    var startLoc = new Location(
                        leg.GetProperty("start_location").GetProperty("lat").GetDouble(),
                        leg.GetProperty("start_location").GetProperty("lng").GetDouble());
                    var endLoc = new Location(
                        leg.GetProperty("end_location").GetProperty("lat").GetDouble(),
                        leg.GetProperty("end_location").GetProperty("lng").GetDouble());

                    var steps = new List<DirectionStep>();
                    if (leg.TryGetProperty("steps", out var stepsElement))
                    {
                        foreach (var step in stepsElement.EnumerateArray())
                        {
                            steps.Add(new DirectionStep(
                                step.GetProperty("html_instructions").GetString() ?? string.Empty,
                                step.GetProperty("distance").GetProperty("value").GetDouble(),
                                step.GetProperty("duration").GetProperty("value").GetDouble(),
                                new Location(
                                    step.GetProperty("start_location").GetProperty("lat").GetDouble(),
                                    step.GetProperty("start_location").GetProperty("lng").GetDouble()),
                                new Location(
                                    step.GetProperty("end_location").GetProperty("lat").GetDouble(),
                                    step.GetProperty("end_location").GetProperty("lng").GetDouble())
                            ));
                        }
                    }

                    legs.Add(new DirectionLeg(legDistance, legDuration, startAddress, endAddress, startLoc, endLoc, steps));
                }
            }
        }

        return new DirectionsResult(legs, overviewPolyline, totalDistance, totalDuration);
    }
}
