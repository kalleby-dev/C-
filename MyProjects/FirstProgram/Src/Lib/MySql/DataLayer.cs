using System.Linq;
using System;
using System.Collections.Generic;

namespace FirstProgram.Src.Lib.MySql
{
    public class DataLayer : SQLCrud
    {
        

        protected String table;
        protected String primary;

        protected bool timestamps;
        protected Array required;
        

        public DataLayer(String table, Array required = null, String primary = "id", bool timestamps = true){
            Console.WriteLine(this.Connection.State);
            data.Add("name", "Livro Legal");
            data.Add("price", "10.50");
            this.create(table, this.data);
        }

        public DataLayer find(){
            return this;
        }


    }
}

/* //Select
Console.WriteLine("Select - Command:");
MySqlCommand stmt = conn.CreateCommand();
stmt.CommandText = "SELECT * FROM book";
stmt.Prepare();
MySqlDataReader result = stmt.ExecuteReader();
while (result.Read())
{
    Console.WriteLine($"Result: id: {result.GetString("id")} name: {result.GetString("name")} price: {result.GetString("price")}");
} */