using System.ComponentModel.DataAnnotations;
using TaskBoardApp.Models.Task;
using static TaskBoardApp.Models.ErrorMessages;
using BoardConstants = TaskBoardApp.Data.DataConstants.Board;


namespace TaskBoardApp.Models.Board
{
    public class BoardViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(BoardConstants.NameMaxLength, MinimumLength = BoardConstants.NameMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
