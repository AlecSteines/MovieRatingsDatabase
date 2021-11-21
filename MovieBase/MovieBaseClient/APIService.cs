using MovieBaseClient.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieBaseClient
{
    public class APIService
    {
        private readonly string API_URL = "http://localhost:39410";
        private readonly IRestClient client = new RestClient();

        public List<Movie> GetTop100Movies()
        {
            RestRequest request = new RestRequest(API_URL + "/movie/top/all");

        }
    }
}
