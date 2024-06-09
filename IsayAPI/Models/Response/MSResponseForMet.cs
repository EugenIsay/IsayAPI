namespace IsayAPI.Models.Response
{
    public class MSResponseForMet
    {
        public int SensorInventoryNumber { get; set; }

        public DateTime? AddedTs { get; set; }

        public DateTime? RemovedTs { get; set; }
        public int SensorId { get; set; }
        public string SensorName { get; set; }


    }
}