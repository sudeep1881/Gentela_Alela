using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Models;

public partial class AddtionalDetail
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Enter your caste")]
    public string? Caste { get; set; }

    public string? Disability { get; set; }

    public string? Upidno { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
