using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Guid? RefreshToken { get; set; }
    }
}
