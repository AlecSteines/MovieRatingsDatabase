using MovieBaseServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBaseServer.DAO
{
    public interface IMovieDao
    {
        List<Movie> GetTop100Movies();
        List<Movie> GetTop15ByGenre(string genre);
        List<List<Movie>> GetMoviesByActor(string actor);
        List<Movie> GetMoviesByTitle(string title);
    }
}
