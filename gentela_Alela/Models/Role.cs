using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();
}
