using DataAccess.Contracts;

namespace VelevetechTest.Controllers.Students.Contracts
{
    public class AddStudentContract
    {
        public string Uid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public SexEnum Sex { get; set; }
    }
}
