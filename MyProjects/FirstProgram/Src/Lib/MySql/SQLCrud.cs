using System.Linq;
using System;
using System.Collections.Generic;

namespace FirstProgram.Src.Lib.MySql
{
    public abstract class SQLCrud : SQLConnector
    {
        
        protected Dictionary <String, String> data = new Dictionary <string, string> ();

        protected void create(String table, Dictionary <String, String> data, bool timestamp = false){
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

            stmt.ExecuteNonQuery();

            Console.WriteLine("Ok");

            
        }

        protected void read( Array data){

        }
        protected void update( Array data){

        }
        protected void delete( Array data){

        }
    }
}