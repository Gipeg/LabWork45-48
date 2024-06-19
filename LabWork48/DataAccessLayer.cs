using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//На всякий случай есть документик в папке labWork45-48/labWork48/SQL_запрос.txt
namespace LabWork48
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

        public static void AddAuthor(string lastName, string firstName, string country)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand("AddAuthor", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@Country", country);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static int AddAuthorWithId(string lastName, string firstName, string country)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand("AddAuthorWithId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@Country", country);

                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(idParameter);

                connection.Open();
                command.ExecuteNonQuery();
                return (int)idParameter.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static DataTable GetBooksByPriceRange(decimal minPrice, decimal maxPrice)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                using SqlCommand command = new SqlCommand("GetBooksByPriceRange", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MinPrice", minPrice);
                command.Parameters.AddWithValue("@MaxPrice", maxPrice);

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
    }
}
