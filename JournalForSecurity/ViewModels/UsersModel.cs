using JournalForSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalForSecurity.ViewModels
{
    public class UsersModel
    {
        public List<User> Users { get; set; }
        public string dUserId { get; set; }
        public UsersModel()
        {
            Users = new List<User>();
        }
    }
}
