using System.ComponentModel.DataAnnotations;

namespace Practice.Models.Intro
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Requred")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must between 2 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is requred")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

    }
}
