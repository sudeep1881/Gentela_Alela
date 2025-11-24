using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class YuvanidhiApplicantDetail
{
    public int Id { get; set; }

    public string? AdharName { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? Photo { get; set; }

    public string? PernmentAdress { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public int? DistinctId { get; set; }

    public int? TalukId { get; set; }

    public string? Pincode { get; set; }

    public bool? Isdeleted { get; set; }

    public int? EducationId { get; set; }

    public int? DomicineId { get; set; }

    public int? CommunicationId { get; set; }

    public int? AdditionalId { get; set; }

    public virtual AddtionalDetail? Additional { get; set; }

    public virtual CommunicationDetail? Communication { get; set; }

    public virtual District? Distinct { get; set; }

    public virtual DomicileVerification? Domicine { get; set; }

    public virtual EducationDetail? Education { get; set; }

    public virtual Taluk? Taluk { get; set; }
}
