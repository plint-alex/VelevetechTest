using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Students.Contracts
{
    public interface IStudentsBlService
    {
        Task<GetStudentBlResult> GetStudent(Guid studentId);

        Task<GetStudentsBlResult> GetStudents(GetStudentsBlContract contract);

        Task<Guid> AddStudent(AddStudentBlContract contract);

        Task UpdateStudent(UpdateStudentBlContract contract);

        Task DeleteStudent(Guid studentId);
    }
}
