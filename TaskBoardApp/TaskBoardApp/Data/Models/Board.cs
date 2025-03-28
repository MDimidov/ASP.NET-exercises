using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using BoardConstants = TaskBoardApp.Data.DataConstants.Board;

namespace TaskBoardApp.Data.Models
{
    public class Board
    {
        [Key]
        [Comment("Board identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(BoardConstants.NameMaxLength)]
        [Comment($"{nameof(Board)} identifier")]
        public required string Name { get; set; }

        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}

//•	Id – a unique integer, Primary Key
//•	Name – a string with min length 3 and max length 30 (required)
//•	Tasks – a collection of Task
