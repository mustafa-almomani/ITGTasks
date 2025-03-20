using System;
using System.Collections.Generic;

namespace SaveErrorFile.Models;

public partial class ErrorLog
{
    public int Id { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public string? Source { get; set; }

    public DateTime? LoggedAt { get; set; }
}
