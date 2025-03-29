using TaskBoardApp.Models.Board;
using TaskBoardApp.Models.Task;

namespace TaskBoardApp.Contracts
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllAsync();

        Task<IEnumerable<TaskBoardModel>> GetBoardsListAsync();
    }
}
