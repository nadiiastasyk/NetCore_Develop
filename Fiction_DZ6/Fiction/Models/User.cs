using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiction_DZ6.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
