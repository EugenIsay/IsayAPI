namespace IsayAPI;

public class Measurement
{
    public int sensor_inventory_number { get; set; }
    public decimal measurement_value { get; set; }
    public DateTime measurement_ts { get; set; }
    public int measurement_type { get; set; }
}