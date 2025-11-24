using gentela_Alela.Models;
using System.Numerics;

namespace gentela_Alela.DTOs
{
    public class PersonalDetailsDTOs
    {
        public int IdDTOs { get; set; }

        public string? FullNameDTOs { get; set; }

        public string? GenderDTOs { get; set; }

        public DateOnly? DobDTOs { get; set; }

        public string? EmailDTOs { get; set; }

        public string? PasswordDTOs { get; set; }

        public string? CountryDTOs { get; set; }

        public string? StateDTOs { get; set; }

        public string? DistrictDTOs { get; set; }

        public string? ProfileImageDTOs { get; set; }

        public string? RoleNameDTOs { get; set; }

        public string? PhoneNumber { get; set; }

        public virtual Role? Role { get; set; }
    }
}
