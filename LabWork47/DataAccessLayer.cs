using System.Data;
using Microsoft.Data.SqlClient;

namespace LabWork47
{
    class DataAccessLayer
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

        public static int GetBookCountByPrice(decimal price)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT COUNT(*) FROM Books WHERE Price < @Price";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Price", price);
                connection.Open();
                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static int InsertBookAndGetId(string insertCommand)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                string query = insertCommand + "; SET @id = SCOPE_IDENTITY()";
                using SqlCommand command = new SqlCommand(query, connection);
                SqlParameter idParameter = new SqlParameter("@id", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParameter);

                connection.Open();
                command.ExecuteNonQuery();
                return (int)idParameter.Value;
            }
            catch
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static DataTable GetBooksByPriceAndGenre(decimal price, string genre)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT * FROM Books WHERE Price < @Price AND Genre = @Genre";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Genre", genre);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool UpdateBook(int id, decimal price, string title)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                string query = "UPDATE Books SET Price = @Price, Title = @Title WHERE Id = @Id";
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Title", title);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
