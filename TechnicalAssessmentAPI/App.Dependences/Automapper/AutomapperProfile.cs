using App.Config.DTO;
using App.Infrastructure.Database.Entities;
using AutoMapper;

namespace App.Dependences.Automapper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Users, UsersDTO>().ReverseMap();
            CreateMap<Persons, PersonsDTO>().ReverseMap();
        }
        
    }
}
