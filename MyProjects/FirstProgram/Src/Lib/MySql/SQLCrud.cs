using System.Linq;
using System;
using System.Collections.Generic;


namespace FirstProgram.Src.Lib.MySql
{
    #nullable enable
    public abstract class SQLCrud : SQLConnector
    {
        
        protected String table = "";
        protected String? statement = null;
        protected String? param = null;
        protected Dictionary <String, Object> data = new Dictionary <string, Object> ();

        public Dictionary <String, Object> Data { get{return this.data;} }

        // Insere um novo registro no banco de dados
        protected long? insert(Dictionary <String, Object> data, bool timestamp = false){
            this.open();
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
            finally{
                this.close();
            }
        }
        

        //

        

        protected void update( Array data){

        }
        protected void delete( Array data){

        }
        

        
    }
    #nullable disable
}
