using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business
{
    public class RoleManagerBusiness : IRoleManagerBusiness
    {
        RoleManager<AppRole> _roleManager;

        public RoleManagerBusiness(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> RoleEkleAsync(AppRoleEkleVM model)
        {
            AppRole role = new AppRole
            {
                Name = model.Name
            };
            IdentityResult result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<(bool,AppRole)> RoleSilAsync(int Id)
        {
            AppRole role = await RolGetir(Id);
            IdentityResult result = await _roleManager.DeleteAsync(role);
            return (result.Succeeded, role);
        }

        public async Task<AppRole> RolGetir(int Id)
        {
            AppRole role = await _roleManager.FindByIdAsync(Id.ToString());
            return role;
        }

        public List<AppRoleListeVM> RolleriGetir()
        {
            return _roleManager.Roles.Select(r => new AppRoleListeVM
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }
    }
}
