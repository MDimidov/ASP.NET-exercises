using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Infrastructure.Data;
using ForumApp.Infrastructure.Data.Models;
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

        public async Task AddNewPostAsync(PostModel model)
        {
            Post entity = new()
            {
                Title = model.Title,
                Content = model.Content,
            };

            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeletePostById(int id)
        {
            Post? entity = await context.FindAsync<Post>(id);

            if (entity == null)
            {
                throw new ArgumentException($"No such post with id = {id}");
            }

            context.Posts.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(PostModel model)
        {
            Post? entity = await context.FindAsync<Post>(model.Id);

            if (entity == null)
            {
                throw new ArgumentException($"No such post with id = {model.Id}");
            }

            entity.Title = model.Title;
            entity.Content = model.Content;
            await context.SaveChangesAsync();
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

        public async Task<PostModel?> GetPostByIdAsync(int id)
        {
            Post? entity = await context.FindAsync<Post>(id);

            if (entity == null)
            {
                return null;
            }

            return new PostModel { Id = entity.Id, Content = entity.Content, Title = entity.Title };
        }
    }
}
