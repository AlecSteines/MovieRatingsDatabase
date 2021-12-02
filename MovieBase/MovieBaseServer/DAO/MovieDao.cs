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

        public List<List<Movie>> GetMoviesByActor(string actor)
        {
            //Couldn't figure out how to order by difference for multiple columns in one query in sql. 
            //Had to get a bit creative. Made a list of lists. So if you're actor is a supporting or costar, you'll still see those titles as well
            //It definitely isn't pretty, but it works as intended. 
            List<List<Movie>> movies = new List<List<Movie>>();
            List<Movie> mainStar = new List<Movie>();
            List<Movie> coStar = new List<Movie>();
            List<Movie> supportOne = new List<Movie>();
            List<Movie> supportTwo = new List<Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //Main Star
                    SqlCommand cmd = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "WHERE star1 LIKE '%" + actor + "%' ORDER BY Difference(star1, '" + actor + "') desc", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        Movie movie = GetMovieFromReader(reader);

                        if (movie != null)
                        {
                            mainStar.Add(movie);
                        }
                    }
                    reader.Close();
                    movies.Add(mainStar);
                    //Co-Star
                    SqlCommand cmd2 = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "WHERE star2 LIKE '%" + actor + "%' ORDER BY Difference(star2, '" + actor + "') desc", conn);
                    reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = GetMovieFromReader(reader);
                        if(movie != null)
                        {
                            coStar.Add(movie);
                        }
                    }
                    reader.Close();
                    movies.Add(coStar);
                    //Supporting Role One
                    SqlCommand cmd3 = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "WHERE star3 LIKE '%" + actor + "%' ORDER BY Difference(star3, '" + actor + "') desc", conn);
                    reader = cmd3.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = GetMovieFromReader(reader);
                        if (movie != null)
                        {
                            supportOne.Add(movie);
                        }
                    }
                    reader.Close();
                    movies.Add(supportOne);
                    //Supporting Role Two
                    SqlCommand cmd4 = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "WHERE star4 LIKE '%" + actor + "%' ORDER BY Difference(star4, '" + actor + "') desc", conn);
                    reader = cmd4.ExecuteReader();
                    while (reader.Read())
                    {
                        Movie movie = GetMovieFromReader(reader);
                        if (movie != null)
                        {
                            supportTwo.Add(movie);
                        }
                    }
                    reader.Close();
                    movies.Add(supportTwo);
                }
                return movies;
            }
            catch (SqlException)
            {
                throw new Exception();
            }
        }
        public List<Movie> GetMoviesByTitle(string title)
        {
            List<Movie> movies = new List<Movie>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT series_title, released_year, runtime, genre, imdb_rating, overview, director, star1, star2, star3, star4 FROM moviebase " +
                        "WHERE series_title LIKE '%"+title+"%' ORDER BY Difference(series_title, '"+title+"') desc", conn);
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
