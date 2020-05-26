using System;

namespace VelevetechTest.Controllers.Authentication.Contracts
{
    public class RefreshTokenContract
    {
        public string AccessToken { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
