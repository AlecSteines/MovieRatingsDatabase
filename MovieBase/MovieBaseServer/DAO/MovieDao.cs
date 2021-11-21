using Microsoft.Data.SqlClient;
using MovieBaseServer.Models;
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

        public Dictionary<string, Movie> GetTop100Movies()
        {
            Dictionary<string, Movie> movies = new Dictionary<string, Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 100 series_title, release_year, runtime, genre, imdb_rating, director FROM moviebase " +
                        "order by imdb_rating desc", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Movie movie = GetMovieFromReader(reader);

                        if(movie != null)
                        {
                            movies.Add(movie.Title, movie);
                        }
                    }
                }
                return movies;
            }
            catch(SqlException)
            {
                throw new Exception();
            }
        }

        private Movie GetMovieFromReader(SqlDataReader reader)
        {
            try
            {
                Movie movie = new Movie()
                {
                    Title = Convert.ToString(reader["series_title"]),
                    ReleaseDate = Convert.ToString(reader["release_year"]),
                    Runtime = Convert.ToString(reader["runtime"]),
                    Genre = Convert.ToString(reader["genre"]),
                    Rating = Convert.ToDecimal(reader["imdb_rating"]),
                    Director = Convert.ToString(reader["director"])
                };

                return movie;
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }
    }
}
