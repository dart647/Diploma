using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        //public string RoleId { get; set; }
        //public IdentityRole Role { get; set; }

    }
}
