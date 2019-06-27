using OnshoreSDAttendanceTrackerNet.Models;
using OnshoreSDAttendanceTrackerNetBLL.Models;
using OnshoreSDAttendanceTrackerNetDAL.Models;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnshoreSDAttendanceTrackerNetBLL.Interfaces;
using OnshoreSDAttendanceTrackerNet.Interfaces;
using OnshoreSDAttendanceTrackerNetDAL.Interfaces;

namespace OnshoreSDAttendanceTrackerNet.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                //user mappings
                cfg.CreateMap<IUserLoginBO, IUserLoginPO>();
                cfg.CreateMap<IUserLoginDO,IUserLoginBO > ();
                cfg.CreateMap<UserBO, UserPO>();
                cfg.CreateMap<IUserBO, IUserPO>();
                cfg.CreateMap<IUserDO, IUserPO>();
                cfg.CreateMap<UserPO, IUserDO>();
                cfg.CreateMap<List<IUserDO>,List<IUserBO>>();
                cfg.CreateMap<List<IUserDO>, List<IUserPO>>();

                //userCred mappings
                cfg.CreateMap<UserCredentialsBO,UserCredentialPO>();
                cfg.CreateMap<IUserCredentialsPO,IUserCredentialsDO>();
                cfg.CreateMap<IUserCredentialsDO,UserCredentialPO>();
                cfg.CreateMap<UserCredentialPO,IUserCredentialsDO>();
                cfg.CreateMap<List<IUserCredentialsDO>,List<IUserCredentialsBO>>();
                cfg.CreateMap<List<IUserCredentialsDO>,List<IUserCredentialsPO>>();

                //team mappings
                cfg.CreateMap<TeamBO,TeamPO>();
                cfg.CreateMap<ITeamDO, ITeamBO>();
                cfg.CreateMap<ITeamDO, TeamPO>();
                cfg.CreateMap<TeamPO, ITeamDO>();
                cfg.CreateMap<List<ITeamDO>,List<ITeamBO>>();
                cfg.CreateMap<List<ITeamDO>,List<TeamPO>>();

                //absence mappings
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsenceBO, AbsencePO>();
                cfg.CreateMap<AbsencePO, IAbsenceDO>();
                cfg.CreateMap<IAbsenceBO, DashboardViewModel>();
                cfg.CreateMap<AbsenceBO, DashboardViewModel>();
                cfg.CreateMap<IAbsencePO, DashboardViewModel>();
                cfg.CreateMap<AbsencePO, DashboardViewModel>();
                cfg.CreateMap<List<IAbsenceDO>, List<IAbsenceBO>>();
                cfg.CreateMap<List<IAbsenceDO>, List<AbsencePO>>();
                cfg.CreateMap<List<IAbsencePO>, List<DashboardViewModel>>();
                cfg.CreateMap<List<AbsencePO>, List<DashboardViewModel>>();
                cfg.CreateMap<List<IAbsenceBO>, List<DashboardViewModel>>();
                cfg.CreateMap<List<AbsenceBO>, List<DashboardViewModel>>();

                //exception mappings
                cfg.CreateMap<ExceptionBO, ExceptionPO>();
                cfg.CreateMap<IExceptionDO, IExceptionBO>();
                cfg.CreateMap<IExceptionDO, ExceptionPO>();
                cfg.CreateMap<ExceptionPO, IExceptionDO>();
                cfg.CreateMap<List<IExceptionDO>,List<IExceptionBO>>();
                cfg.CreateMap<List<IExceptionDO>, List<IExceptionPO>>();

                //Navigation mappings
                cfg.CreateMap<INavigationDO, INavigationBO>();
                cfg.CreateMap<INavigationDO, INavigationPO>();
                cfg.CreateMap<INavigationBO, INavigationPO>();
                cfg.CreateMap<List<INavigationDO>, List<INavigationBO>>();
                cfg.CreateMap<List<INavigationDO>, List<INavigationPO>>();
                cfg.CreateMap<List<INavigationBO>, List<INavigationPO>>();
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
