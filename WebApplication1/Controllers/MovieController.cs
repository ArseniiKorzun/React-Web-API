using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController(MovieService _movieService, IValidator<MovieRequest> _validator) : ControllerBase
    {
        [HttpGet("movies")]
        public ActionResult<List<MovieResponse>> GetMovies()
        {
            var response = _movieService.GetAll().Select(m => ToResponse(m)).ToList();
            return Ok(response);
        }

        [HttpGet("movies/{id}")]
        public ActionResult<MovieResponse> GetMovie(int id)
        {
            var movie = _movieService.GetById(id);
            if (movie == null)
                return NotFound();

            return Ok(ToResponse(movie));
        }

        [HttpPost("movies")]
        public ActionResult AddMovie([FromBody] MovieRequest movieRequest)
        {
            var validationResult = _validator.Validate(movieRequest);

            if (!validationResult.IsValid)
            {
                return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
            }

            var movie = new Movie
            {
                Title = movieRequest.Title,
                Description = movieRequest.Description,
                PhotoPath = movieRequest.PhotoPath,
                Director = movieRequest.Director,
                Runtime = movieRequest.Runtime
            };
            _movieService.Add(movie);
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, ToResponse(movie));
        }

        [HttpDelete("movies/{id}")]
        public ActionResult DeleteMovie(int id)
        {
            if (!_movieService.Delete(id))
                return NotFound();

            return NoContent();
        }

        private static MovieResponse ToResponse(Movie m) => new()
        {
            Id = m.Id,
            Title = m.Title,
            Description = m.Description,
            PhotoPath = m.PhotoPath,
            Director = m.Director,
            Runtime = m.Runtime
        };
    }
}
