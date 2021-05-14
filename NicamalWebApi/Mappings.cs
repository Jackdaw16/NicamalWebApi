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
            CreateMap<Publication, PublicationResponse>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));

        }
    }
}