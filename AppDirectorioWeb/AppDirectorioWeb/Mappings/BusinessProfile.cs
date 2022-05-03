using AutoMapper;
using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Mappings
{
    public class BusinessProfile:Profile
    {
        public BusinessProfile()
        {
            CreateMap<BussinesViewModel, Negocio>();
            CreateMap<FeatureNegocioViewModel, FeatureNegocio>();
            CreateMap<HorarioNegocioViewModel, HorarioNegocio>();
            CreateMap<ImagenesNegocioViewModel, ImagenesNegocio>();
        }
           
    }
}
