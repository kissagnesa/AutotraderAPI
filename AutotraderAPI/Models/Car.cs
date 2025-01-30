using System;
using System.Collections.Generic;

namespace AutotraderAPI.Models;

public partial class Car
{
    public Guid Id { get; set; }

    public string? Brand { get; set; }

    public string? Type { get; set; }

    public string? Color { get; set; }

    public DateTime? Myear { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }
}
