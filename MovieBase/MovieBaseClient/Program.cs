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
            //console service - used to make menu selection more minimalistic for the most part
            ConsoleService consoleService = new ConsoleService();
            //viewhelpers - used to make lists of movie viewable to user
            ViewHelpers view = new ViewHelpers();
            int menuSelect = -1;

            while (menuSelect != 0)
            {
                Console.WriteLine("MOVIE BASE");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("1) Top 100 List");
                Console.WriteLine("2) Top Movies By Category");
                Console.WriteLine("3) Search Movies");
                Console.WriteLine("0) Exit Movie Base");
                Console.WriteLine("-------------------------------------------");
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out menuSelect))
                {
                    //clear console and return to main menu if selection invalid
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
                    //string for user search key
                    string key;
                    //integer used to go back to search by title method in case of invalid input, or no movies found
                    int searchReset = 0;
                    Console.Clear();
                    do
                    {
                        //search by title function
                        Console.WriteLine();
                        key = consoleService.SearchByTitle();
                        if (key == "")
                        {
                            Console.Clear();
                            MainMenu();
                        }
                        //api call based on search key, creates list of movies from search
                        movieList = apiService.GetMoviesByTitle(key);
                        if (movieList.Count < 1)
                        {
                            Console.WriteLine("I'm sorry, we couldn't find any movie titles matching your search key");
                            Console.WriteLine("");
                        }
                    } while (searchReset == -1);
                    //place holder for select so that it isn't out of scope later
                    int select;
                    do
                    {
                        //Function creates dictionary of movies from list so that user can select by numeric ranking
                        //also outputs movies to console for users to select/view
                        Dictionary<int, Movie> searchList = view.MovieView(movieList);
                        select = consoleService.SearchByTitleSelection();
                        if (select == 0)
                        {
                            //return to main menu based on user choice
                            Console.Clear();
                            MainMenu();
                        }
                        if (searchList.ContainsKey(select))
                        {

                        }
                        else
                        {
                            //if given invalid input, allow user to go back to select new option
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

