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
            var user = database.findById("1");
            Console.WriteLine($"id:{user.Data["id"]} - name:{user.Data["name"]}");

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
