using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? ProfileImage { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsDeleted { get; set; }
}
