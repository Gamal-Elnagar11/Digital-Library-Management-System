namespace BookShopping_MVC.Repositories
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> GetBooks(string sTerm = "", int generId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}
