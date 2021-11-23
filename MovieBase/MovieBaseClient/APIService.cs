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
            IRestResponse<List<Movie>> response = client.Get<List<Movie>>(request);
            
            if(response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("Server side error has occured. Server response not complete");
                return null;
            }
            else if(!response.IsSuccessful)
            {
                Console.WriteLine("An error response was received from the server. Status Code: " + (int)response.StatusCode);
                return null;
            }
            else
            {
                return response.Data;
            }
        }

        public List<Movie> GetTop15ByGenre(string genre)
        {
            RestRequest request = new RestRequest(API_URL + "/movie/top/" + genre);
            IRestResponse<List<Movie>> response = client.Get<List<Movie>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                Console.WriteLine("Server side error has occured. Server response not complete");
                return null;
            }
            else if (!response.IsSuccessful)
            {
                Console.WriteLine("An error response was received from the server. Status Code: " + (int)response.StatusCode);
                return null;
            }
            else
            {
                return response.Data;
            }
        }
    }
}
