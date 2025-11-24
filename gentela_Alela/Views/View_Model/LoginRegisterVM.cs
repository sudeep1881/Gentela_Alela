using gentela_Alela.Models;
using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Views.View_Model
{
    public class LoginRegisterVM
    {
        public LoginDetail loginRegDetails { get; set; } = new();


        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(300, ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password Should be Requried")]
        public string? Password { get; set; }
    }
}
