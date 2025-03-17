using Microsoft.AspNetCore.Mvc;
using Practice.Contracts;
using Practice.Models.Intro;
using Practice.Services;

namespace Practice.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";
            return View();
        }

        [HttpGet]
        public IActionResult GetStudentInfo(int id)
        {
            ViewBag.Title = "Show student info";

            Student model = studentService.GetStudent(id);

            return View("StudentInfo", model);
        }

        [HttpPost]
        public IActionResult EditStudent(Student student)
        {
            ViewBag.Title = "Edit student";

            Student model = student;
            if (!ModelState.IsValid)
            {
                return View("StudentInfo", model);
            }

            if (studentService.UpdateStudent(model))
            {
                return RedirectToAction(nameof(GetStudentInfo), new { id = model.Id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
