using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Simple.Data;
using System.IO;
using System.Reflection;

namespace TodoList
{
    public class TodoDAO
    {
        public void AddTodo(Todo todo)
        {
            var db = Database.Open();
            db.TODOs.Insert(todo);
        }

        public IList<Todo> GetTodos()
        {
            var db = Database.Open();
            IList<Todo> todos = db.TODOs.All().ToList<Todo>();
            return todos;
        }
    }
}
