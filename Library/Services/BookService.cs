using Library.Contracts;
using Library.Data;
using Library.Data.Models;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext context;

        public BookService(LibraryDbContext _context)
        {
            this.context = _context;
        }

        public async Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync()
        {
           return await this.context.Books
                .Select(b => new AllBookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    Rating = b.Rating,
                    Category = b.Category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MineBookViewModel>> GetMyBooksAsync(string UserId)
        {
            return await context.IdentityUsers
                .Where(iu => iu.CollectorId == UserId)
                .Select(b => new MineBookViewModel 
                {
                    Id = b.Book.Id,
                    Title = b.Book.Title,
                    Author = b.Book.Author,
                    Description = b.Book.Description,
                    ImageUrl = b.Book.ImageUrl,
                    Category = b.Book.Category.Name
                }).ToListAsync();
        }

        // TODO: this need to be implemented
        //public async Task<IActionResult> AddToCollection(int userId)
        //{
        //    return;

        //}

        public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
        {
            bool alreadyAdded = await context.IdentityUsers
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if(alreadyAdded)
            {
                throw new InvalidOperationException("Book already added to collection");
            }

            var userBook = new IdentityUserBook()
            {
                CollectorId = userId,
                BookId = book.Id,
            };

            await context.IdentityUsers.AddAsync(userBook);
            await context.SaveChangesAsync();

        }

        public async Task<BookViewModel?> GetBookByIdAsync(int id)
        {
            return await context.Books
                 .Where(b => b.Id == id)
                 .Select(x => new BookViewModel
                 { 
                     Id = x.Id,
                     Title = x.Title,
                     Author = x.Author,
                     Description = x.Description,
                     ImageUrl = x.ImageUrl,
                     Rating = x.Rating,
                     CategoryId = x.CategoryId,
                 }).FirstOrDefaultAsync();

        }
    }
}
