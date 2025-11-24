using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class LoginDetail
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? Isdeleted { get; set; }
}
