using System.Linq;
using System;
using System.Collections.Generic;

namespace FirstProgram.Src.Lib.MySql
{
    public abstract class SQLCrud : SQLConnector
    {
        
        protected Dictionary <String, String> data = new Dictionary <string, string> ();

        protected long? create(String table, Dictionary <String, String> data, bool timestamp = false){
            var sqlColumns = "";
            var sqlValues = "";

            var last = data.Keys.Last();
            foreach (var item in data)
            {
                sqlColumns += (item.Key.Equals(last))? $"{item.Key}" : $"{item.Key},";
                sqlValues += (item.Key.Equals(last))? $"?{item.Key}" : $"?{item.Key},";
            }

            var stmt = this.Connection.CreateCommand();
            stmt.CommandText = $"INSERT INTO {table}({sqlColumns}) VALUES ({sqlValues})";

            foreach (var item in data)
            {
                stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            }

            try
            {
                stmt.ExecuteNonQuery();
                return stmt.LastInsertedId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
                
            }
        }

        protected DataLayer? read(String table, String terms = null, String param = null, String columns = "*"){
            var stmt = this.Connection.CreateCommand();
            
            stmt.CommandText = $"SELECT {columns} FROM {table}";
            stmt.Prepare();
            var result = stmt.ExecuteReader();

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