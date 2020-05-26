using DataAccess.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BusinessLayer.Services.Students.Contracts
{
    public class AddStudentBlContract
    {
        public string Uid { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SexEnum Sex { get; set; }
    }
}
