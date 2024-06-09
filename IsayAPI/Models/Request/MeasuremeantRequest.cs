using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class MeasuremeantRequest
{
    public int? SensorInventoryNumber { get; set; }

    public decimal? MeasurementValue { get; set; }

    public DateTime? MeasurementTs { get; set; }

    public int? MeasurementType { get; set; }
}
