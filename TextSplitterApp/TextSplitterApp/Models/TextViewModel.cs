using System.ComponentModel.DataAnnotations;

namespace TextSplitterApp.Models
{
    public class TextViewModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "The field Text must be a string with a minimum length of 2 and maximum length of 30.")]
        public required string Text { get; set; } = string.Empty;

        public string SplitedText => string.Join(Environment.NewLine, Text.Split());

    }
}
