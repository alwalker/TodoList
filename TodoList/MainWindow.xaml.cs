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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TodoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Todo> _todos = new List<Todo>();

        public MainWindow()
        {
            InitializeComponent();
            lstTodos.ItemsSource = _todos;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Todo newTodo = null;
            var add = new AddTodo(newTodo);
            if (add.ShowDialog().Value)
            {
                _todos.Add(newTodo);
            }
        }

        private async void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            var dao = new TodoDAO();
            var todos = await dao.GetTodos();
            lstTodos.ItemsSource = todos;
        }
    }
}
