using System;

namespace BusinessLayer.Services.Authentication.Contracts
{
    public class LoginServiceContract
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
