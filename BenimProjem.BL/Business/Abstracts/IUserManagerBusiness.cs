using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using BenimProjem.Entities.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IUserManagerBusiness
    {
        Task<bool> CreateAsync(AppUserKayitOlVM model);
        List<AppUserListeVM> AllUsers();
        Task<(bool,string)> DeleteUser(int Id);
        Task<AppUser> KullaniciGetir(int Id);
        Task<(bool, bool)> MailGonder(string userEmail);
        Task<bool> SifreGuncelle(int userId, string token, AppUserSifreGuncelleVM model);
        Task<List<AppRoleAtaVM>> TumRolleriGetir(int userId);
        Task<(bool,string)> RolAtaveSil(int userId, List<AppRoleAtaVM> model);
    }
}
