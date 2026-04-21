using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Models;

public partial class EducationDetail
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Please Enter  CourseType")]
    public string? CourseType { get; set; }
    [Required(ErrorMessage ="Please Enter Level")]
    public string? Level { get; set; }

    [Required(ErrorMessage ="Please Enter your yearof passout")]
    public int? YearOfPassout { get; set; }

    [Required(ErrorMessage ="Please Enter your university ")]
    public string? UniversityName { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? CandidateName { get; set; }

    public string? CollegeName { get; set; }

    public DateOnly? ResultDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Isdelted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
