using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenimProjem.PL.Controllers.Base
{
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
