using System;
using System.Collections.Generic;

namespace AiArtBaseWebApplication.Models;

public partial class User
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public byte[]? AvatarImage { get; set; }

    public string? Description { get; set; }

    public string Username { get; set; } = null!;

    public bool Hidden { get; set; }

    public bool Premium { get; set; }

    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();

    public virtual ICollection<Art> ArtsNavigation { get; set; } = new List<Art>();

    public virtual ICollection<User> Followers { get; set; } = new List<User>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
