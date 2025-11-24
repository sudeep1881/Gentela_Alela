using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class District
{
    public int Id { get; set; }

    public string? DistrictName { get; set; }

    public int? StateId { get; set; }

    public virtual ICollection<PersonalDetail> PersonalDetails { get; set; } = new List<PersonalDetail>();

    public virtual State? State { get; set; }

    public virtual ICollection<Taluk> Taluks { get; set; } = new List<Taluk>();

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
