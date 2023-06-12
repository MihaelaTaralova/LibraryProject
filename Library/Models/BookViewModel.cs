using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants.Book;

namespace Library.Models
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(MaxBookTitle), MinLength(MinBookTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(MaxBookAuthor), MinLength(MinBookAuthor)]
        public string Author { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;
        [Required]
        public decimal Rating { get; set; }

        [Required]
        [StringLength(MaxBookDescription), MinLength(MinBookDescription)]
        public string Description { get; set; } = null!;
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}

