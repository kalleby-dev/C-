using System.Linq;
using System;
using System.Collections.Generic;

namespace FirstProgram.Src.Lib.MySql
{
    public class DataLayer : SQLCrud
    {
        

        protected String table;
        protected String primary = null;

        protected bool timestamps;
        protected Array required;
        

        public DataLayer(String table, Array required = null, String primary = "id", bool timestamps = true)
        {
            Console.WriteLine(this.Connection.State);
            this.table = table;
            this.data.Add("name", "Tiago Pinto");
            this.data.Add("price", "13.2");
            Console.WriteLine(this.save());
            this.read(this.table);

        }

        public DataLayer find(){
            return this;
        }

        public bool save()
        {
            var primary = this.primary;
            string id = null;

            // Criar um novo
            if(this.data.ContainsKey("primary")){
                //Update here
            }

            if(!this.data.ContainsKey("primary")){
                id = this.create(this.table, this.data).ToString();
            }
            
            if(id == null) return false;

            return true;
        }

        public DataLayer? findById(long id){

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