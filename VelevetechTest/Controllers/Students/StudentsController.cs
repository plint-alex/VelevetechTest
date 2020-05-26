using AutoMapper;
using BusinessLayer.Services.Students.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VelevetechTest.Controllers.Students.Contracts;

namespace VelevetechTest.Controllers.Students
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsBlService _studentsBlService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsBlService studentsBlService, IMapper mapper)
        {
            _studentsBlService = studentsBlService;
            _mapper = mapper;
        }

        [HttpGet("GetStudent/{id}")]
        public async Task<GetStudentResult> GetStudent(Guid id)
        {
            var result = await _studentsBlService.GetStudent(id);
            return _mapper.Map<GetStudentResult>(result);
        }

        [HttpPost("GetStudents")]
        public async Task<GetStudentsBlResult> GetStudents(GetStudentsBlContract contract)
        {
            return await _studentsBlService.GetStudents(contract);
        }

        [HttpPost("AddStudent")]
        public async Task<object> AddStudent(AddStudentContract contract)
        {
            var blContract = _mapper.Map<AddStudentBlContract>(contract);
            var id = await _studentsBlService.AddStudent(blContract);

            return new { id };
        }

        [HttpPost("UpdateStudent")]
        public async Task<object> UpdateStudent(UpdateStudentContract contract)
        {
            var blContract = _mapper.Map<UpdateStudentBlContract>(contract);
            await _studentsBlService.UpdateStudent(blContract);

            return new { };
        }

        [HttpPost("DeleteStudent")]
        public async Task<object> DeleteStudent([FromBody] Guid studentId)
        {
            await _studentsBlService.DeleteStudent(studentId);

            return new { };
        }
    }
}