using System.Linq;
using System;
using System.Collections.Generic;

#nullable enable
namespace FirstProgram.Src.Lib.MySql
{
    public class DataLayer : SQLCrud
    {
        
        
        protected String? primary = null;

        protected bool timestamps;
        protected Array required;
        

        public DataLayer(String table, Array? req = null, String primary = "id", bool timestamps = true)
        {
            Console.WriteLine(this.Connection.State);
            this.table = table;
            this.primary = primary;
            this.data.Add("name", "Veronica");
            this.data.Add("price", "60");

            //Console.WriteLine(this.save());
            this.findById(1);

        }

        public DataLayer find(String? terms = null, String? param = null, String columns = "*"){
            if(terms != null){
                this.statement = $"SELECT {columns} FROM {this.table} WHERE {terms}";
                this.param = param;
                return this;
            }

            this.statement = $"SELECT {columns} FROM {this.table}";
            return this;
        }

        public DataLayer? findById(int id, string columns = "*"){
            

            return this.find($"{this.primary} = ?id", $"?id = {id}", columns).fetch();
        }

        public bool save()
        {
            var primary = this.primary;
            string? id = null;

            // Criar um novo
            if(this.data.ContainsKey("primary")){
                //Update here
            }

            if(!this.data.ContainsKey("primary")){
                id = this.insert(this.data).ToString();
            }
            
            if(id == null) return false;

            return true;
        }

        
        public DataLayer? findById(long id){

            return this;
        }
        

    }
}
#nullable disable

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