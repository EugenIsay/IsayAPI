using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsayAPI.Models
{
    public class Sensor
    {
        public string sensor_name { get; set; }
        public int sensor_id { get; set; }
        public List<Measurement_Type> measurement_types { get; set; }
        public List<Sensor_Measurement> sensor_Measurements { get; set; }

    }
}
