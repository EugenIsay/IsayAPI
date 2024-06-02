using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsayAPI.Models
{
    public class Sensor_Measurement
    {
        public int sensor_id { get; set; }
        public int type_id { get; set; }
        public string measurement_formula { get; set; }
        public Sensor Sensor { get; set; }
        public Measurement_Type measurement_type { get; set; }

    }
}
