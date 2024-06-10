namespace IsayAPI.Models.Request;

public class MeteostationRequest
{
    public string? StationName { get; set; }

    public decimal? StationLongitude { get; set; }

    public decimal? StationLatitude { get; set; }
}