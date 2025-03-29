using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Contracts
{
    public interface ITaskService
    {
        Task CreateTaskAsync(TaskFormModel model, string userId);
    }
}
