using gentela_Alela.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Views.View_Model
{
    public class PersonalDetailsVM
    {
        public PersonalDetail personalReg { get; set; } = new();
            
        public IEnumerable<SelectListItem> RoleList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> CountryList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> StateList { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> DistrictList { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(300, ErrorMessage = "Invalid Email")]
        public string? Emaill { get; set; }
        [Required(ErrorMessage = "Password Should be Requried")]
        public string? Passwordd { get; set; }


        // Display-only names
        public string? CountryName { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }
    }
}
