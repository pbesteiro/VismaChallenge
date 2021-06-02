using AutoMapper;
using System.Linq;
using VismaUserCore.DTOs;
using VismaUserCore.Entities;
using VismaUserCore.Requests;
using VismaUserCore.Responses;

namespace VismaUserInsfrestucture.Mapping
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            
            CreateMap<Users, UpdateUserRequest>().ReverseMap();
            CreateMap<Users, AddUserRequest>().ReverseMap();
            CreateMap<UserDTO, Users>();//.ForMember(u => u.RolsUser, opt => opt.MapFrom(x => x.Rols.Select(r => r.)));
            CreateMap<Users, UserDTO>().ForMember(u => u.Rols, opt => opt.MapFrom(x => x.RolsUser.Select(r => r.Rol)));
            CreateMap<Users, UserResponse>().ForMember(u => u.Rols, opt => opt.MapFrom(x => x.RolsUser.Select(r => r.Rol)));
            CreateMap<RolDTO, Rols>();
            CreateMap<CreateRolRequest, Rols>().ReverseMap();
            CreateMap<UpdateRolRequest, Rols>().ReverseMap();
            CreateMap<Rols, RolDTO>().ForMember(u => u.Permissions, opt => opt.MapFrom(x => x.RolPermissions.Select(r => r.Permission))); 
            CreateMap<Departments, DepartmentDTO>().ReverseMap();
            CreateMap<Permissions, PermissionDTO>().ReverseMap();
        }
    }
}
