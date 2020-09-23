using System;
using System.Windows.Forms;
using FirstProgram.Src.View;
using FirstProgram.Src.Interfaces;
using FirstProgram.Src.Lib.MySql;

namespace FirstProgram
{
    static class Program
    {

        static void Main()
        {
            DataLayer database = new DataLayer("book");
            var user = database;
            user.set("name", "Daniel");
            user.set("price", "15.5");
            Console.WriteLine(user.save());
            //Console.WriteLine($"id:{user.Data["id"]} - name:{user.Data["name"]} - price:{user.Data["price"]}");

            Console.WriteLine("-----------");

            var users = database.find().fetch(true).GetFetch;
            
            foreach (var item in users){
                Console.WriteLine($"id:{item["id"]} - name: {item["name"]}");
            }
            /* Application.EnableVisualStyles();
            Application.Run(new TelaInicio()); */
            Console.ReadKey();
        }
    }
}
