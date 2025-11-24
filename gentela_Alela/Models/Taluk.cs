using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class Taluk
{
    public int Id { get; set; }

    public string? TalukName { get; set; }

    public int? DistictId { get; set; }

    public virtual District? Distict { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
