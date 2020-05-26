using BusinessLayer.Services.Students.Contracts;
using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Students
{
    public class StudentsBlService : IStudentsBlService
    {
        private readonly IStudentDbService _studentDbService;

        public StudentsBlService(IStudentDbService studentDbService)
        {
            _studentDbService = studentDbService;
        }

        public async Task<GetStudentBlResult> GetStudent(Guid studentId)
        {
            var student = await _studentDbService.GetStudent(studentId);

            return new GetStudentBlResult
            {
                FirstName = student.FirstName,
                Id = student.Id,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Sex = student.Sex,
                Uid = student.Uid,
            };
        }

        public async Task<GetStudentsBlResult> GetStudents(GetStudentsBlContract contract)
        {
            if (contract == null)
            {
                return new GetStudentsBlResult
                {
                    PageNumber = 1,
                    Students = new StudentBl[0],
                    Total = 0,
                };
            }

            var pageNumber = contract.PageNumber < 1 ? 1 : contract.PageNumber;
            var pageSize = contract.PageSize < 1 ? 1 : contract.PageSize;

            IQueryable<Student> filter(IQueryable<Student> query)
            {
                if (!string.IsNullOrWhiteSpace(contract.FirstName))
                {
                    query = query.Where(x => x.FirstName.ToLower().Contains(contract.FirstName.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(contract.LastName))
                {
                    query = query.Where(x => x.LastName.ToLower().Contains(contract.LastName.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(contract.MiddleName))
                {
                    query = query.Where(x => x.MiddleName.ToLower().Contains(contract.MiddleName.ToLower()));
                }

                if (contract.Sex.HasValue)
                {
                    query = query.Where(x => x.Sex == contract.Sex.Value);
                }

                if (contract.Uids != null && contract.Uids.Any())
                {
                    query = query.Where(x => contract.Uids.Contains(x.Uid));
                }

                return query.Where(x => !x.Deleted);
            }

            IQueryable<Student> postFilter(IQueryable<Student> query)
            {
                if (!string.IsNullOrWhiteSpace(contract.SortByField))
                {
                    Expression<Func<Student, object>> orderExpression = null;

                    switch (contract.SortByField)
                    {
                        case "FirstName":
                            orderExpression = (x) => x.FirstName;
                            break;
                        case "LastName":
                            orderExpression = (x) => x.LastName;
                            break;
                        case "MiddleName":
                            orderExpression = (x) => x.MiddleName;
                            break;
                        case "Uid":
                            orderExpression = (x) => x.Uid;
                            break;
                        case "Sex":
                            orderExpression = (x) => x.Sex;
                            break;
                    }

                    if (orderExpression != null )
                    {
                        if (contract.Desc)
                        {
                            query = query.OrderByDescending(orderExpression);
                        }
                        else
                        {
                            query = query.OrderBy(orderExpression);
                        }
                    }
                }

                return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }

            var count = await _studentDbService.GetStudentCount(filter);
            var students = await _studentDbService.GetStudents((query) => postFilter(filter(query)));

            return new GetStudentsBlResult
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Total = count,
                Students = students.Select(x => new StudentBl
                {
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName,
                    MiddleName = x.MiddleName,
                    Sex = x.Sex,
                    Uid = x.Uid,
                }).ToArray()
            };
        }

        public async Task<Guid> AddStudent(AddStudentBlContract contract)
        {
            await ValidateStudent(contract);

            var newStudent = new Student
            {
                Deleted = false,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                MiddleName = contract.MiddleName,
                Sex = contract.Sex,
                Uid = contract.Uid,
            };

            await _studentDbService.AddStudent(newStudent);

            return newStudent.Id;
        }

        public async Task UpdateStudent(UpdateStudentBlContract contract)
        {
            await ValidateStudent(contract, contract.Id);

            await _studentDbService.UpdateStudent(new Student
            {
                Id = contract.Id,
                Deleted = false,
                FirstName = contract.FirstName,
                LastName = contract.LastName,
                MiddleName = contract.MiddleName,
                Sex = contract.Sex,
                Uid = contract.Uid,
            });
        }

        public async Task DeleteStudent(Guid studentId)
        {
            await _studentDbService.DeleteStudent(studentId);
        }

        private async Task ValidateStudent(AddStudentBlContract contract, Guid? id = null)
        {
            if (contract == null)
            {
                throw new ArgumentNullException("contract");
            }

            if (string.IsNullOrEmpty(contract.FirstName) || contract.FirstName.Length > 40)
            {
                throw new ArgumentException("This field is required and length must be less or equal 40", "FirstName");
            }

            if (string.IsNullOrEmpty(contract.LastName) || contract.LastName.Length > 40)
            {
                throw new ArgumentException("This field is required and length must be less or equal 40", "LastName");
            }

            if (!string.IsNullOrEmpty(contract.MiddleName) && contract.MiddleName.Length > 60)
            {
                throw new ArgumentException("Length must be less or equal 60", "MiddleName");
            }

            if (!string.IsNullOrEmpty(contract.Uid))
            {
                if(contract.Uid.Length < 6 || contract.Uid.Length > 16)
                {
                    throw new ArgumentException("Length must be between 6 and 16", "uid");
                }

                IQueryable<Student> filter(IQueryable<Student> query)
                {
                    if (!string.IsNullOrEmpty(contract.Uid))
                    {
                        query = query.Where(x => contract.Uid == x.Uid && x.Id != id);
                    }

                    return query;
                }

                var count = await _studentDbService.GetStudentCount(filter);

                if (count > 0)
                {
                    throw new ArgumentException("This field must be unique", "uid");
                }
            }
        }
    }
}
