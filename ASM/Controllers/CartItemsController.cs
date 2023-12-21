using ASM.Models;
using ASM.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ASM.Repository;

namespace ASM.Controllers
{
    public class CartItemsController : Controller
    {
        BookstoreDbContext _db = new BookstoreDbContext();
        
        public IActionResult AddToCart(int id, int quantity)
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Access");
            }

            var book = _db.Books.SingleOrDefault(x => x.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            cart.AddItem(book, quantity);

            SaveCart(cart);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ViewCart()
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Access");
            }
            var cart = GetCart();
            return View( cart);
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            cart.RemoveItem(id);

            SaveCart(cart);

            return RedirectToAction("ViewCart");
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.Get<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }

        private int GetCustomerIdByEmail(string email)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                return customer.CustomerId;
            }
            return -1;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
