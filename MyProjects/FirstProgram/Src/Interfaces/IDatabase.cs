using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FirstProgram.Src.Interfaces
{
    public class IDatabase
    {
        MySqlConnectionStringBuilder connBuilder = new MySqlConnectionStringBuilder();
        MySqlConnection conn = null;
        public IDatabase(){
            connBuilder.Server = "localhost";
            connBuilder.UserID = "root";
            connBuilder.Password = "";
            connBuilder.Database = "teste";

            conn = new MySqlConnection(connBuilder.ConnectionString);

            Console.WriteLine("Starting connection...");
            try
            {
                conn.Open();
                Console.WriteLine($"Status: {conn.State}");
                
                Console.WriteLine("Select - Command:");
                MySqlCommand stmt = conn.CreateCommand();
                stmt.CommandText = "SELECT * FROM book";
                stmt.Prepare();
                /* stmt.ExecuteNonQuery(); */
                MySqlDataReader result = stmt.ExecuteReader();
                while (result.Read())
                {
                    Console.WriteLine($"Result: id: {result.GetString("id")} name: {result.GetString("name")} price: {result.GetString("price")}");
                }
                
                

                conn.Close();
                Console.WriteLine($"Status: {conn.State}");
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Status: {conn.State}");
            }

            Console.WriteLine("\n\n");
        }
    }
}