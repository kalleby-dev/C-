using System.Globalization;
using System.Linq;
using System;
using System.Collections.Generic;

#nullable enable
namespace FirstProgram.Src.Lib.MySql
{
    public class DataLayer : SQLCrud
    {
        
        
        protected String primary;
        protected bool timestamps;
        protected String?[] required {get; set;} = null!;

        protected List<Dictionary<String, Object>> list = new List<Dictionary<string, object>> ();
        public List<Dictionary<String, Object>> GetFetch {get{return this.list;}}
        

        public DataLayer(String table, String?[] req = null!, String primary = "id", bool timestamps = true)
        {
            /* Console.WriteLine(this.Connection.State); */
            this.table = table;
            this.primary = primary;
            this.required = req ?? new string[0];

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

        // Cria uma string de consulta com base nos parametros informados
        public DataLayer find(String? terms = null, Dictionary <string, Object>? param = null, String columns = "*"){
            if(terms != null){
                this.statement = $"SELECT {columns} FROM {this.table} WHERE {terms}";
                this.param = param;
                return this;
            }

            this.statement = $"SELECT {columns} FROM {this.table}";
            return this;
        }

        public DataLayer findById(string id, string columns = "*"){
            var param = new Dictionary <string, Object>();
            param["?id"] = id;

            return this.find($"{this.primary} = ?id", param, columns).fetch();
        }

        //USER: aluno
        //PASS: aluno@escola

        public DataLayer fetch(bool all = false){

            try
            {
                var rows = this.read();
                if(!all){
                    this.data = rows.First();
                    this.list = rows;
                }
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            return this;
           /*  if(rows == null || rows.Count == 0) return null;   */
            //foreach (var item in rows){}
        }


        public bool save()
        {

            string? id = null;
            try{
                // Criar um novo
                if(this.data.ContainsKey(this.primary)){
                    //this.update();
                }

                if(!this.data.ContainsKey(this.primary)){
                    id = this.insert(this.data).ToString();
                }
                
                if(id == null) return false;

                
                this.data = this.findById(id).Data;
                return true;
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}");
                return false;
            }

        }


        public void set(String column, Object value){
            this.data[column] = value;
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