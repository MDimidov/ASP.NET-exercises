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

        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual Board? Board { get; set; }

        [Required]
        [Comment("Application user identifier")]
        public required string OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual IdentityUser Owner { get; set; } = null!;

    }
}
