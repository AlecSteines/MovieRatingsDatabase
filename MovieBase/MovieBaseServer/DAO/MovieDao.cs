using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBaseServer.DAO
{
    public class MovieDao : IMovieDao
    {
        private readonly string connectionString;

        public MovieDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
    }
}
