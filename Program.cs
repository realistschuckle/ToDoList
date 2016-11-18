using System.Data.Entity;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<ToDoContext>(null);
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
