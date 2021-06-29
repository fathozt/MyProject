using BenimProjem.Entities.ViewModels.Marka;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenimProjem.BL.Business.Abstracts
{
    public interface IMarkaBusiness
    {
        List<MarkaListeVM> Listele();
    }
}
