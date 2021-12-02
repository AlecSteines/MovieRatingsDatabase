using MovieBaseClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieBaseClient
{
    class ViewHelpers
    {
        public Dictionary<int, Movie> MovieView(List<Movie> movies)
        {
            Console.WriteLine();
            Dictionary<int, Movie> movieDictionary = new Dictionary<int, Movie>();
            int i = 1;
            foreach (Movie movie in movies)
            {
                movieDictionary.Add(i, movie);
                Console.WriteLine($"({i})| {movie.Title} | Rating : {movie.Rating} |");
                i++;
            }
            Console.WriteLine();
            return movieDictionary;
        }
    }
}
