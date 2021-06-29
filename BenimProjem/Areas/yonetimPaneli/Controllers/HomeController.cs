using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.Areas.yonetimPaneli.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BenimProjem.Areas.yonetimPaneli.Controllers
{
    public class HomeController : BaseController
    {
        [Area("yonetimPaneli")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
