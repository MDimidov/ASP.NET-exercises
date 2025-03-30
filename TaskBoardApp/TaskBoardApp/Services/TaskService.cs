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

        public async Task CreateTaskAsync(TaskFormModel model, string userId)
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

        public async Task EditTaskAsync(TaskFormModel model, int taskId, string userId)
        {
            var entityTask = await context.Tasks.FindAsync(taskId);

            if (entityTask == null)
            {
                throw new ArgumentException($"Invalid id: {taskId}");
            }

            if (entityTask.OwnerId != userId)
            {
                throw new UnauthorizedAccessException(entityTask.OwnerId);
            }

            entityTask.Title = model.Title;
            entityTask.Description = model.Description;
            entityTask.BoardId = model.BoardId;

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

        public async Task<TaskFormModel> GetTaskFormById(int id, string userId)
        {
            var entityTask = await context.Tasks
                .FindAsync(id);

            if (entityTask == null)
            {
                throw new ArgumentException($"Invalid id: {id}");
            }

            if (entityTask.OwnerId != userId)
            {
                throw new UnauthorizedAccessException(entityTask.OwnerId);
            }

            return new TaskFormModel()
            {
                Title = entityTask.Title,
                Description = entityTask.Description,
                BoardId = entityTask.BoardId,
            };
        }
    }
}
