using BusinessLayer.Services.Authentication.Contracts;
using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserDbService _userService;

        public AuthenticationService(IUserDbService userService)
        {
            _userService = userService;
        }

        public async Task AddUser(AddUserServiceContract contract)
        {
            await _userService.AddUser(new User { Login = contract.Login, Password = MD5Hash(contract.Password) });
        }

        public async Task<LoginServiceResult> Login(LoginServiceContract contract)
        {
            var user = await _userService.GetUser(contract.Login);

            var loginResult = GetLoginResult(contract, user);

            if (loginResult.Success)
            {
                await _userService.UpdateRefreshToken(user.Id, contract.RefreshToken);
            }

            return loginResult;
        }

        public async Task Logout(Guid userId)
        {
            var user = await _userService.GetUser(userId);

            if (user == null) return;

            user.RefreshToken = null;

            await _userService.UpdateRefreshToken(userId, null);
        }

        public async Task<bool> UpdateRefreshToken(Guid userId, Guid oldRefreshToken, Guid newRefreshToken)
        {
            bool result = false;
            var user = await _userService.GetUser(userId);

            if (user == null || user.RefreshToken != oldRefreshToken) return result;

            user.RefreshToken = newRefreshToken;

            await _userService.UpdateRefreshToken(userId, newRefreshToken);
            result = true;

            return result;
        }

        private LoginServiceResult GetLoginResult(LoginServiceContract contract, User user)
        {
            var loginReturn = new LoginServiceResult();
            if (user != null && MD5Hash(contract?.Password).Equals(user.Password))
            {
                loginReturn.UserId = user.Id;
                loginReturn.UserName = user.Login;
                loginReturn.Success = true;
            }
            else
            {
                loginReturn.Success = false;
                loginReturn.Error = "Неверное имя пользователя или пароль";
            }

            return loginReturn;
        }

        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
                return Encoding.ASCII.GetString(result);
            }
        }
    }
}
