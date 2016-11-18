using System.Data.Entity;

namespace ToDoList
{
    public class ToDoContext : DbContext
    {
        public ToDoContext()
            : base("ToDoContext")
        {
        }

        public virtual DbSet<Item> Items { get; set; }
    }
}
