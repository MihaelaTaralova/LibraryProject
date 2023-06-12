using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task AddBookToCollectionAsync(string userId, BookViewModel book);
        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

        Task<IEnumerable<MineBookViewModel>> GetMyBooksAsync(string UserId);

        Task<BookViewModel?> GetBookByIdAsync(int id);

    }
}
