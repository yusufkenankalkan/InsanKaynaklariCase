using AutoMapper;
using CaseEL.Models;
using CaseEL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseEL.Mappings
{
    public class Maps : Profile
    {
        public Maps()
        {            
            CreateMap<AdayCv, AdayCvVM>().ReverseMap();
            CreateMap<Sicil, SicilVM>().ReverseMap();
            CreateMap<SicilOgrenim, SicilOgrenimVM>().ReverseMap();
            CreateMap<SicilUcret, SicilUcretVM>().ReverseMap();
        }
    }
}
