using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Models;

public partial class CommunicationDetail
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Mobile no is mandatory")]
    public string? MobileNo { get; set; }

    [Required(ErrorMessage ="Please Enter Email")]
    public string? Email { get; set; }


    public bool? Isdeleted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
