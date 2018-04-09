using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using UrbanSpork.Common;
using UrbanSpork.Common.DataTransferObjects;
using UrbanSpork.Common.DataTransferObjects.Permission;
using UrbanSpork.Common.DataTransferObjects.Projection;
using UrbanSpork.Common.DataTransferObjects.User;
using UrbanSpork.DataAccess;
using UrbanSpork.DataAccess.Projections;

namespace UrbanSpork.API
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<UserAggregate, UserDTO>();
            CreateMap<UserDTO, UserAggregate>();
            CreateMap<UserDetailProjection, UserDetailProjectionDTO>()
                .ForMember(dest => dest.PermissionList,
                    opt => opt.MapFrom(src =>
                        JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                            src.PermissionList)))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<UserDetailProjection, LoginUserDTO>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<UserDetailProjection, OffBoardUserDTO>()
                .ForMember(dest => dest.PermissionList,
                    opt => opt.MapFrom(src =>
                        JsonConvert.DeserializeObject<Dictionary<Guid, DetailedUserPermissionInfo>>(
                            src.PermissionList)));
            CreateMap<UserAggregate, UpdateUserInformationDTO>()
                .ForMember(dest => dest.ForID, opt => opt.MapFrom(src => src.Id));
            CreateMap<PermissionAggregate, PermissionDTO>();
            CreateMap<PermissionDTO, PermissionAggregate>();
            CreateMap<PermissionDTO, PermissionDetailProjection>();
            CreateMap<PermissionDetailProjection, PermissionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PermissionId));
            CreateMap<UserManagementProjection, UserManagementDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId));
            CreateMap<SystemActivityProjection, SystemActivityDTO>();
        }
    }
}
