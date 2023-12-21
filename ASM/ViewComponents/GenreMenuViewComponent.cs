using ASM.Repository;
using Microsoft.AspNetCore.Mvc;
namespace ASM.ViewComponents
{
    public class GenreMenuViewComponent : ViewComponent
    {
        private readonly IGenreRepository _genreRepository;
        public GenreMenuViewComponent(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public IViewComponentResult Invoke()
        {
            var genre = _genreRepository.GetAllGenre().OrderBy(x => x.GenreId);
            return View(genre);
        }
    }
}
