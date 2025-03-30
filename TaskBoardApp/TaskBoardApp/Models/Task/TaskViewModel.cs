using System.ComponentModel.DataAnnotations;
using static TaskBoardApp.Models.ErrorMessages;
using TaskConstants = TaskBoardApp.Data.DataConstants.Task;


namespace TaskBoardApp.Models.Task
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(TaskConstants.TitleMaxLength, MinimumLength = TaskConstants.TitleMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(TaskConstants.DescriptionMaxLength, MinimumLength = TaskConstants.DescriptionMinLength, ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public required string Owner { get; set; }
    }
}
