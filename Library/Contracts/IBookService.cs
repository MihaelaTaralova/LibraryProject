using Library.Models;

namespace Library.Contracts
{
    public interface IBookService
    {
        Task AddBookAsync(AddBookViewModel model);

        Task AddBookToCollectionAsync(string userId, BookViewModel book);

        Task EditBookAsync(AddBookViewModel model, int id);

        Task<IEnumerable<AllBookViewModel>> GetAllBooksAsync();

        Task<AddBookViewModel?> GetBookByIdForEditAsync(int id);

        Task<IEnumerable<MineBookViewModel>> GetMyBooksAsync(string UserId);

        Task<BookViewModel?> GetBookByIdAsync(int id);

        Task RemoveBookFromCollectionAsync(string userId, BookViewModel book);

        Task<AddBookViewModel> GetNewAddBookModelAsync();
    }
}
