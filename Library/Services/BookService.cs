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

        public async Task AddBookToCollectionAsync(string userId, BookViewModel book)
        {
            bool alreadyAdded = await context.IdentityUsers
                .AnyAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (alreadyAdded == false)
            {
                var userBook = new IdentityUserBook()
                {
                    CollectorId = userId,
                    BookId = book.Id,
                };

                await context.IdentityUsers.AddAsync(userBook);
                await context.SaveChangesAsync();

            }

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

        public async Task RemoveBookFromCollectionAsync(string userId, BookViewModel book)
        {
            var userBook = await context.IdentityUsers
                .FirstOrDefaultAsync(ub => ub.CollectorId == userId && ub.BookId == book.Id);

            if (userBook != null)
            {
                context.IdentityUsers.Remove(userBook);
                await context.SaveChangesAsync();
            };
        }

        public async Task<AddBookViewModel> GetNewAddBookModelAsync()
        {
            var categories = await context.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            var model = new AddBookViewModel
            {
                Categories = categories
            };

            return model;
        }

        public async Task AddBookAsync(AddBookViewModel model)
        {
            Book book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                ImageUrl = model.Url,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Rating = decimal.Parse(model.Rating)
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }

        public async Task<AddBookViewModel?> GetBookByIdForEditAsync(int id)
        {
            var categories = await context.Categories.Select(c => new CategoryViewModel
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();

            return await context.Books
                .Where(c => c.Id == id)
                .Select(b => new AddBookViewModel 
                {
                    Title = b.Title,
                    Author = b.Author,
                    Url = b.ImageUrl,
                    Description = b.Description,
                    Rating = b.Rating.ToString(),
                    CategoryId = b.CategoryId,
                    Categories = categories
                }).FirstOrDefaultAsync();
        }

        public async Task EditBookAsync(AddBookViewModel model, int id)
        {
            var book = await context.Books.FindAsync(id);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.ImageUrl = model.Url;
                book.Description = model.Description;
                book.CategoryId = model.CategoryId;
                book.Rating = decimal.Parse(model.Rating);

                await context.SaveChangesAsync();
            }
        }
    }
}
