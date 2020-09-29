using System;
using System.Linq;
using System.Collections.Generic;


namespace FirstProgram.Src.Lib.MySql
{
    #nullable enable
    public abstract class SQLCrud : SQLConnector
    {

        ///<summary>Insere um novo registro no banco de dados retorna o ID do registro</summary>
        protected long insert(String table, Dictionary <String, Object> data, bool timestamp = false){
            base.open();
            var stmt = base.Connection.CreateCommand();

            // Cria o cmd de Inserção dinamicamente
            var sqlColumns = String.Join(",", data.Keys);
            var sqlParam = String.Join(",",  (data.Keys).Select( value => String.Format($"?{value}")) );
            stmt.CommandText = $"INSERT INTO {table}({sqlColumns}) VALUES ({sqlParam})";

            // Prepara a statement com os valores
            foreach (var item in data){
                stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            }
            
            try{
                // Executa a insersão e retorna o ID do registro
                stmt.ExecuteNonQuery();
                return stmt.LastInsertedId;
            }
            catch (Exception ex){
                Console.WriteLine($"CRUD::SQL-ERROR: {ex.Message}");
                throw new Exception("Não foi possivel efetuar o registro");
            }
            finally{
                base.close();
            }
        }
        

        ///<summary>Realiza uma busca no banco de dados e retorna uma lista com as linhas da tabela</summary>
        protected List<Dictionary<String, Object>> read(String statement, Dictionary <String, Object>? param = null){
            base.open();

            // Carrega o cmd SQL para efetuar a consulta
            var stmt = base.Connection.CreateCommand();
            stmt.CommandText = $"{statement}";
            
            // Caso hajam parametros irá carregar os valores na stmt 
            if(param != null){
                foreach (var item in param){
                    stmt.Parameters.AddWithValue(item.Key, item.Value);  
                } 
            }

            try{
                // Executa a busca no banco de dados
                var rows = stmt.ExecuteReader(); 
                var list = new List<Dictionary <String, Object>> ();

                while (rows.Read()){
                    // Faz a leitura dos valores recebidos
                    var tempData = new Dictionary <String, Object>();
                    for (int i = 0; i < rows.FieldCount; i++){
                        tempData[ rows.GetName(i) ] = rows.GetValue(i);
                    }

                    // Armazena o registro em memoria
                    list.Add(tempData); 
                }
                // Retorna uma lista com todas os registros encontrados
                return list;
            }
            catch (Exception ex){
                Console.WriteLine($"CRUD::SQL-ERROR: {ex.Message}");
                throw new NullReferenceException("Não foi possivel efetuar a busca");
            }
            finally{
                base.close();
            }  
        }

        
        ///<summary>Atualiza informações de um registro no banco de dados</summary>
        protected bool update(String table, String terms, Dictionary<String, Object> data, Dictionary<String, Object>? param = null){
            base.open();
            
            var stmt = base.Connection.CreateCommand();
            
            // Cria o cmd de Alteração dinamicamente
            var sqlColumns = String.Join(",", data.Keys);
            var sqlParam = String.Join(", ",  (data.Keys).Select( key => String.Format($"{key} = ?{key}")) );
            stmt.CommandText = $"UPDATE {table} SET {sqlParam} {terms}";

            // Prepara a statement com os valores
            foreach (var item in data){
                stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            }

            // Prepara a statement com as condições
            if(param != null){
                foreach (var item in param){
                    stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
                }  
            }

            try{
                // Executa a insersão e retorna o ID do registro
                stmt.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex){
                Console.WriteLine($"CRUD::SQL-ERROR: {ex.Message}");
                return false;
                throw new Exception("Não foi possivel efetuar o registro");
            }
            finally{
                base.close();
            }
        }

        protected bool delete(String table, String terms, Dictionary<String, Object> param){
            base.open();
            
            // Cria o comando SQL para remoção
            var stmt = base.Connection.CreateCommand();
            stmt.CommandText = $"DELETE FROM {table} WHERE {terms}";

            // Prepara a statement com os valores
            foreach (var item in param){
                stmt.Parameters.AddWithValue($"?{item.Key}", $"{item.Value}");
            }

            try{
                // Executa o comando para remover o registo do banco
                stmt.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex){
                Console.WriteLine($"CRUD::SQL-ERROR: {ex.Message}");
                return false;
                throw new Exception("Erro ao tentar remover registo");
            }
            finally{
                base.close();
            }
        }
        

        
    }
    #nullable disable
}
