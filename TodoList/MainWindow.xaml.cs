using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Todo> _todos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Todo newTodo = new Todo();
            var add = new AddTodo(newTodo);
            if (add.ShowDialog().Value)
            {
                _todos.Add(newTodo);
                lstTodos.InvalidateVisual();
            }
        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var dao = new TodoDAO();
            _todos = await Todo.GetAllTodos(dao);
            lstTodos.ItemsSource = _todos;
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            File.Copy("TodoList.s3db", @"..\..\..\TodoList.s3db", true);
        }

        private void chkDone_Checked_1(object sender, RoutedEventArgs e)
        {
            var dao = new TodoDAO();
            var todo = ((e.Source as CheckBox).DataContext as Todo);
            if (todo != null)
            {
                todo.Complete(dao);
            }
        }

        private void chkDone_Unchecked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
