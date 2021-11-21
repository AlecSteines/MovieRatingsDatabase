using MovieBaseServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBaseServer.DAO
{
    public interface IMovieDao
    {
        Dictionary<string, Movie> GetTop100Movies();
    }
}
