using AutoMapper;
using ProjectManager.WebAPI.Models;
using ProjectManager.WebAPI.Requests.Projects;
using ProjectManager.WebAPI.Requests.Users;

namespace ProjectManager.WebAPI.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<EditUserRequest, User>();
        CreateMap<AuthenticateUserRequest, User>();

        CreateMap<CreateProjectRequest, Project>();
    }
}