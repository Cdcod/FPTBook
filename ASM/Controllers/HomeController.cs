using ASM.Models;
using ASM.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace ASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookstoreDbContext _context;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, BookstoreDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? page)
        {
            int pageSie = 8;
            int pageNumber = page == null || page == 0 ? 1 : page.Value;
            var listbook = _context.Books.AsNoTracking().OrderBy(X => X.Title);
            PagedList<Book> lst = new PagedList<Book>(listbook, pageNumber, pageSie);
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(lst);
        }

        public IActionResult BookByGenre(int genreId, int? page)
        {
            int pageSie = 8;
            int pageNumber = page == null || page == 0 ? 1 : page.Value;
            var listbook = _context.Books.AsNoTracking().Where(x => x.GenreId == genreId).OrderBy(X => X.Title);
            PagedList<Book> lst = new PagedList<Book>(listbook, pageNumber, pageSie);
            ViewBag.GenreId = genreId;
            return View(lst);
        }

        public IActionResult Detail(int bookId, int genreId)
        {
            var book = _context.Books.SingleOrDefault(x => x.BookId == bookId);
            var genre = _context.Genres.Where(x => x.GenreId == genreId).ToList();
            ViewBag.genre = genre;
            return View(book);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
