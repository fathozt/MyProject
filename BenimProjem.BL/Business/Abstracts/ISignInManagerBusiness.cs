using BenimProjem.Entities.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface ISignInManagerBusiness
    {
        Task<bool> SignInAsync(AppUserGirisYapVM model);
        Task SignOutAsync();
    }
}
