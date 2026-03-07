using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace BookShopping_MVC.Controllers
{
    [Authorize]  // must be logged in to access this controller
   // [Authorize(Roles = "Admin")]  // must be admin to access this controller
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }


        public async Task<IActionResult> AddItem(int bookId, int qty=1, int redirect=0)
        {
            var cartCount = await _cartRepository.AddItem(bookId, qty);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }


        public async Task<IActionResult> RemoveItem(int bookId)
        {
             var cartCount = await _cartRepository.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");

        }


        public async Task<IActionResult> GetUserCart(int bookId)
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }

        
        public async Task<IActionResult> GetTotalItemInCart()
        {
            int cartItem = await _cartRepository.GetCartItemCount();
            return Ok(cartItem);
        }



        public IActionResult Checkout()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutModel model)
        {
            if(!ModelState.IsValid) 
                return View(model);  // here return the same page with the error in same page without delete the data

            bool isCheckOut = await _cartRepository.DoCheck(model);
            if (!isCheckOut)
                return RedirectToAction(nameof(OrderFailure));
             return RedirectToAction(nameof(OrderSuccess));
        }



        public IActionResult OrderSuccess()
        {

            return View();
        }

        public IActionResult OrderFailure()
        {

            return View();
        }






    }
}
