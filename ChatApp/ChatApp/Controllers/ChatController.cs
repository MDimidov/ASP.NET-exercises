using ChatApp.Models.Chat;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static ICollection<MessageViewModel> messages = new List<MessageViewModel>();

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Show));
        }

        public IActionResult Show()
        {
            if (messages.Count() < 1)
            {
                ViewBag.Title = "No Messages yet!";
            }
            else
            {
                ViewBag.Title = "Messages:";
            }

                return View(messages);
        }

        [HttpPost]
        public IActionResult Send(string author, string message)
        {
            int id;
            if (messages.Count() < 1)
            {
                id = 1;
            } else
            {
                id = messages.Max(m => m.Id) + 1;
            }

            MessageViewModel newChat = new()
            {
                Author = author,
                Message = message,
                Id = id,
            };

            messages.Add(newChat);
            return RedirectToAction(nameof(Show));
        }
    }
}
