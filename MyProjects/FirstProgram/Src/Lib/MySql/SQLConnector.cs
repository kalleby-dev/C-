using System;
using MySql.Data.MySqlClient;

namespace FirstProgram.Src.Lib.MySql
{
    public abstract class SQLConnector
    {   
        private MySqlConnection conn = null;
        protected MySqlException error = null;
        protected MySqlConnection Connection {get {return conn;} }

        public SQLConnector(String host = "localhost", String database = "teste", String user = "root", String pass = null)
        {
            this.open(host, database, user, pass);
        }



    ///<summary>Abre uma conexão com o banco de dados</summary>
        public void open(String host = null, String database = null, String user = null, String pass = null)
        {
            host = (host != null)? host : "localhost";
            user = (user != null)? user : "root";
            pass = (pass != null)? pass : "";

            this.conn = new MySqlConnection($"Server={host}; UID={user}; password={pass}");
            
            try{
                conn.Open();
                if(database != null) conn.ChangeDatabase(database);

                //Console.WriteLine($"Connection: {conn.State}");
            }
            catch(MySqlException ex){
                this.error = ex;
                //Console.WriteLine(ex.Message);
            }
        }
    


    ///<summary>Encerra a conexão com o banco de dados</summary>
        public void close(){
            try{
                conn.Close();
                //Console.WriteLine($"Connection: {conn.State}");
            }
            catch(MySqlException ex){
                this.error = ex;
                //Console.WriteLine(ex.Message);
                //Console.WriteLine($"Connection: {conn.State}");
            }
        }
    }
}