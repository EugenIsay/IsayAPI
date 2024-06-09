namespace IsayAPI.Models.Response
{
    public class MeteostationResponse
    {
        public int StationId { get; set; }

        public string? StationName { get; set; }

        public decimal? StationLongitude { get; set; }

        public decimal? StationLatitude { get; set; }
        public List<MSResponseForMet> meteostationsSensors { get; set; } = null;


    }
}