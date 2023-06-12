using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Data.DataConstants.Book;

namespace Library.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxBookTitle)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(MaxBookAuthor)]
        public string Author { get; set; } = null!;

        [Required]
        [MaxLength(MaxBookDescription)]
        public string Description { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public decimal Rating { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [Required]
        public Category Category { get; set; } = null!;

        public IEnumerable<IdentityUserBook> UsersBooks { get; set; } = new List<IdentityUserBook>();

    }
}
