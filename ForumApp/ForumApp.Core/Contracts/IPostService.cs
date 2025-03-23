using ForumApp.Core.Models;

namespace ForumApp.Core.Contracts
{
    public interface IPostService
    {
        Task AddNewPostAsync(PostModel model);
        Task EditPostAsync(PostModel model);
        Task<IEnumerable<PostModel>> GetAllAsync();
        Task<PostModel> GetPostByIdAsync(int id);
    }
}
