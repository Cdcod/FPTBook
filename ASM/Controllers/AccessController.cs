using ASM.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;

namespace FPTBook.Controllers
{
    public class AccessController : Controller
    {
        BookstoreDbContext _context = new BookstoreDbContext();
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(Customer customer)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var user = _context.Customers.Where(x => x.Email.Equals(customer.Email)).FirstOrDefault();
                if (user != null && Crypto.VerifyHashedPassword(user.Password, customer.Password))
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Access");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var passwordHashed = Crypto.HashPassword(customer.Password);

                var user = new Customer()
                {
                    Email = customer.Email,
                    Username = customer.Username,
                    Password = passwordHashed
                };

                _context.Customers.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
