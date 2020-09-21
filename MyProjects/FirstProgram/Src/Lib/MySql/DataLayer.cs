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
        

        public DataLayer(String tabela, Array? req = null, String primary = "id", bool timestamps = true)
        {
            /* Console.WriteLine(this.Connection.State); */
            this.table = tabela;
            this.primary = primary;
            this.data.Add("name", "GEORGE");
            this.data.Add("price", "111");

            Console.WriteLine(this.save());
            Console.WriteLine("\n-----\n");
            foreach (var item in data){
                Console.WriteLine(item.Key +" - "+ item.Value);
            }
            this.findById("1");

            Console.WriteLine("\n-----\n");
            foreach (var item in data){
                Console.WriteLine(item.Key +" - "+ item.Value);
            }
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

        public DataLayer? findById(string id, string columns = "*"){
            return this.find($"{this.primary} = ?id", id, columns).fetch();
        }

        protected  DataLayer? fetch(bool all = false){
            this.open();
            var stmt = this.Connection.CreateCommand();
        
            stmt.CommandText = $"{this.statement}";
            stmt.Parameters.AddWithValue("@id", param);

            // Executa o cmd SQL e captura os possiveis erros
            try{
                var rows = stmt.ExecuteReader();
                var tempData = new Dictionary <String, Object>();

                while (rows.Read()){
                    for (int i = 0; i < rows.FieldCount; i++){
                        tempData[ rows.GetName(i) ] = rows.GetValue(i);
                    }
                }

                if(tempData.Count == 0) return null;
                
                this.data = tempData;
                return this;
            }
            catch (Exception ex){
                Console.WriteLine(ex);
                return null;
            }
            finally{
                this.close();
            }  
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