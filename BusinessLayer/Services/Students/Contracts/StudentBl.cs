namespace BusinessLayer.Services.Students.Contracts
{
    public class StudentBl : UpdateStudentBlContract
    {
        public override string ToString()
        {
            return $"{FirstName} {LastName} {MiddleName} {Sex} {Uid} {Id}";
        }
    }
}
