using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for AddTodo.xaml
    /// </summary>
    public partial class AddTodo : Window
    {
        private Todo _todo;

        public AddTodo(Todo newTodo)
        {
            InitializeComponent();
            _todo = newTodo;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            _todo = new Todo();
            _todo.Task = txtTask.Text;
            _todo.CreateDate = DateTime.Now;
            _todo.DueDate = dtpDueDate.SelectedDate.Value;

            var dao = new TodoDAO();
            dao.AddTodo(_todo);
            this.DialogResult = true;
            this.Close();
        }
    }
}
