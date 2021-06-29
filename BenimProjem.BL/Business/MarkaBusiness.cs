using BenimProjem.BL.Business.Abstracts;
using BenimProjem.BL.Repositories.Abstracts;
using BenimProjem.BL.Repositories.Concretes;
using BenimProjem.Entities.ViewModels.Marka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenimProjem.BL.Business
{
    public class MarkaBusiness : IMarkaBusiness
    {
        IMarkaRepository _markaRepository;

        public MarkaBusiness()
        {
            _markaRepository = new MarkaRepository();
        }

        public List<MarkaListeVM> Listele()
        {
            return _markaRepository.Get().Select(m => new MarkaListeVM
            {
                Id = m.Id,
                MarkaAdi = m.MarkaAdi
            }).ToList();
        }
    }
}
