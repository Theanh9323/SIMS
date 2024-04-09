using System.ComponentModel.DataAnnotations;

namespace EcommerceMVC.ViewModel
{
    public class LoginVM
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage ="*")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
