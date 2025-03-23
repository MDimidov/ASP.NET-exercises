using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ForumApp.Infrastructure.Constants.Constants.PostConstants;

namespace ForumApp.Infrastructure.Data.Models
{
    [Comment("Post table")]
    public class Post
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Post title")]
        public required string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        [Comment("Post content")]
        public required string Content { get; set; }

    }
}


//•	Id – an unique integer, primary key
//•	Title – a string with min length 10 and max length 50 (required)
//•	Content – a string with min length 30 and max length 1500 (required)
