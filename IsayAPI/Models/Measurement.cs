using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class Measurement
{
    public int? SensorInventoryNumber { get; set; }

    public decimal? MeasurementValue { get; set; }

    public DateTime? MeasurementTs { get; set; }

    public int? MeasurementType { get; set; }

    public virtual MeasurementsType? MeasurementTypeNavigation { get; set; }

    public virtual MeteostationsSensor? SensorInventoryNumberNavigation { get; set; }
}
