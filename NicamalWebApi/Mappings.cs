﻿using AutoMapper;
using NicamalWebApi.Models;
using NicamalWebApi.Models.ViewModels;

namespace NicamalWebApi
{
    public class Mappings: Profile
    {
        public Mappings()
        {
            CreateMap<User, UserShelterPatch>().ReverseMap();
            CreateMap<User, UserShelterUpdate>().ReverseMap();
            CreateMap<User, UserShelterDetail>().ReverseMap();
            CreateMap<User, UserShelterRegister>().ReverseMap();
            CreateMap<UserShelter, UserShelterDetail>().ReverseMap();
            CreateMap<UserShelter, UserShelterList>().ReverseMap();
            CreateMap<User, UserShelter>()
                .ForMember(a => a.Publications,
                    b => b.MapFrom(o => o.Publications));
            CreateMap<User, UserShelterDetail>().ReverseMap();
            
            CreateMap<Provinces, ProvincesResponse>();
            
            CreateMap<Disappearance, DisappearanceDetail>().ReverseMap();
            CreateMap<Disappearance, DisappearanceListResponse>().ReverseMap();
            CreateMap<Disappearance, DisappearanceCreate>().ReverseMap();    
            
            CreateMap<User, UserForPublication>();
            CreateMap<User, UserResponseWhenLoggedIn>();
            CreateMap<UserResponseWhenLoggedIn, User>();
            CreateMap<UserRegister, User>();
            CreateMap<User, UserRegister>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserForPublicationDetail>();
            CreateMap<UserForPublicationDetail, User>();
            
            CreateMap<Publication, PublicationsList>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            CreateMap<Publication, PublicationForReport>();
            CreateMap<PublicationCreate, Publication>().ForMember(a => a.Image,
                options => options.Ignore());
            CreateMap<Publication, PublicationDetail>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            CreateMap<PublicationDetail, Publication>().ForMember(a => a.User,
                b => b.MapFrom(o => o.User));
            CreateMap<Publication, PublicationCount>().ReverseMap();
            
            CreateMap<Report, ReportList>()
                .ForMember(a => a.Publication,
                    b => b.MapFrom(o => o.Publication));
            
            CreateMap<Report, ReportDetail>()
                .ForMember(a => a.Publication,
                    b => b.MapFrom(o => o.Publication))
                .ForMember(a => a.UserReported,
                    b => b.MapFrom(o => o.ReportedUser))
                .ForMember(a => a.User,
                    b => b.MapFrom(o => o.User));

            CreateMap<Report, ReportCreate>().ReverseMap();
        }
    }
}