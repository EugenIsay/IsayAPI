namespace IsayAPI.Models;

public class Measurement_Type
{
    public int type_id { get; set; }
    public string type_name { get; set; }
    public string type_units { get; set; }
    public List<Sensor> sensors { get; set; }
    public List<Sensor_Measurement> measurement { get; set; }
}