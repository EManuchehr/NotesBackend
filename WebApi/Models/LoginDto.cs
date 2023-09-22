using AutoMapper;
using Application.Authenticate.Commands;
using Application.Common.Mappings;
using Application.Notes.Commands.CreateNote;

namespace WebApi.Models;

public class LoginDto : IMapWith<AuthenticateCommand>
{
    public string Username { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginDto, AuthenticateCommand>()
            .ForMember(command => command.Username, opt => 
                opt.MapFrom(dto => dto.Username))
            .ForMember(command => command.Password, opt => 
                opt.MapFrom(dto => dto.Password));
    }
}