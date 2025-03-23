using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumAppDbContext context;

        public PostService(ForumAppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
            => await context.Posts
                .Select(p => new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                })
            .AsNoTracking() 
            .ToListAsync();
    }
}
