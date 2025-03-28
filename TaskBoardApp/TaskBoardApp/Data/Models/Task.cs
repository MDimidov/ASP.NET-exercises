using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskConstants = TaskBoardApp.Data.DataConstants.Task;

namespace TaskBoardApp.Data.Models
{
    [Comment("Board Tasks")]
    public class Task
    {
        [Key]
        [Comment($"{nameof(Task)} identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(TaskConstants.TitleMaxLength)]
        [Comment($"{nameof(Task)} title")]
        public required string Title { get; set; }

        [Required]
        [StringLength(TaskConstants.DescriptionMaxLength)]
        [Comment($"{nameof(Task)} description")]
        public required string Description { get; set; }

        [Comment($"{nameof(Task)} creation date")]
        public DateTime? CreatedOn { get; set; } // = DateTime.Now;

        [Comment($"Board identifier")]
        public int? BoardId { get; set; }

        public virtual Board? Board { get; set; }

        [Required]
        [Comment("Application user identifier")]
        public required string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual required IdentityUser Owner { get; set; }

    }
}


//•	Id – a unique integer, Primary Key
//•	Title – a string with min length 5 and max length 70 (required)
//•	Description – a string with min length 10 and max length 1000 (required)
//•	CreatedOn – date and time
//•	BoardId – an integer
//•	Board – a Board object
//•	OwnerId – an integer (required)
//•	Owner – an IdentityUser object

