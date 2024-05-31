namespace IsayAPI.Models;

public class Meteostation
{
    public int station_id { get; set; }
    public string station_name { get; set; }
    public decimal station_long { get; set; }
    public decimal station_lat { get; set; }
}