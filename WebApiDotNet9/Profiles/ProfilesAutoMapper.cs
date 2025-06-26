using AutoMapper;
using WebApiDotNet9.Dto.Usuario;
using WebApiDotNet9.Models;

namespace WebApiDotNet9.Profiles
{
    public class ProfilesAutoMapper : Profile
    {
        public ProfilesAutoMapper()
        {
            CreateMap<UsuarioCriacaoDto, UsuarioModel>().ReverseMap();
            
        }
    }
}
