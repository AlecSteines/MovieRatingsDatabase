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
    // You may need to delete 'api' from the route
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
            Dictionary<string, Movie> topMovieList = movieDao.GetTop100Movies();
            if(topMovieList != null)
            {
                return Ok(topMovieList);
            }
            return StatusCode(503);
        }
    }
}
