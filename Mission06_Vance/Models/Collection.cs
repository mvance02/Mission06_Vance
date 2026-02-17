using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission06_Vance.Models
{
    public class Collection
    {
        // Primary key — auto-incremented by EF
        public int CollectionID { get; set; }

        // Foreign key to the Categories table
        [Required(ErrorMessage = "Please select a category.")]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        // Core required fields per assignment spec
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = string.Empty;

        // Year must be at least 1888 (the year the first movie was made)
        [Required(ErrorMessage = "Year is required.")]
        [Range(1888, int.MaxValue, ErrorMessage = "Year must be 1888 or later.")]
        public int Year { get; set; }

        // Edited and CopiedToPlex are nullable so [Required] forces
        // the user to make an explicit yes/no selection in the form
        [Required(ErrorMessage = "Please indicate whether the movie has been edited.")]
        public bool? Edited { get; set; }

        [Required(ErrorMessage = "Please indicate whether the movie has been copied to Plex.")]
        public bool? CopiedToPlex { get; set; }

        // Optional fields
        public string? Director { get; set; }
        public string? Rating { get; set; }
        public string? LentTo { get; set; }

        [MaxLength(25, ErrorMessage = "Notes cannot exceed 25 characters.")]
        public string? Notes { get; set; }
    }
}
