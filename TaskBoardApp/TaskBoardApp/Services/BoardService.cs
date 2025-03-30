using Microsoft.EntityFrameworkCore;
using TaskBoardApp.Contracts;
using TaskBoardApp.Data;
using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Home;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Services
{
    public class BoardService : IBoardService
    {
        private readonly TaskBoardAppDbContext context;

        public BoardService(TaskBoardAppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BoardViewModel>> GetAllAsync()
            => await context.Boards
                .Select(b => new BoardViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new TaskViewModel
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName!
                    })
                })
                .ToListAsync();

        public async Task<IEnumerable<TaskBoardModel>> GetBoardsListAsync()
            => await context.Boards
                .AsNoTracking()
                .Select(b => new TaskBoardModel { Id = b.Id, Name = b.Name, })
                .ToListAsync();

        public async Task<HomeViewModel> GetTasksCountAsync(string userId)
        {

            var boards = await context.Boards
                .Select(b => b.Name)
                .Distinct()
                .ToListAsync();

            var tasksCount = new List<HomeBoardModel>();
            foreach (var board in boards)
            {
                int tasksInBoard = await context.Tasks
                    .Where(t => t.Board!.Name == board)
                    .CountAsync();

                tasksCount.Add(new HomeBoardModel
                {
                    BoardName = board,
                    TasksCount = tasksInBoard
                });
            }

            return new HomeViewModel
            {
                AllTasksCount = await context.Tasks.CountAsync(),
                UserTasksCount = context.Tasks.Where(t => t.OwnerId == userId).Count(),
                BoardsWithTasksCount = tasksCount
            };
        }
    }
}
