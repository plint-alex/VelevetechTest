using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IStudentDbService
    {
        Task<Student> GetStudent(Guid studentId);

        Task<int> GetStudentCount(Func<IQueryable<Student>, IQueryable<Student>> filter);

        Task<IEnumerable<Student>> GetStudents(Func<IQueryable<Student>, IQueryable<Student>> filter);

        Task AddStudent(Student student);

        Task UpdateStudent(Student student);

        Task DeleteStudent(Guid studentId);
    }
}
