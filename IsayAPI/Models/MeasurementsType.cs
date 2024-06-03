using System;
using System.Collections.Generic;

namespace IsayAPI.Models;

public partial class MeasurementsType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public string? TypeUnits { get; set; }
}
