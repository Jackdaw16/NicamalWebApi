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
            CreateMap<User, UserResponse>();
            CreateMap<User, UserForPublicationDetail>();
            CreateMap<UserForPublicationDetail, User>();
            
            CreateMap<Publication, PublicationsResponseForList>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            CreateMap<Publication, PublicationForReport>();
            CreateMap<PublicationCreate, Publication>().ForMember(a => a.Image,
                options => options.Ignore());
            CreateMap<Publication, PublicationDetail>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            CreateMap<PublicationDetail, Publication>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            
            CreateMap<Report, ReportResponse>()
                .ForMember(a => a.Publication,
                    b => b.MapFrom(o => o.Publication))
                .ForMember(a => a.UserReported,
                    b => b.MapFrom(o => o.ReportedUser))
                .ForMember(a => a.User,
                    b => b.MapFrom(o => o.User));
        }
    }
}