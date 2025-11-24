using gentela_Alela.Models;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Views.View_Model
{
    public class RegistrationVM
    {
        public Registration registrationReg { get; set; } = new();

     


        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(300, ErrorMessage = "Invalid Email")]
        public string? Emaill { get; set; }
        [Required(ErrorMessage = "Password Should be Requried")]
        public string? Passwordd { get; set; }
    }
}
