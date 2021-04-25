using System.ComponentModel.DataAnnotations;

namespace WebSocketsExample.ViewModels
{
    public class UserJoinChatViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
