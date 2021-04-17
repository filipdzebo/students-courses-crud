using AutoMapper;
using Page_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Page_Backend
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, KorisnikViewModel>();
        }
    }
}
