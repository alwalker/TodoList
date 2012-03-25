using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    public class Todo
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Done { get; set; }

        public override string ToString()
        {
            return Task;
        }

        public static async Task<ObservableCollection<Todo>> GetAllTodos(TodoDAO dao)
        {
            try
            {
                return await dao.GetTodos();
            }
            catch
            {
                throw new ApplicationException("Error retrieving todo items.");
            }

        }

        public void Complete(TodoDAO dao)
        {
            Done = true;
            dao.SetComplete(this);
        }
    }
}
