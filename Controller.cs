using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ToDoList
{
    public class Controller
    {
        /// <summary>
        /// The screen prompts the user for input and writes output
        /// to the screen.
        /// </summary>
        private Screen screen;
        private string saveDirectory;

        /// <summary>
        /// Create a new instance of the to-do list controller.
        /// This runs the to-do-list app.
        /// </summary>
        public Controller()
        {
            screen = new Screen();

            string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveDirectory = Path.Combine(myDocuments, "to-do");
        }

        /// <summary>
        /// This method runs the controller, in effect, running the
        /// specific to-do list.
        /// </summary>
        public void Run()
        {
            UserResponse selection = DoMenu();
            while (selection != UserResponse.Exit)
            {
                if (selection == UserResponse.CreateListItem)
                {
                    AddNewListItem();
                }
                else if (selection == UserResponse.ListActiveItems)
                {
                    ShowActiveItems();
                }
                else if (selection == UserResponse.ListCompletedItems)
                {
                    ShowCompletedItems();
                }
                else if (selection == UserResponse.CompleteItem)
                {
                    CompleteItem();
                }
                else if (selection == UserResponse.ExportItems)
                {
                    ExportItems();
                }
                selection = DoMenu();
            }
        }

        private void ExportItems()
        {
            Directory.CreateDirectory(saveDirectory);
            string fileName = screen.PromptForFileName();
            string fullFileName = Path.Combine(saveDirectory, fileName);

            using (ToDoContext context = new ToDoContext())
            {
                using (StreamWriter writer = File.CreateText(fullFileName))
                {
                    writer.WriteLine("Description,Is Completed");
                    foreach(Item item in context.Items)
                    {
                        writer.WriteLine($"{item.Description},{item.IsCompleted}");
                    }
                }
            }
        }

        private void CompleteItem()
        {
            using (ToDoContext context = new ToDoContext())
            {
                List<Item> items = context.Items.Where(item => !item.IsCompleted).ToList();
                int id = screen.PromptForItemToComplete(items);

                Item completedItem = context.Items.Where(item => item.Id == id).SingleOrDefault();
                if (completedItem != null)
                {
                    completedItem.IsCompleted = true;
                    context.SaveChanges();
                }
            }
        }

        private void ShowCompletedItems()
        {
            using (ToDoContext context = new ToDoContext())
            {
                List<Item> items = context.Items.Where(item => item.IsCompleted).ToList();
                screen.DrawItems("COMPLETED ITEMS", items);
            }
        }

        private void ShowActiveItems()
        {
            using (ToDoContext context = new ToDoContext())
            {
                List<Item> items = context.Items.Where(item => !item.IsCompleted).ToList();
                screen.DrawItems("ACTIVE ITEMS", items);
            }
        }

        private void AddNewListItem()
        {
            string itemDescription = screen.PromptForNewListItem();

            using (ToDoContext context = new ToDoContext())
            {
                Item item = new Item();
                item.Description = itemDescription;
                context.Items.Add(item);
                context.SaveChanges();
            }
        }

        private UserResponse DoMenu()
        {
            screen.DrawMenu();
            return screen.PromptForMenuChoice();
        }
    }
}
