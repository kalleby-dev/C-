using System;
using System.Collections.Generic;

#nullable enable
namespace FirstProgram.Src.Lib.MySql
{
    public class DataLayer : SQLCrud
    {
        protected String primary;
        protected String table;
        protected bool timestamps;
        protected String?[] required {get; set;} = null!;
        protected Dictionary <String, Object> data = new Dictionary <string, Object> ();
        protected List<Dictionary<String, Object>> list = new List<Dictionary<string, object>> ();
        

        public DataLayer(String table, String?[] req = null!, String primary = "id", bool timestamps = true)
        {
            this.table = table;
            this.primary = primary;
            this.required = req ?? new string[0];
        }



        ///<summary>Executes a SQL search, and return the first result</summary>
        ///<param name="terms" type="string">Custom clauses for the SQL command</param>
        ///<param name="param" type="string">Custom parameters for the SQL clauses</param>
        ///<param name="columns" type="string">Columns that will be returned on result</param>
        public DataLayer find(String? terms = null, Dictionary <string, Object>? param = null, String columns = "*"){
            // Define a SQL statement without clauses
            var sql = $"SELECT {columns} FROM {this.table}";
            
            // Define a custom SQL statement with custom clauses
            if(terms != null){
                sql = $"SELECT {columns} FROM {this.table} WHERE {terms}"; 
            }
            
            // Execute the statement and store results
            var rows = base.read(sql, param);
            if(rows.Count != 0){
                this.list = rows;
                this.data = rows[0];                
            }
            return this;
        }



        ///<summary>Uses the primary key to search on database</summary>
        ///<param name="id" type="string">Primary key value of the row data</param>
        ///<param name="columns" type="string">Columns that will be returned on SQL search</param>
        ///<returns>A Datalayer object with all data stored</returns>
        public DataLayer findById(string id, string columns = "*"){
            var param = new Dictionary <string, Object>();
            param["?id"] = id;

            return this.find($"{this.primary} = ?id", param, columns);
        }



        ///<summary>Return all rows from result in DataLayer objects</summary>
        ///<returns>A object list with all data stored</returns>
        public List<DataLayer> fetch(){
            var result = new List<DataLayer>();
            try
            {   
                var rows = this.list;

                // Verifica se houve resultados
                if(rows.Count == 0){
                    throw new Exception("Não foram encontrados resultados para essa busca");
                }
                
                
                foreach (var item in rows){
                    var dataItem = new DataLayer(this.table);
                    dataItem.data = item;
                    result.Add(dataItem);
                }

                this.list = rows;
                
                
                
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}{ex.StackTrace}");
            }
            return result;
        }


        ///<summary>Stores a SELECT result data</summary>
        ///<param name="getAll">If True will get all rows, If False will get only the first row on result</param>
        ///<returns>A Datalayer object with all data stored</returns>
        public bool save()
        {
            string? id = null;
            try{
                // Criar um novo
                if(this.data.ContainsKey(this.primary)){
                    if(!this.alter()) throw new Exception("Não foi possivel concluir a alteração");      
                    
                    id = this.data[this.primary].ToString();
                }
                
                else if(!this.data.ContainsKey(this.primary)){
                    id = base.insert(this.table, this.data).ToString();
                }

                if(id == null) return false;
                this.data = this.findById(id).data;
                return true;
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}{ex.StackTrace}");
                return false;
            }

        }



        ///<summary>Generate a SQL command string with all terms and parameters</summary>
        ///<param name="terms" type="string">Clauses for SQL command</param>
        ///<param name="param" type="string">Parameters for SQL clauses</param>
        protected bool alter(String? terms = null, Dictionary <string, Object>? param = null)
        {
            try{
                if(!this.data.ContainsKey(this.primary)){
                    throw new Exception("Objeto não iniciado: você deve iniciar o registo antes de altera-lo");
                }

                String stmt = $"WHERE {this.primary} = {this.data[this.primary]}";

                if(terms != null){
                    stmt = $"WHERE {terms}";
                }

                return base.update(this.table, stmt, this.data, param);
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}{ex.StackTrace}");
                return false;
            }
        }



        public DataLayer remove(){
            try{
                if(!this.data.ContainsKey(this.primary)){
                    throw new Exception("Objeto não iniciado: você deve iniciar o registo antes de remove-lo");
                }

                String terms = $"{this.primary} = ?id";
                var param = new Dictionary <string, Object>();
                param["?id"] = this.data[this.primary];


                if(base.delete(this.table, terms, param)){
                    this.data.Clear();
                    //this.GetFetch.Clear();
                }

            }            
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}{ex.StackTrace}");
            }

            return this;
        }




        public void set(String column, Object value){
            this.data[column] = value;
        }

        public Object? get(String column){
            try
            {
                if(!this.data.ContainsKey(column)){
                    throw new Exception($"Indice '{column}' não encontrado");
                }
                return this.data[column];
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}{ex.StackTrace}");
                return null;
            }

        }


        public Dictionary <string, Object> createParameter(){
            return new Dictionary <string, Object>();  
        }
        

    }
}
#nullable disable