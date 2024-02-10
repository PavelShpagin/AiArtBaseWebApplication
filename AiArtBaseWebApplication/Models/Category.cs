using System;
using System.Collections.Generic;

namespace AiArtBaseWebApplication.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public byte[] Vector { get; set; } = null!;

    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();
}
