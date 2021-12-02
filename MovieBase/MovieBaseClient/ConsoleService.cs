using MovieBaseClient.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieBaseClient
{
    public class ConsoleService
    {
        public string SearchByTitle()
        {
            Console.WriteLine("--Leave Blank and Press Enter to Return To Main Menu");
            Console.Write("Search By Title: ");
            string searchTerm = Console.ReadLine();
            return searchTerm;
        }
        
        public int SearchByTitleSelection()
        {
            Console.WriteLine("--Enter 0 to return to main menu");
            Console.Write("Select a Movie By Numeric Ranking: ");
            int select;
            if (!int.TryParse(Console.ReadLine(), out select))
            {
                return -1;
            }
            else
            {
                return select;
            }
        }

    }
}
