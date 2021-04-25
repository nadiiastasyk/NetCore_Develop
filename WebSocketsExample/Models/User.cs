using System.ComponentModel.DataAnnotations;

namespace WebSocketsExample.Models
{
    public class User
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
