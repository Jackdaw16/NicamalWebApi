using AutoMapper;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;

namespace NicamalWebApi
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<User, UserForPublication>();
            CreateMap<User, UserResponseWhenLoggedIn>();
            CreateMap<UserResponseWhenLoggedIn, User>();
            CreateMap<UserRegister, User>();
            CreateMap<User, UserRegister>();
            
            CreateMap<Publication, PublicationsResponseForList>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            
        }
    }
}