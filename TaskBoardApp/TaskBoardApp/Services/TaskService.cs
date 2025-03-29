using TaskBoardApp.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.Data.Models;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskBoardAppDbContext context;

        public TaskService(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public async System.Threading.Tasks.Task CreateTaskAsync(TaskFormModel model, string userId)
        {
            Data.Models.Task entity = new ()
            {
                Title = model.Title,
                Description = model.Description,
                BoardId = model.BoardId,
                CreatedOn = DateTime.UtcNow,
                OwnerId = userId,
            };

            await context.Tasks.AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}
