using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Models.ErrorMessages;
using TaskConstants = TaskBoardApp.Data.DataConstants.Task;

namespace TaskBoardApp.Models.Task
{
    public class TaskFormModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(TaskConstants.TitleMaxLength, MinimumLength = TaskConstants.TitleMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(TaskConstants.DescriptionMaxLength, MinimumLength = TaskConstants.DescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel> Boards { get; set; } = new List<TaskBoardModel>();
    }
}
