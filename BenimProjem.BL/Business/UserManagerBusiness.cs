using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Services;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.Role;
using BenimProjem.Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BenimProjem.BL.Business
{
    public class UserManagerBusiness : IUserManagerBusiness
    {
        UserManager<AppUser> _userManager;
        RoleManager<AppRole> _roleManager;
        public UserManagerBusiness(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public List<AppUserListeVM> AllUsers()
        {
            return _userManager.Users.ToList().Select(u => new AppUserListeVM
            {
                Id = u.Id,
                Email = u.Email,
                Username = u.UserName,
                Roller = (List<string>)_userManager.GetRolesAsync(u).Result
            }).ToList();
        }

        public async Task<bool> CreateAsync(AppUserKayitOlVM model)
        {
            IdentityResult result = await _userManager.CreateAsync(new AppUser
            {
                Email = model.Email,
                UserName = model.Username
            }, model.Password);

            return result.Succeeded;
        }

        public async Task<(bool, string)> DeleteUser(int Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id.ToString());
            IdentityResult result = await _userManager.DeleteAsync(user);
            return (result.Succeeded, user.UserName);
        }

        public async Task<AppUser> KullaniciGetir(int Id)
        {
            return await _userManager.FindByIdAsync(Id.ToString());
        }

        public async Task<(bool, bool)> MailGonder(string userEmail)
        {
            AppUser user = await _userManager.FindByEmailAsync(userEmail);

            if (user != null)
            {
                string token = await _userManager.GeneratePasswordResetTokenAsync(user);

                byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(token);
                string codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                string body = "Sayın" + user.UserName + "<br><br>";
                body += "Şifre yenilemek için lütfen <a href=\"https://localhost:5001/User/SifreGuncelle?userId=" + user.Id + "&token=" + codeEncoded + "\">tıklayınız.</a>";

                bool mailSonuc = MailService.SendMail(user.Email, "Şifre Talebi", body);
                return (true, mailSonuc);
            }
            return (false, false);
        }

        public async Task<(bool,string)> RolAtaveSil(int userId, List<AppRoleAtaVM> model)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            try
            {
                foreach (var role in model)
                {
                    if (role.HasAssigned)
                        await _userManager.AddToRoleAsync(user, role.Name);
                    else
                        await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                return (true, user.UserName);
            }
            catch (Exception ex)
            {

                return (false, user.UserName);
            }
        }

        public async Task<bool> SifreGuncelle(int userId, string token, AppUserSifreGuncelleVM model)
        {
            byte[] byteToken = WebEncoders.Base64UrlDecode(token);
            string stringToken = Encoding.UTF8.GetString(byteToken);

            AppUser user = await KullaniciGetir(userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, stringToken, model.Password);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);

            return result.Succeeded;
        }

        public async Task<List<AppRoleAtaVM>> TumRolleriGetir(int userId)
        {
            List<AppRoleAtaVM> atanmisRoller = new List<AppRoleAtaVM>();

            AppUser user = await KullaniciGetir(userId);
            List<string> roller = (List<string>)await _userManager.GetRolesAsync(user);
            List<AppRole> tumRoller = _roleManager.Roles.ToList();

            foreach (var rol in tumRoller)
            {
                atanmisRoller.Add(new AppRoleAtaVM
                {
                    Id = rol.Id,
                    Name = rol.Name,
                    HasAssigned = roller.Contains(rol.Name)
                });
            }
            return atanmisRoller;
        }
    }
}
