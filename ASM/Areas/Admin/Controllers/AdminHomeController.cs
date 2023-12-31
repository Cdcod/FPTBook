using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Admin/AdminHome")]
    public class AdminHomeController : Controller
    {
        BookstoreDbContext db = new BookstoreDbContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            var book = db.Books.ToList();
            return View(book);
        }
        [Route("Genre")]
        public IActionResult Genre()
        {
            var listGenre = db.Genres.ToList();
            return View(listGenre);
        }
        [Route("GenreAdd")]
        [HttpGet]
        public IActionResult GenreAdd()
        {
            return View();
        }
        [Route("GenreAdd")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenreAdd(Genre genre)
        {
            if(ModelState.IsValid)
            {
                db.Add(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Genre));
            }
            return View(genre);
        }
        [Route("GenreEdit")]
        [HttpGet]
        public IActionResult GenreEdit(int id)
        {
            var genre = db.Genres.Find(id);
            if(genre == null)
            {
                return RedirectToAction(nameof(Genre));
            }
            return View(genre);
        }
        [Route("GenreEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GenreEdit(Genre genre)
        {
            if(ModelState.IsValid)
            {
                db.Entry(genre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Genre));
            }
            return View(genre);
        }
        [Route("GenreDelete")]
        [HttpGet]
        public ActionResult GenreDelete(int id)
        {
            var itemcount = db.Books.Where(x => x.GenreId == id).Count();
            if(itemcount > 0)
            {
                return RedirectToAction(nameof(Genre));
            }
            var genre = db.Genres.Where(x => x.GenreId == id).FirstOrDefault();
            if(genre != null)
            {
                db.Remove(genre);
                db.SaveChanges();
                return RedirectToAction(nameof(Genre));
            }
            return RedirectToAction(nameof(Genre));

        }
        [Route("Book")]
        public ActionResult Book()
        {
            var book = db.Books.ToList();
            ViewBag.Genre = db.Genres.ToList();
            return View(book);
        }
        [Route("BookAdd")]
        [HttpGet]
        public IActionResult BookAdd()
        {
            ViewBag.Genre = new SelectList(db.Genres.ToList(), "GenreId", "GenreName");
            return View();
        }
        [Route("BookAdd")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookAdd(Book book)
        {
            if(ModelState.IsValid)
            {
                db.Add(book);
                db.SaveChanges();
                return RedirectToAction(nameof(Book));
            }
            return View(book);
        }
        [Route("BookEdit")]
        [HttpGet]
        public IActionResult BookEdit(int id)
        {
            var book = db.Books.Find(id);
            ViewBag.Genre = new SelectList(db.Genres.ToList(), "GenreId", "GenreName");
            return View(book);
        }
        [Route("BookEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookEdit(Book book)
        {
            if(ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(nameof(Book));
            }
            return View(book);
        }
        [Route("BookDelete")]
        public IActionResult BookDelete(int id)
        {
            var book = db.Books.Where(x => x.BookId == id).FirstOrDefault();
            if (book != null)
            {
                db.Remove(book);
                db.SaveChanges();
                return RedirectToAction(nameof(Book));
            }
            return RedirectToAction(nameof(Book));
        }
    }
}
