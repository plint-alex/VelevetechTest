using DataAccess.Contracts;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class StudentDbService : IStudentDbService
    {
        public readonly ApplicationDbContext _applicationDbContext;

        public StudentDbService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Student> GetStudent(Guid studentId)
        {
            return await _applicationDbContext.Students.FirstOrDefaultAsync(x => x.Id == studentId);
        }

        public async Task<int> GetStudentCount(Func<IQueryable<Student>, IQueryable<Student>> filter)
        {
            return await filter(_applicationDbContext.Students.AsNoTracking()).CountAsync();
        }

        public async Task<IEnumerable<Student>> GetStudents(Func<IQueryable<Student>, IQueryable<Student>> filter)
        {
            return await filter(_applicationDbContext.Students.AsNoTracking()).ToArrayAsync();
        }

        public async Task AddStudent(Student student)
        {
            await _applicationDbContext.AddAsync(student);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _applicationDbContext.Students.Attach(student);
            _applicationDbContext.Entry(student).State = EntityState.Modified;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteStudent(Guid studentId)
        {
            var student = new Student { Id = studentId, Deleted = true };
            _applicationDbContext.Students.Attach(student);
            _applicationDbContext.Entry(student).Property(x => x.Deleted).IsModified = true;

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
