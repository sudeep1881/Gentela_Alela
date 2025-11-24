using System.ComponentModel.DataAnnotations;

namespace gentela_Alela.Views.View_Model
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Email is Required")]
        [MaxLength(300,ErrorMessage ="Invalid Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage ="Password Should be Requried")]
        public string? Password { get; set; }
    }
}
