using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class State
{
    public int Id { get; set; }

    public string? StateName { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();
}
