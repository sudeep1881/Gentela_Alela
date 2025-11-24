using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class CommunicationDetail
{
    public int Id { get; set; }

    public string? MobileNo { get; set; }

    public string? Email { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
