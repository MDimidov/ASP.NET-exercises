using System.ComponentModel.DataAnnotations;
using static ForumApp.Infrastructure.Constants.Constants.PostConstants;
using static ForumApp.Infrastructure.Constants.Constants.ErrorMessages;

namespace ForumApp.Core.Models
{
    /// <summary>
    /// Post Transfer data model
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Post Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Post title
        /// </summary>
        [Required(ErrorMessage = RequredMessage)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = LengthMessage)]
        public required string Title { get; set; }

        /// <summary>
        /// Post content
        /// </summary>
        [Required(ErrorMessage = RequredMessage)]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength, ErrorMessage = LengthMessage)]
        public required string Content { get; set; }
    }
}
