using ASM.Models;
using ASM.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
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

        public IActionResult Index(string keyword, int? page)
        {
            int pageSie = 8;
            int pageNumber = page == null || page == 0 ? 1 : page.Value;
            var listbook = _context.Books.AsNoTracking().OrderBy(X => X.Title);
            if(!string.IsNullOrEmpty(keyword))
            {
                listbook = _context.Books.Where(x => x.Title.Contains(keyword)).AsNoTracking().OrderBy(X => X.Title);
            }
            PagedList<Book> lst = new PagedList<Book>(listbook, pageNumber, pageSie);
            RouteData.Values.Remove("value");
            RouteData.Values.Add("value", keyword);
            RouteData.Values.Remove("key");
            RouteData.Values.Add("key", "keyword");
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
            RouteData.Values.Remove("value");
            RouteData.Values.Add("value", genreId);
            RouteData.Values.Remove("key");
            RouteData.Values.Add("key", "genreId");
            return View(lst);
        }

        public IActionResult Detail(int bookId, int genreId)
        {
            var book = _context.Books.SingleOrDefault(x => x.BookId == bookId);
            var genre = _context.Genres.Where(x => x.GenreId == genreId).ToList();
            ViewBag.genre = genre;
            return View(book);
        }

        public IActionResult Search(string keyword, int? page)
        {
            int pageSie = 8;
            int pageNumber = page == null || page == 0 ? 1 : page.Value;
            var listbook = _context.Books.AsNoTracking().Where(x => x.Title.Contains(keyword)).OrderBy(X => X.Title);
            PagedList<Book> lst = new PagedList<Book>(listbook, pageNumber, pageSie);
            ViewBag.Keyword = keyword;
            return View(lst);
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
