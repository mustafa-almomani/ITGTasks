using System;
using System.Collections.Generic;

namespace SaveErrorFile.Models;

public partial class User
{
    public int Id { get; set; }

    public string? ClientName { get; set; }

    public string? UserName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Birthday { get; set; }

    public int? Usertype { get; set; }

    public string? Language { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Img { get; set; }

    public int? LangId { get; set; }

    public int? NationalId { get; set; }

    public virtual Language? Lang { get; set; }
}
