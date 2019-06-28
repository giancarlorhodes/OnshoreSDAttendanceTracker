using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace OnshoreSDAttendanceTrackerNet.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //user mappings
                cfg.CreateMap<IUserDO, IUserBO>();
                cfg.CreateMap<UserBO, UserPO>();
                cfg.CreateMap<UserDO, UserPO>();
                cfg.CreateMap<UserPO, UserDO>();
                cfg.CreateMap<IUserBO, IUserPO>();
                cfg.CreateMap<IUserDO, IUserPO>();
                cfg.CreateMap<UserPO, IUserDO>();
                cfg.CreateMap<List<IUserDO>,List<IUserBO>>();
                cfg.CreateMap<List<IUserDO>, List<IUserPO>>();
                cfg.CreateMap<List<IUserDO>, List<UserPO>>();


                //userCred mappings
                cfg.CreateMap<UserCredentialsBO,UserCredentialPO>();
                cfg.CreateMap<IUserCredentialsPO,IUserCredentialsDO>();
                cfg.CreateMap<IUserCredentialsDO,UserCredentialPO>();
                cfg.CreateMap<UserCredentialPO,IUserCredentialsDO>();
                cfg.CreateMap<List<IUserCredentialsDO>,List<IUserCredentialsBO>>();
                cfg.CreateMap<List<IUserCredentialsDO>,List<IUserCredentialsPO>>();

                //team mappings
                cfg.CreateMap<TeamBO,TeamPO>();
                cfg.CreateMap<TeamDO, TeamPO>();
                cfg.CreateMap<ITeamDO, ITeamBO>();
                cfg.CreateMap<ITeamDO, ITeamPO>();
                cfg.CreateMap<ITeamDO, TeamPO>();
                cfg.CreateMap<TeamPO, ITeamDO>();
                cfg.CreateMap<List<ITeamDO>,List<ITeamBO>>();
                cfg.CreateMap<List<ITeamDO>,List<TeamPO>>();
                cfg.CreateMap<List<TeamDO>, List<TeamPO>>();

                //absence mappings
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsencePO, IAbsenceDO>();
                cfg.CreateMap<List<IAbsenceDO>, List<IAbsenceBO>>();
                cfg.CreateMap<List<IAbsenceDO>, List<AbsencePO>>();

                //exception mappings
                cfg.CreateMap<ExceptionBO, ExceptionPO>();
                cfg.CreateMap<IExceptionDO, IExceptionBO>();
                cfg.CreateMap<IExceptionDO, ExceptionPO>();
                cfg.CreateMap<ExceptionPO, IExceptionDO>();
                cfg.CreateMap<List<IExceptionDO>,List<IExceptionBO>>();
                cfg.CreateMap<List<IExceptionDO>, List<IExceptionPO>>();

                //cfg.AddProfile(new UserProfile());
                //cfg.AddProfile(new PostProfile());
            });
        }
        // AutoMapper.Mapper.CreateMap<SourceClass, DestinationClass>();
        //AutoMapper.Mapper.CreateMap<Book, BookViewModel>()
        //    .ForMember(dest => dest.Author,
        //               opts => opts.MapFrom(src => src.Author.Name));

    

        //public class UserProfile : Profile
        //{
        //    protected override void Configure()
        //    {
        //        Mapper.CreateMap<User, UserViewModel>();
        //    }
        //}


        //AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap< UserBO, UserPO >());
        //    AutoMapper.Mapper.Instance
        // AutoMapper.Mapper.Configuration.

    }
}
