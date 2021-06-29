using BenimProjem.BL.Business.Abstracts;
using BenimProjem.Entities.Authentications;
using BenimProjem.Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business
{
    public class SignInManagerBusiness : ISignInManagerBusiness
    {
        SignInManager<AppUser> _signInManager;
        UserManager<AppUser> _userManager;
        public SignInManagerBusiness(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<bool> SignInAsync(AppUserGirisYapVM model)
        {
            SignInResult result = null;
            AppUser user = await _userManager.FindByNameAsync(model.UsernameorEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(model.UsernameorEmail);
            if (user != null)
                result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            return result == null ? false : result.Succeeded;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
