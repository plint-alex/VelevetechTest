using BusinessLayer.CommonContracts;
using DataAccess.Contracts;

namespace BusinessLayer.Services.Students.Contracts
{
    public class GetStudentsBlContract : PageInfoContract
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public SexEnum? Sex { get; set; }

        public string[] Uids { get; set; }
    }
}
