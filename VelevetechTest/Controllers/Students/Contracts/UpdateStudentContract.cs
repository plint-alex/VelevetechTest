using System;

namespace VelevetechTest.Controllers.Students.Contracts
{
    public class UpdateStudentContract : AddStudentContract
    {
        public Guid Id { get; set; }
    }
}
