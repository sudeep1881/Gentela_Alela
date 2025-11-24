using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class DomicileVerification
{
    public int Id { get; set; }

    public string? DomicileOption { get; set; }

    public string? RationOrcetNumber { get; set; }

    public string? NameAsPerRtionOrcet { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
