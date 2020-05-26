using System;

namespace BusinessLayer.Services.Authentication.Contracts
{
    public class LoginServiceResult
    {
        public string Error { get; set; }
        public bool Success { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }
}
