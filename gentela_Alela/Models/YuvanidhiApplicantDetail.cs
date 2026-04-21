using System;
using System.ComponentModel.DataAnnotations;


namespace gentela_Alela.Models;

public partial class YuvanidhiApplicantDetail
{
    public int Id { get; set; }
    [Required(ErrorMessage ="AdharName is mandatory")]
    public string? AdharName { get; set; }

    [Required(ErrorMessage ="Enter date of brith")]
    public DateOnly? Dob { get; set; }

    [Required(ErrorMessage ="Enter gender")]
    public string? Gender { get; set; }

    [Required(ErrorMessage ="Photo should be mandatory  ")]
    public string? Photo { get; set; }

    [Required(ErrorMessage ="Please Enter PernmentAddress")]
    public string? PernmentAdress { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }
    [Required(ErrorMessage ="Enter your district")]
    public int? DistinctId { get; set; }
    [Required(ErrorMessage ="Enter your taluk")]
    public int? TalukId { get; set; }
    [Required(ErrorMessage ="Enter Pincode")]
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
