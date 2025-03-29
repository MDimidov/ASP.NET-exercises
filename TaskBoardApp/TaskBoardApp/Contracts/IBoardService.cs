using TaskBoardApp.Models.Board;

namespace TaskBoardApp.Contracts
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardViewModel>> GetAllAsync();
    }
}
