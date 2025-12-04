using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Services;
using MoviesAPI.Data;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        public MoviesService MoviesService { get; set; }

        public MoviesController(MoviesService moviesService)
        {
            MoviesService = moviesService;
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            var movies = MoviesService.GetAllMovies();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int id)
        {
            var movie = MoviesService.GetMovieById(id);
            if (movie == null) return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieVM movie)
        {
            var newMovie = MoviesService.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovieById), new { id = newMovie.Id }, newMovie);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovieById(int id, [FromBody] MovieVM movie)
        {
            var updated = MoviesService.UpdateMovieById(id, movie);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var deleted = MoviesService.DeleteMovie(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
