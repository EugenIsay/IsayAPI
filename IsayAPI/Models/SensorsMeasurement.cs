using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class SensorsMeasurement
{
    public int? SensorId { get; set; }

    public int? TypeId { get; set; }

    public string? MeasurementFormula { get; set; }

    public virtual Sensor? Sensor { get; set; }

    public virtual MeasurementsType? Type { get; set; }
}
