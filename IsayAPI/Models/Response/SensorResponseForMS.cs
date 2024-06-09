namespace IsayAPI.Models.Response
{
    public class SensorResponseForMS
    {
        public int sensor_id { get; set; }
        public string sensor_name { get; set; }
        public int SensorInventoryNumber { get; set; }

        public DateTime? AddedTs { get; set; }

        public DateTime? RemovedTs { get; set; }

    }
}