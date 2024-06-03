namespace IsayAPI.Models.Response
{
    public class SensorResponse
    {
        public int sensor_id { get; set; }
        public string sensor_name { get; set; }

        public List<MeasurementsType> measurements { get; set; }
       
    }
}
