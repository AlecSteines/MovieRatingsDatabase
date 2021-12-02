using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBaseServer.DAO;
using MovieBaseServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBaseServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieDao movieDao;

        public MovieController(IMovieDao _movieDao)
        {
            movieDao = _movieDao;
        }

        [HttpGet("top/all")]
        public IActionResult GetTop100Movies()
        {
            List<Movie> topMovieList = movieDao.GetTop100Movies();
            if(topMovieList != null)
            {
                return Ok(topMovieList);
            }
            return StatusCode(503);
        }
        [HttpGet("top/{genre}")]
        public IActionResult GetTop15ByGenre(string genre)
        {
            List<Movie> topMovieGenreList = movieDao.GetTop15ByGenre(genre);
            if (topMovieGenreList != null)
            {
                return Ok(topMovieGenreList);
            }
            return StatusCode(503);
        }
        [HttpGet("actor/{actor}")]
        public IActionResult GetMoviesByActor(string actor)
        {
            List<List<Movie>> moviesByActor = movieDao.GetMoviesByActor(actor);
            if (moviesByActor != null)
            {
                return Ok(moviesByActor);
            }
            return StatusCode(503);
        }
        [HttpGet("title/{title}")]
        public IActionResult GetMoviesByTitle(string title)
        {
            List<Movie> moviesByTitle = movieDao.GetMoviesByTitle(title);
            if (moviesByTitle != null)
            {
                return Ok(moviesByTitle);
            }
            return StatusCode(503);
        }
    }
}
