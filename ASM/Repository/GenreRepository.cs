using ASM.Models;

namespace ASM.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookstoreDbContext _context;
        public GenreRepository(BookstoreDbContext context)
        {
            _context = context;
        }
        public Genre Add(Genre genre)
        {
            _context.Genres.Add(genre); 
            _context.SaveChanges();
            return genre;
        }

        public Genre Delete(int genreId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> GetAllGenre()
        {
            return _context.Genres;
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Find(genreId);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return genre;
        }
    }
}
