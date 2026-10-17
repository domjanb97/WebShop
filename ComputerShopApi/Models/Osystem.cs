using System;
using System.Collections.Generic;

namespace ComputerShopApi.Models;

public partial class Osystem
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public int Version { get; set; }

    public DateTime RegisterTime { get; set; }

    public DateTime UpdateTime { get; set; }
}
