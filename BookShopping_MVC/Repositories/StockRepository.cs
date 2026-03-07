namespace BookShopping_MVC.Repositories
{
    public class StockRepository : IStockRepository 
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<Stock?> GetStockByBookId(int bookId) => await
       _context.Stock.FirstAsync(a => a.BookId == bookId);


        public async Task ManageStock(StockDTO stockToManage)
        {
            var existingStock = await GetStockByBookId(stockToManage.BookId);
            if (existingStock is null)
            {
                var stock = new Stock
                {
                    BookId = stockToManage.BookId,
                    Quantity = stockToManage.Quantity
                };

                _context.Stock.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }

                _context.SaveChanges();

        }









        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from book in _context.Books
                               join stock in _context.Stock
                               on book.Id equals stock.BookId
                               into book_stock
                               from bookStock in book_stock.DefaultIfEmpty()
                               where string.IsNullOrWhiteSpace(sTerm) || book.BookName.ToLower().Contains(sTerm.ToLower())
                               select new StockDisplayModel
                               {
                                   BookId = book.Id,
                                   BookName = book.BookName,
                                   Quantity = bookStock == null ? 0 : bookStock.Quantity
                               }
                               ).ToListAsync();

            return stocks;
        }

         

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByBookId(int bookId);
        Task ManageStock(StockDTO stockToManage);

    }
}
