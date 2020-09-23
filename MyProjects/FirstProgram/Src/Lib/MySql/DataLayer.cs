using System.Globalization;
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

        protected List<Dictionary<String, Object>> list;
        public List<Dictionary<String, Object>> GetFetch {get{return this.list;}}
        

        public DataLayer(String tabela, Array? req = null, String primary = "id", bool timestamps = true)
        {
            /* Console.WriteLine(this.Connection.State); */
            this.table = tabela;
            this.primary = primary;

/*             Console.WriteLine(this.save());
            Console.WriteLine("\n-----\n");
            foreach (var item in data){
                Console.WriteLine(item.Key +" - "+ item.Value);
            } */
/*             var p = new Dictionary <string, Object>();
            p["?name"] = "GEORGE";
            this.find("name = ?name", p).fetch();

            Console.WriteLine("\n-----\n");
            foreach (var item in data){
                Console.WriteLine(item.Key +" - "+ item.Value);
            } */
        }

        public DataLayer find(String? terms = null, Dictionary <string, Object>? param = null, String columns = "*"){
            if(terms != null){
                this.statement = $"SELECT {columns} FROM {this.table} WHERE {terms}";
                this.param = param;
                return this;
            }

            this.statement = $"SELECT {columns} FROM {this.table}";
            return this;
        }

        public DataLayer? findById(string id, string columns = "*"){
            var param = new Dictionary <string, Object>();
            param["?id"] = id;

            return this.find($"{this.primary} = ?id", param, columns).fetch();
        }



        public DataLayer? fetch(bool all = false){

            var rows = this.read();
            if(rows == null || rows.Count == 0) return null;  

            if(!all){
                this.data = rows.First();
            }
            
            //foreach (var item in rows){}
            this.list = rows;
            return this;
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

            try{
                this.data = this.findById(id).Data;
                return true;
            }
            catch (Exception ex){
                Console.WriteLine(ex);
                return false;
            }

        }

        
/*         public DataLayer? findById(long id){

            return this;
        } */
        

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