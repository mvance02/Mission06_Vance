using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission06_Vance.Models
{
    public class Collection
    {
        public int CollectionID { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public required string Title { get; set; }
        public required string Director { get; set; }
        public int Year { get; set; }
        public string? LentTo { get; set; }

        [MaxLength(25)]
        public string? Notes { get; set; }
        public required string Rating { get; set; }
        public bool? Edited { get; set; }
    }
}