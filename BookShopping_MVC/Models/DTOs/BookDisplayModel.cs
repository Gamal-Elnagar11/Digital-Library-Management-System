namespace BookShopping_MVC.Models.DTOs
{
    public class BookDisplayModel
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Genre> Geners { get; set; }  // this is properety has all element in Gener 

        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    
    }
}
