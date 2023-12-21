using ASM.Models;
namespace ASM.Repository
{
    public interface IGenreRepository
    {
        Genre Add(Genre genre);
        Genre Update(Genre genre);
        Genre Delete(int genreId);
        Genre GetGenre(int genreId);
        IEnumerable<Genre> GetAllGenre();
    }
}
