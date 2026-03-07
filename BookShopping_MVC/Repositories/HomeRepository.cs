namespace BookShopping_MVC.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;


        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }



        public async Task<IEnumerable<Book>> GetBooks (string sTerm ="", int generId  = 0)
        {
            sTerm = sTerm.ToLower();   // case Insensitive (a or A ) 


               var books = (from book in _db.Books
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             join stock in _db.Stock
                                             on book.Id equals stock.BookId
                                             into book_stocks 
                                             from bookWithStock in book_stocks.DefaultIfEmpty()
                                             where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.BookName.ToLower().StartsWith(sTerm))   // here condation on name
                                             select new Book
                                             {
                                                 Id = book.Id,
                                                 Image = book.Image,
                                                 AuthorName = book.AuthorName,
                                                 BookName = book.BookName,
                                                 GenreId = book.GenreId,
                                                 Price = book.Price,
                                                 GenerName = genre.GenreName,
                                                 Quantity = bookWithStock == null ? 0 : bookWithStock.Quantity
                                             });
                  if(generId > 0)
                  {
                        books = books.Where(a => a.GenreId == generId);
                  }
                  return await books.ToListAsync();

        }


    }
}
