using System;
using System.Collections.Generic;

namespace Entity_FrameWork.Models;

public partial class Item
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
