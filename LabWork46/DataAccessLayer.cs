using Microsoft.Data.SqlClient;
using System.Data;

namespace LabWork46
{
    internal class DataAccessLayer
    {
        public static string connectionString
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
        public static int ExecuteNonQuery(string sqlCommand)
        {
            using var connection = new SqlConnection(connectionString);
            using var command = new SqlCommand(sqlCommand, connection);
                    connection.Open();
                    return command.ExecuteNonQuery();
        }

        public static bool UpdateBookPrice(int id, double newPrice)
        {
            try
            {
                string sqlCommand = $"UPDATE Books SET Price = @Price WHERE Id = @Id";
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(sqlCommand, connection);
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Price", newPrice);
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static DataTable SelectAllFromTable(string tableName)
        {
            try
            {
                string sqlCommand = $"SELECT * FROM {tableName}";
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(sqlCommand, connection);
                using var adapter = new SqlDataAdapter(command);
                var resultTable = new DataTable();
                adapter.Fill(resultTable);
                return resultTable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static void UpdateTable(ref DataTable dataTable, string tableName)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                using var adapter = new SqlDataAdapter($"SELECT * FROM {tableName}", connection);
                using var builder = new SqlCommandBuilder(adapter);
                    adapter.Update(dataTable);
                    dataTable.Clear();
                    adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
