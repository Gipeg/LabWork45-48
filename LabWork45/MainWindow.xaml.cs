using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LabWork45.DataAccessLayer;

namespace LabWork45
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public string ConnectionString => DataAccessLayer.ConnectionString;

        private void ExecuteScalarButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlCommand = ScalarCommandTextBox.Text;
            object result = DataAccessLayer.ExecuteScalar(sqlCommand);
            ScalarResultTextBlock.Text = result?.ToString() ?? "No result";
        }

        private void ExecuteQueryButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlCommand = QueryCommandTextBox.Text;
            DataTable resultTable = DataAccessLayer.ExecuteQuery(sqlCommand);
            QueryResultDataGrid.ItemsSource = resultTable?.DefaultView;
        }

        private void GetBooksButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlCommand = BooksCommandTextBox.Text;
            List<Book> books = DataAccessLayer.GetBooks(sqlCommand);
            BooksListView.ItemsSource = books;
        }
    }
}