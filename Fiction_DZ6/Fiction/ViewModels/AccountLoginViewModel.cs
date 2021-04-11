using System.ComponentModel.DataAnnotations;

namespace Fiction_DZ6.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
