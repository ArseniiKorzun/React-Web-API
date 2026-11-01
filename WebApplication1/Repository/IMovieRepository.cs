using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        void Add(Movie movie);
        void Remove(Movie movie);
        void SaveChanges();
    }
}
