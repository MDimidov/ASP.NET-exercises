using Practice.Contracts;
using Practice.Models.Intro;

namespace Practice.Services
{
    public class StudentService : IStudentService
    {
        public Student GetStudent(int id)
        {
            return Database.GetStudent(id);
        }

        public bool UpdateStudent(Student student)
        {
            return Database.UpdateStudent(student);
        }
    }
}
