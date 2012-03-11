using System;
using System.Collections.Generic;
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

        public override string ToString()
        {
            return Task;
        }
    }
}
