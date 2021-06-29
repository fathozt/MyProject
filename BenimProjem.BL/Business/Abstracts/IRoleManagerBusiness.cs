using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IRoleManagerBusiness
    {
        List<AppRoleListeVM> RolleriGetir();
        Task<bool> RoleEkleAsync(AppRoleEkleVM model);
        Task<(bool,AppRole)> RoleSilAsync(int Id);
        Task<AppRole> RolGetir(int Id);
    }
}
