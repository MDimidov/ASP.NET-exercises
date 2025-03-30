using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Contracts
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskFormModel model, string userId);
        Task EditTaskAsync(TaskFormModel model, int taskId, string userId);
        Task<TaskDetailsViewModel> GetTaskById(int id);
        Task<TaskFormModel> GetTaskFormById(int id, string userId);
    }
}
