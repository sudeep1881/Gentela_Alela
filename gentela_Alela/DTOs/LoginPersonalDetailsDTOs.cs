using gentela_Alela.Models;

namespace gentela_Alela.DTOs
{
    public class LoginPersonalDetailsDTOs
    {
        public int Id { get; set; }

        public string? FullName { get; set; }

        public string? Gender { get; set; }

        public DateOnly? Dob { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? PhoneNo { get; set; }

        public string? CountryId { get; set; }

        public string? StateId { get; set; }

        public string? DistrictId { get; set; }

        public string? ProfileImage { get; set; }

        public int? RoleId { get; set; }
        public virtual Country? Country { get; set; }

        public virtual District? District { get; set; }

        public virtual Role? Role { get; set; }

        public virtual State? State { get; set; }
    }
}
