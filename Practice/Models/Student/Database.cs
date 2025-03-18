namespace Practice.Models.Intro
{
    static class Database
    {
        private static List<Student> _students = new List<Student>
        {
            new Student
            {
                Id = 1,
                Name = "John",
                Email = "John@abv.bg",
            },
            new Student
            {
                Id = 2,
                Name = "John2",
                Email = "John2@abv.bg",
            },
        };

        public static Student? GetStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public static bool UpdateStudent(Student student)
        {
            Student? existingStudent = _students.FirstOrDefault(s => s.Id == student.Id);
            bool result = false;

            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Email = student.Email;

                result = true;
            }

            return result;
        }
    }
}
