using AutoMapper;
using BusinessLayer.Services.Students.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VelevetechTest.Controllers.Students.Contracts;

namespace VelevetechTest.Controllers.Students
{
    public class StudentsMapperProfile : Profile
    {
        public StudentsMapperProfile()
        {
            CreateMap<AddStudentContract, AddStudentBlContract>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .IgnoreAllSourcePropertiesWithAnInaccessibleSetter();


            CreateMap<GetStudentBlResult, GetStudentResult>();

            CreateMap<AddStudentContract, AddStudentBlContract>();

            CreateMap<UpdateStudentContract, UpdateStudentBlContract>();
        }
    }
}
