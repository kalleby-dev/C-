using System.Linq;
using System;
using System.Collections.Generic;

#nullable enable
namespace FirstProgram.Src.Lib.MySql
{
    
    public abstract class SQLCrud : SQLConnector
    {
        
        protected String? table = null;
        protected String? statement = null;
        protected String? param = null;
        protected Dictionary <String, String> data = new Dictionary <string, string> ();

        
        // Insere um novo registro no banco de dados
        protected long? insert(Dictionary <String, String> data, bool timestamp = false){
            var stmt = this.Connection.CreateCommand();

            // Cria o cmd SQL dinamicamente
            var sqlColumns = String.Join(",", data.Keys);
            var sqlParam = String.Join(",",  (data.Keys).Select( value => String.Format($"?{value}")) );
            stmt.CommandText = $"INSERT INTO {table}({sqlColumns}) VALUES ({sqlParam})";

            // Prepara a statement com os valores
            foreach (var item in data) stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            
            // Executa o cmd SQL e captura os possiveis erros
            try{
                stmt.ExecuteNonQuery();
                Console.WriteLine("ok");
                return stmt.LastInsertedId;
            }
            catch (Exception ex){
                Console.WriteLine(ex);
                return null;
            }
        }
        

        //
        protected DataLayer? fetch(bool all = false){
            var stmt = this.Connection.CreateCommand();
            
            Console.WriteLine(this.statement);
            //Console.WriteLine(this.param);

/*             stmt.CommandText = $"{this.statement} {this.param}";
            stmt.Parameters.AddWithValue("@id", '1');
            // Executa o cmd SQL e captura os possiveis erros
            try{
                var result = stmt.ExecuteReader();
                while (result.Read())
                    Console.WriteLine($"id: {result.GetString("id")}\n name: {result.GetString("name")}\n price: {result.GetString("price")}");
            }
            catch (Exception ex){
                Console.WriteLine(ex);
            }
            Console.WriteLine("finish"); */
            stmt.CommandText = $"{this.statement}";
            //stmt.CommandText = $"SELECT * FROM {table} WHERE id = ?id";
            
            stmt.Parameters.AddWithValue("?id", "1");
            
            var result = stmt.ExecuteReader();
            Console.WriteLine(stmt.CommandText);
            while (result.Read())
            {
                Console.WriteLine($"Result: id: {result.GetString("id")} name: {result.GetString("name")} price: {result.GetString("price")}");
            }

            return null;
        }
        

        protected void update( Array data){

        }
        protected void delete( Array data){

        }
        

        
    }
}
#nullable disable