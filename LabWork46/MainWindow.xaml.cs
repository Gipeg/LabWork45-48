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

namespace LabWork46
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataTable _dataTable;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ExecuteNonQueryButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlCommand = NonQueryCommandTextBox.Text;
            int rowsAffected = DataAccessLayer.ExecuteNonQuery(sqlCommand);
            NonQueryResultTextBlock.Text = rowsAffected >= 0 ? $"{rowsAffected} rows affected" : "Error executing command";
        }

        private void UpdatePriceButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(BookIdTextBox.Text, out int id) && double.TryParse(NewPriceTextBox.Text, out double newPrice))
            {
                bool success = DataAccessLayer.UpdateBookPrice(id, newPrice);
                UpdatePriceResultTextBlock.Text = success ? "Price updated successfully" : "Error updating price";
            }
            else
            {
                UpdatePriceResultTextBlock.Text = "Invalid input";
            }
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            string tableName = TableNameTextBox.Text;
            _dataTable = DataAccessLayer.SelectAllFromTable(tableName);
            QueryResultDataGrid.ItemsSource = _dataTable?.DefaultView;
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dataTable != null)
            {
                string tableName = TableNameTextBox.Text;
                DataAccessLayer.UpdateTable(ref _dataTable, tableName);
                QueryResultDataGrid.ItemsSource = _dataTable.DefaultView;
            }
        }
    }
}