namespace IsayAPI.Models.Response
{
    public class MeteostationsSensorResponse
    {

        public int StationId { get; set; }
        public string StationName { get; set; }

        public decimal? StationLongitude { get; set; }

        public decimal? StationLatitude { get; set; }
        public List<SensorResponseForMS> sensors { get; set;}

    }
}