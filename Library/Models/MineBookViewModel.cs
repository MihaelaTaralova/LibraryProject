using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Models
{
    public class MineBookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxBookTitle), MinLength(MinBookTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxBookAuthor), MinLength(MinBookAuthor)]
        public string Author { get; set; } = null!;

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
