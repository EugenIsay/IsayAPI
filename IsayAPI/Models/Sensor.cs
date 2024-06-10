using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IsayAPI.Models;

public partial class Sensor
{   [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SensorId { get; set; }
    public string? SensorName { get; set; }
}
