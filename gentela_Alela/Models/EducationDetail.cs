using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class EducationDetail
{
    public int Id { get; set; }

    public string? CourseType { get; set; }

    public string? Level { get; set; }

    public int? YearOfPassout { get; set; }

    public string? UniversityName { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? CandidateName { get; set; }

    public string? CollegeName { get; set; }

    public DateOnly? ResultDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Isdelted { get; set; }

    public virtual ICollection<YuvanidhiApplicantDetail> YuvanidhiApplicantDetails { get; set; } = new List<YuvanidhiApplicantDetail>();
}
