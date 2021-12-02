using MovieBaseClient.Models;
using System;
using System.Collections.Generic;

namespace MovieBaseClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }
        private static void MainMenu()
        {
            APIService apiService = new APIService();
            ConsoleService consoleService = new ConsoleService();
            ViewHelpers view = new ViewHelpers();
            int menuSelect = -1;

            while (menuSelect != 0)
            {
                Console.WriteLine("MOVIE BASE");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1) Top 100 List");
                Console.WriteLine("2) Top Movies By Category");
                Console.WriteLine("3) Search Movies");
                Console.WriteLine("");
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out menuSelect))
                {
                    Console.Clear();
                    Console.WriteLine("Invalid menu selection. Please select an option from the menu");
                    Console.WriteLine("Select by the number to the left of the menu option(Ex: 1)");
                    Console.WriteLine();
                    MainMenu();
                }
                else if (menuSelect == 1)
                {

                }
                else if (menuSelect == 2)
                {

                }
                //Search by Title
                else if (menuSelect == 3)
                {
                    List<Movie> movieList = new List<Movie>();
                    string key;
                    int searchReset = 0;
                    Console.Clear();
                    do
                    {
                        Console.WriteLine();
                        key = consoleService.SearchByTitle();
                        if (key == "")
                        {
                            Console.Clear();
                            MainMenu();
                        }
                        movieList = apiService.GetMoviesByTitle(key);
                        if (movieList.Count < 1)
                        {
                            Console.WriteLine("I'm sorry, we couldn't find any movie titles matching your search key");
                            Console.WriteLine("");
                        }
                    } while (searchReset == -1);
                    int select;
                    do
                    {
                        Dictionary<int, Movie> searchList = view.MovieView(movieList);
                        select = consoleService.SearchByTitleSelection();
                        if (select == 0)
                        {
                            Console.Clear();
                            MainMenu();
                        }
                        if (searchList.ContainsKey(select))
                        {

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please select a valid input.");
                            select = -1;
                        }
                    } while (select == -1);
                }
            }
        }
    }

}

