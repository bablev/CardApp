using AutoMapper;
using CardApp.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Contracts
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CardDTO, Card>().ReverseMap();
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<RegistrationModel, AppUser>().ReverseMap();
        }
    }
}
