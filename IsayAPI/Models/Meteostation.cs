using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class Meteostation
{
    public int StationId { get; set; }

    public string? StationName { get; set; }

    public decimal? StationLongitude { get; set; }

    public decimal? StationLatitude { get; set; }

    public virtual ICollection<MeteostationsSensor> MeteostationsSensors { get; set; } = new List<MeteostationsSensor>();
}
