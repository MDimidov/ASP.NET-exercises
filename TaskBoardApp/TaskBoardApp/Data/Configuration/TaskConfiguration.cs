using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoardApp.Data.Configuration;

namespace TaskBoardApp.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Models.Task>
    {
        public void Configure(EntityTypeBuilder<Models.Task> builder)
        {
            builder
                .HasData(new Models.Task()
                {
                    Id = 1,
                    Title = "Improve CSS styles",
                    Description = "Implement better styling for all public pages",
                    CreatedOn = DateTime.Now.AddDays(-200),
                    OwnerId = Seeds.TestUser.Id,
                    BoardId = Seeds.OpenBoard.Id
                },
                new Models.Task()
                {
                    Id = 2,
                    Title = "Android Client App",
                    Description = "Create Android client app for the TaskBoard RESTful APIO",
                    CreatedOn = DateTime.Now.AddDays(-5),
                    OwnerId = Seeds.TestUser.Id,
                    BoardId = Seeds.OpenBoard.Id
                },
                new Models.Task()
                {
                    Id = 3,
                    Title = "Desktop Client App",
                    Description = "Create Windows Forms desktop app client for the TaskBoard RESTful API",
                    CreatedOn = DateTime.Now.AddDays(-5),
                    OwnerId = Seeds.TestUser.Id,
                    BoardId = Seeds.InProgressBoard.Id
                },
                new Models.Task()
                {
                    Id = 4,
                    Title = "Create Task",
                    Description = "Implement [Create Task] page for adding new tasks",
                    CreatedOn = DateTime.Now.AddDays(-1),
                    OwnerId = Seeds.TestUser.Id,
                    BoardId = Seeds.DoneBoard.Id
                });
        }
    }
}
