using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Simple.Data;
using System.IO;
using System.Reflection;
using System.Data;

namespace TodoList
{
    public class TodoDAO
    {
        public void AddTodo(Todo todo)
        {
            var db = Database.Open();
            db.TODOs.Insert(todo);
        }

        public async Task<List<Todo>> GetTodos()
        {
            using (var conn = new SQLiteConnection("Data Source=TodoList.s3db"))
            {
                using (var cmd = new SQLiteCommand("SELECT * FROM TODOs", conn))
                {
                    await conn.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        var todos = new List<Todo>();
                        Todo todo = null;
                        while (reader.Read())
                        {
                            todo = new Todo();
                            todo.Id = Convert.ToInt32(reader["Id"].ToString());
                            todo.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            todo.Task = reader["Task"].ToString();
                            todo.DueDate = reader["DueDate"] == DBNull.Value ?
                                null as DateTime? : Convert.ToDateTime(reader["DueDate"].ToString());
                            todos.Add(todo);
                        }
                        return todos;
                    }
                }
            }
        }
    }
}
