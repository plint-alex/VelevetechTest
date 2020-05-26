using BusinessLayer.CommonContracts;
using System.Collections.Generic;

namespace BusinessLayer.Services.Students.Contracts
{
    public class GetStudentsBlResult : PageInfoResult
    {
        public ICollection<StudentBl> Students { get; set; }
    }
}
