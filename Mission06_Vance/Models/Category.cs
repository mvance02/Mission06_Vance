using System.ComponentModel.DataAnnotations;

namespace Mission06_Vance.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public required string CategoryName { get; set; }
    }
}
