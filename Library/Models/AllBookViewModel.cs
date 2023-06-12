using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;
namespace Library.Models
{
    public class AllBookViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(MaxBookTitle), MinLength(MinBookTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxBookAuthor), MinLength(MinBookAuthor)]
        public string Author { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public string Category { get; set; } = null!;
    }
}
