using System;
using System.Collections.Generic;
using System.Text;

namespace MovieBaseClient.Models
{
    public class Movie
    {
        public string Title { get; set; }

        public string ReleaseDate { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public decimal Rating { get; set; }

        public string Director { get; set; }

        public Movie()
        {

        }
    }
}
