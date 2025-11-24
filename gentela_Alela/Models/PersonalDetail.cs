using System;
using System.Collections.Generic;

namespace gentela_Alela.Models;

public partial class PersonalDetail
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? PhoneNo { get; set; }

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public int? DistrictId { get; set; }

    public string? ProfileImage { get; set; }

    public int? RoleId { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual Country? Country { get; set; }

    public virtual District? District { get; set; }

    public virtual Role? Role { get; set; }

    public virtual State? State { get; set; }
}
