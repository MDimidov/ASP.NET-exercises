using Practice.Models.Intro;

namespace Practice.Contracts
{
    public interface IStudentService
    {
        Student GetStudent(int id);

        bool UpdateStudent(Student student);
    }
}
