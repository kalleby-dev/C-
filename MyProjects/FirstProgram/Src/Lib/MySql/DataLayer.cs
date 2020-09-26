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
        protected String table = "";
        protected bool timestamps;
        protected String?[] required {get; set;} = null!;

        protected List<Dictionary<String, Object>> list = new List<Dictionary<string, object>> ();
        public List<Dictionary<String, Object>> GetFetch {get{return this.list;}}
        

        public DataLayer(String table, String?[] req = null!, String primary = "id", bool timestamps = true)
        {
            this.table = table;
            this.primary = primary;
            this.required = req ?? new string[0];
        }

        ///<summary>Generate a SQL command string with all terms and parameters</summary>
        ///<param name="terms" type="string">Clauses for SQL command</param>
        ///<param name="param" type="string">Parameters for SQL clauses</param>
        ///<param name="columns" type="string">Columns that will be returned on SQL search</param>
        public DataLayer find(String? terms = null, Dictionary <string, Object>? param = null, String columns = "*"){
            if(terms != null){
                this.statement = $"SELECT {columns} FROM {this.table} WHERE {terms}";
                this.param = param;
                return this;
            }

            this.statement = $"SELECT {columns} FROM {this.table}";
            return this;
        }

        ///<summary>Uses the primary key to search on database</summary>
        ///<param name="id" type="string">Primary key value of the row data</param>
        ///<param name="columns" type="string">Columns that will be returned on SQL search</param>
        ///<returns>A Datalayer object with all data stored</returns>
        public DataLayer findById(string id, string columns = "*"){
            var param = new Dictionary <string, Object>();
            param["?id"] = id;

            return this.find($"{this.primary} = ?id", param, columns).fetch();
        }

        ///<summary>Stores a SELECT result data</summary>
        ///<param name="getAll">If True will get all rows, If False will get only the first row on result</param>
        ///<returns>A Datalayer object with all data stored</returns>
        public DataLayer fetch(bool getAll = false){
            try
            {
                var rows = this.read();
                if(rows.Count == 0) return this;

                if(getAll){
                    this.list = rows;
                }
                this.data = rows.First();
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex.Message}\n{ex.StackTrace}");
            }
            return this;
        }


        public bool save()
        {

            string? id = null;
            try{
                // Criar um novo
                if(this.data.ContainsKey(this.primary)){
                    this.update(this.table, this.data);
                }

                if(!this.data.ContainsKey(this.primary)){
                    id = this.insert(this.table, this.data).ToString();
                }
                
                if(id == null) return false;

                
                this.data = this.findById(id).Data;
                return true;
            }
            catch (Exception ex){
                Console.WriteLine($"ERROR: {ex}\n{ex.StackTrace}");
                return false;
            }

        }


        public void set(String column, Object value){
            this.data[column] = value;
        }

        public Dictionary <string, Object> createParameter(){
            return new Dictionary <string, Object>();  
        }
        
/*         public DataLayer? findById(long id){

            return this;
        } */
        

    }
}
#nullable disable