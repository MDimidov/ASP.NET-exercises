using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Contracts
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskFormModel model, string userId);
        Task DeleteTaskByIdAsync(int id);
        Task EditTaskAsync(TaskFormModel model, int taskId, string userId);
        Task<TaskDetailsViewModel> GetTaskById(int id);
        Task<TaskViewModel> GetTaskDeleteByIdAsync(int id, string userId);
        Task<TaskFormModel> GetTaskFormById(int id, string userId);
    }
}
