using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class Sensor
{
    public int SensorId { get; set; }

    public string? SensorName { get; set; }

    public virtual ICollection<MeteostationsSensor> MeteostationsSensors { get; set; } = new List<MeteostationsSensor>();
}
