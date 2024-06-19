using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork45
{
    internal class DataAccessLayer
    {
        public static string ConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = @"prserver\SQLEXPRESS",
                    InitialCatalog = "ispp2110",
                    UserID = "ispp2110",
                    Password = "2110",
                    TrustServerCertificate = true
                };
                return builder.ConnectionString;
            }
        }

        public static object ExecuteScalar(string sqlCommand)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sqlCommand, connection);
            connection.Open();
            return command.ExecuteScalar();
        }

        public static DataTable ExecuteQuery(string sqlCommand)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sqlCommand, connection);
            using var adapter = new SqlDataAdapter(command);
            var resultTable = new DataTable();
            adapter.Fill(resultTable);
            return resultTable;
        }
        public class Book
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public double Price { get; set; }
        }
        public static List<Book> GetBooks(string sqlCommand)
        {
            var books = new List<Book>();
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sqlCommand, connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var book = new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Price = reader.GetDouble(2)
                };
                books.Add(book);
            }
            return books;
        }
    }
}

