using System;
using System.Collections.Generic;

namespace VelevetechTest.Controllers.Authentication.Contracts
{
    public class LoginResult
    {
        public Guid UserId { get; set; }

        public string Login { get; set; }

        public string UserName { get; set; }

        public int GiveinPlaceId { get; set; }

        public string AccessToken { get; set; }

        public Guid? RefreshToken { get; set; }

        public DateTime ExpirationTime { get; set; }

        public int IdleTimeout { get; set; }

        public Dictionary<string, string> ErrorFields { get; set; }

        public string Error { get; set; }
    }
}
