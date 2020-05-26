using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication.Contracts
{
    public interface IAuthenticationService
    {
        Task<LoginServiceResult> Login(LoginServiceContract contract);

        Task Logout(Guid userId);

        Task<bool> UpdateRefreshToken(Guid userId, Guid oldRefreshToken, Guid newRefreshToken);
    }
}
