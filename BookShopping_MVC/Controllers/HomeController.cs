
using BookShopping_MVC.Models.DTOs;

namespace BookShopping_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _homeRepository;

        public HomeController(ILogger<HomeController> logger , IHomeRepository homeRepository)
        {
            _logger = logger;
            _homeRepository = homeRepository;
        }





        public async Task<IActionResult> Index(string sterm = "", int genreId = 0)
        {

           IEnumerable<Book> books = await _homeRepository.GetBooks(sterm, genreId);
           IEnumerable<Genre> geners = await _homeRepository.Genres();

            BookDisplayModel bookModel = new BookDisplayModel
            {
                Books = books,
                Geners = geners,
                STerm = sterm,
                GenreId = genreId
            };
            
            return View(bookModel);
        }




         
    }
}
