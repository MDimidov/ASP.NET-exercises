using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Contracts;
using TaskBoardApp.Data;
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
            Data.Models.Task entity = new()
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

        public async Task<TaskDetailsViewModel> GetTaskById(int id)
        {
            var entity = await context.Tasks
                .Include(t => t.Board)
                .Include(t => t.Owner)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (entity == null)
            {
                throw new ArgumentException($"Invalid id: {id}");
            }

            return new TaskDetailsViewModel()
            {
                Id = id,
                Title = entity.Title,
                Description = entity.Description,
                Board = entity.Board!.Name,
                CreatedOn = entity.CreatedOn?.ToString("dd-MM-yyyy HH:mm"),
                Owner = entity.Owner.UserName!
            };
        }
    }
}
