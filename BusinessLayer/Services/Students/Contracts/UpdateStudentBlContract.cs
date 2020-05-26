using System;

namespace BusinessLayer.Services.Students.Contracts
{
    public class UpdateStudentBlContract : AddStudentBlContract
    {
        public Guid Id { get; set; }
    }
}
