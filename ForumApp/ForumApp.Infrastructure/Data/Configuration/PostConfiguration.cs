using ForumApp.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Infrastructure.Data.Configuration
{
    internal class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private readonly Post[] initialPost = new Post[]
        {
            new Post() {Id = 1, Title = "My First Title",Content = "My First Content"},
            new Post() {Id = 2, Title = "My Second Title",Content = "My Second Content"},
            new Post() {Id = 3, Title = "My Third Title",Content = "My Third Content"},
        };

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPost);
        }
    }
}
