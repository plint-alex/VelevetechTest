using DataAccess.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Student
    {
        public Guid Id { get; set; } 

        public SexEnum Sex { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Uid { get; set; }

        public bool Deleted { get; set; }
    }
}
