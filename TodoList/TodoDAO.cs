using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TodoList
{
    public class TodoDAO
    {
        public async void AddTodo(Todo todo)
        {
            using (var conn = new SQLiteConnection("Data Source=TodoList.s3db"))
            {
                var sb = new StringBuilder();
                sb.Append("INSERT INTO TODOs (Task, DueDate, CreateDate, Done) VALUES ('");
                sb.Append(todo.Task);
                sb.Append("', '");
                sb.Append(todo.DueDate.Value.ToString("yyyy-MM-dd HH:mm"));
                sb.Append("', '");
                sb.Append(todo.CreateDate.ToString("yyyy-MM-dd HH:mm"));
                sb.Append("', ");
                sb.Append(todo.Done ? 1 : 0);
                sb.Append(");");

                Debug.WriteLine("Executing: " + sb.ToString());

                using (var cmd = new SQLiteCommand(sb.ToString(), conn))
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<ObservableCollection<Todo>> GetTodos()
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
                            return new ObservableCollection<Todo>();
                        }

                        var todos = new ObservableCollection<Todo>();
                        Todo todo = null;
                        while (reader.Read())
                        {
                            todo = new Todo();
                            todo.Id = Convert.ToInt32(reader["Id"].ToString());
                            todo.CreateDate = Convert.ToDateTime(reader["CreateDate"].ToString());
                            todo.Task = reader["Task"].ToString();
                            todo.Done = Convert.ToBoolean(reader["Done"].ToString());
                            todo.DueDate = reader["DueDate"] == DBNull.Value ?
                                null as DateTime? : Convert.ToDateTime(reader["DueDate"].ToString());
                            todos.Add(todo);
                        }
                        return todos;
                    }
                }
            }
        }

        public async void SetComplete(Todo todo)
        {
            using (var conn = new SQLiteConnection("Data Source=TodoList.s3db"))
            {
                using (var cmd = new SQLiteCommand("UPDATE TODOs SET Done = 1 WHERE Id = " + todo.Id, conn))
                {
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
