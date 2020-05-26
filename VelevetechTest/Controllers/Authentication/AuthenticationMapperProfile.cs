using AutoMapper;
using BusinessLayer.Services.Authentication.Contracts;
using VelevetechTest.Controllers.Authentication.Contracts;

namespace VelevetechTest.Controllers.Authentication
{
    public class AuthenticationMapperProfile : Profile
    {
        public AuthenticationMapperProfile()
        {
            CreateMap<LoginContract, LoginServiceContract>()
                .ForMember(x => x.RefreshToken, x => x.Ignore());
        }
    }
}
