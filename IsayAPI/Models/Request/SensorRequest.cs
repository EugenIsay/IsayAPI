namespace IsayAPI.Models.Request
{
    public class SensorRequest
    {
        public string sensor_name { get; set; }
        public List<SensorMeasurementsRequest> measurements { get; set; } = null;
    }
}
