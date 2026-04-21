using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Models;

public partial class DomicileVerification
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter DomicileOption")]
    public string? DomicileOption { get; set; }
    [Required(ErrorMessage = "Please Enter RationOrcetNumber")]
    public string? RationOrcetNumber { get; set; }

    [Required(ErrorMessage = "Please Enter NameAsPerRtionOrcet")]
    public string? NameAsPerRtionOrcet { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
