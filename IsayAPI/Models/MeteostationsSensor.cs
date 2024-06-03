using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class MeteostationsSensor
{
    public int SensorInventoryNumber { get; set; }

    public int? StationId { get; set; }

    public int? SensorId { get; set; }

    public DateTime? AddedTs { get; set; }

    public DateTime? RemovedTs { get; set; }

    public virtual Sensor? Sensor { get; set; }

    public virtual Meteostation? Station { get; set; }
}
