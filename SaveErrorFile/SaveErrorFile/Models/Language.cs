using System;
using System.Collections.Generic;

namespace SaveErrorFile.Models;

public partial class Language
{
    public int Id { get; set; }

    public string? Language1 { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
