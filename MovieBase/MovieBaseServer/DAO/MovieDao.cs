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

        public List<Movie> GetTop100Movies()
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 100 series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "order by imdb_rating desc", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Movie movie = GetMovieFromReader(reader);
       
                        if(movie != null)
                        {
                            movies.Add(movie);
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

        public List<Movie> GetTop15ByGenre(string genre)
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT TOP 15 series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "where Genre like @genre order by imdb_rating desc", conn);
                    cmd.Parameters.AddWithValue("@genre", "%"+genre+"%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Movie movie = GetMovieFromReader(reader);

                        if (movie != null)
                        {
                            movies.Add(movie);
                        }

                    }
                }
                return movies;
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }

        public List<Movie> GetMoviesByActor(string actor)
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "where star1 like @actor or star2 like @actor or star3 like @actor or star4 like @actor", conn);
                    cmd.Parameters.AddWithValue("@actor", "%" + actor + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        Movie movie = GetMovieFromReader(reader);

                        if (movie != null)
                        {
                            movies.Add(movie);
                        }

                    }
                }
                return movies;
            }
            catch (SqlException)
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
                    ReleaseDate = Convert.ToString(reader["released_year"]),
                    Runtime = Convert.ToString(reader["runtime"]),
                    Genre = Convert.ToString(reader["genre"]),
                    Rating = Convert.ToDecimal(reader["imdb_rating"]),
                    Director = Convert.ToString(reader["director"]),
                    Overview = Convert.ToString(reader["overview"]),
                    StarOne = Convert.ToString(reader["star1"]),
                    StarTwo = Convert.ToString(reader["star2"]),
                    StarThree = Convert.ToString(reader["star3"]),
                    StarFour = Convert.ToString(reader["star4"])
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
