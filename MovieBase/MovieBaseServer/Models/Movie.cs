using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBaseServer.Models
{
    public class Movie
    {
        public string Title { get; set; }

        public string ReleaseDate { get; set; }

        public string Runtime { get; set; }

        public string Genre { get; set; }

        public decimal Rating { get; set; }

        public string Director { get; set; }

        public string Overview { get; set; }

        public string StarOne { get; set; }

        public string StarTwo { get; set; }

        public string StarThree { get; set; }

        public string StarFour { get; set; }
    }
}
