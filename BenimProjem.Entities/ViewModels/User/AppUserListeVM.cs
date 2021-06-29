using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities.ViewModels.User
{
    public class AppUserListeVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<string> Roller { get; set; }
    }
}
