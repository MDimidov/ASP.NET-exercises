namespace TaskBoardApp.Models.Task
{
    public class TaskDetailsViewModel : TaskViewModel
    {        
        public string? CreatedOn { get; set; }

        public required string Board { get; set; }
    }
}
