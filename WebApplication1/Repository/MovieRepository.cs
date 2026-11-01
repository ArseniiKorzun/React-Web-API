using WebApplication1.Context;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Movie movie) => _context.Movies.Add(movie);

        public void Remove(Movie movie) => _context.Movies.Remove(movie);

        public List<Movie> GetAll() => _context.Movies.ToList();

        public Movie GetById(int id) => _context.Movies.FirstOrDefault(m => m.Id == id);

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
