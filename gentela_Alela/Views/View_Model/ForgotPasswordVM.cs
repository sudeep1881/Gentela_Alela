using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Views.View_Model
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Email  is Mandatory")]
        [EmailAddress(ErrorMessage = "Enter Valid Email")]
        public string? Email { get; set; }
    }
}
