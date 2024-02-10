using System;
using System.Collections.Generic;

namespace AiArtBaseWebApplication.Models;

public partial class Art
{
    public long Id { get; set; }

    public byte[] Image { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public bool Premium { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<User> UsersNavigation { get; set; } = new List<User>();
}
