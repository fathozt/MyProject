using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BenimProjem.Areas.yonetimPaneli.Controllers.Base
{
    [Area("yonetimPaneli")]
    public class BaseController : Controller
    {
        [NonAction]
        public void MesajYaz(bool basariDurumu, string basariliMesaj, string basarisizMesaj)
        {
            TempData["BasariDurumu"] = basariDurumu;
            if (basariDurumu)
                TempData["Mesaj"] = basariliMesaj;
            else
                TempData["Mesaj"] = basarisizMesaj;
        }
    }
}
