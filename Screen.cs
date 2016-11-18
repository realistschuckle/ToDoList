using System;
using System.Collections.Generic;

namespace ToDoList
{
    public class Screen
    {
        public string PromptForFileName()
        {
            Console.Clear();
            DrawHeader("EXPORT ITEMS");
            Console.WriteLine();
            Console.WriteLine("What's the name of the file that you'd like to export this to?");
            Console.Write("File name: ");
            return Console.ReadLine();
        }

        public int PromptForItemToComplete(List<Item> items)
        {

            while (true)
            {
                try
                {
                    Console.Clear();
                    DrawListWithHeader("ACTIVE ITEMS", items);
                    Console.WriteLine();
                    Console.Write("Mark which item complete? ");
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                }
            }
        }

        public void DrawItems(string header, List<Item> items)
        {
            DrawListWithHeader(header, items);
            Console.WriteLine();
            Console.WriteLine("Press ENTER to return to the menu.");
            Console.ReadLine();
        }

        public string PromptForNewListItem()
        {
            string description = "";

            while (description.Length == 0)
            {
                Console.Clear();
                DrawHeader("NEW TO-DO ITEM");
                Console.WriteLine();
                Console.WriteLine("Enter the new to-do item:");
                description = Console.ReadLine();
            }

            return description;
        }

        public void DrawMenu()
        {
            Console.Clear();
            DrawHeader("TO-DO MAIN MENU");
            Console.WriteLine("1. Create to-do list item");
            Console.WriteLine("2. List active to-do items");
            Console.WriteLine("3. List completed to-do items");
            Console.WriteLine("4. Complete an active item");
            Console.WriteLine("5. Export all items to a file");
            Console.WriteLine();
            Console.WriteLine("0. Exit");
            Console.WriteLine();
        }

        public UserResponse PromptForMenuChoice()
        {
            Console.Write("What option would you like to do? ");
            int option = int.Parse(Console.ReadLine());
            return (UserResponse)option;
        }

        private void DrawListWithHeader(string header, List<Item> items)
        {
            Console.Clear();
            DrawHeader(header);
            foreach (Item item in items)
            {
                Console.WriteLine($"{item.Id,4} {item.Description}");
            }
        }

        private void DrawHeader(string header)
        {
            int numberOfStars = (Console.WindowWidth - header.Length - 3) / 2;
            string headerStars = new string('*', numberOfStars);
            string starredText = $"{headerStars} {header} {headerStars}";
            while (starredText.Length < Console.WindowWidth - 1)
            {
                starredText += "*";
            }
            Console.WriteLine(starredText);
        }
    }
}
