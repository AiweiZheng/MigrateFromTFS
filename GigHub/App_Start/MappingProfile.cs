using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //domain to DTO
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
            Mapper.CreateMap<IdentityRole, RoleDto>();

            //DTO to domain
            Mapper.CreateMap<UserDto, ApplicationUser>();
        }
    }
}