using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BenimProjem.BL.Business.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BenimProjem.Controllers
{
    public class HomeController : Controller
    {
        IKategoriBusiness _kategoriBusiness;
        public HomeController(IKategoriBusiness kategoriBusiness)
        {
            _kategoriBusiness = kategoriBusiness;
        }
        public IActionResult Index()
        {
            ViewBag.Kategoriler = _kategoriBusiness.Listele();
            return View();
        }
    }
}
