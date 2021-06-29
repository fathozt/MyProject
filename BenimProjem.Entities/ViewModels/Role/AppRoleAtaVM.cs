using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.Entities.ViewModels.Role
{
    public class AppRoleAtaVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasAssigned { get; set; }
    }
}
