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

namespace LabWork48
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = LastNameTextBox.Text;
            string firstName = FirstNameTextBox.Text;
            string country = CountryTextBox.Text;

            DataAccessLayer.AddAuthor(lastName, firstName, country);
            AddAuthorResultTextBlock.Text = "Автор успешно добавлен";
        }

        private void AddAuthorWithIdButton_Click(object sender, RoutedEventArgs e)
        {
            string lastName = LastNameTextBox2.Text;
            string firstName = FirstNameTextBox2.Text;
            string country = CountryTextBox2.Text;

            int id = DataAccessLayer.AddAuthorWithId(lastName, firstName, country);
            AddAuthorWithIdResultTextBlock.Text = id >= 0 ? $"id Автора: {id}" : "Ошибка при добавлении автора.";
        }

        private void GetBooksButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(MinPriceTextBox.Text, out decimal minPrice) && decimal.TryParse(MaxPriceTextBox.Text, out decimal maxPrice))
            {
                DataTable books = DataAccessLayer.GetBooksByPriceRange(minPrice, maxPrice);
                BooksDataGrid.ItemsSource = books?.DefaultView;
            }
            else
            {
                MinPriceTextBox.Text = "Введены неправильно данные";
                MaxPriceTextBox.Text = "Введены неправильно данные";
            }
        }
    }
}