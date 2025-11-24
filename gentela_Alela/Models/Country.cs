using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? CountryName { get; set; }

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
