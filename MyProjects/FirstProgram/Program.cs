using System;
using System.Windows.Forms;
using FirstProgram.Src.View;
using FirstProgram.Src.Interfaces;
using FirstProgram.Src.Lib.MySql;
using FirstProgram.Src.Lib.MySql.Exemple;

namespace FirstProgram
{
    static class Program
    {

        static void Main()
        {
            var param = new UserModel().createParameter();
            param["?name"] = "veronica Plesca";

            var user  = new UserModel().find("name = ?name", param).fetch();
            Console.WriteLine($"id:{user.Data["id"]} - name: {user.Data["name"]} - number: {user.Data["number"]}");
            user.set("name", "Veronica Plesca");
            user.set("number", "100");
            Console.WriteLine(user.save());
            Console.WriteLine($"id:{user.Data["id"]} - name: {user.Data["name"]} - number: {user.Data["number"]}");

            
            Console.WriteLine("-----------");
            var users = new UserModel().find().fetch(true).GetFetch;
            foreach (var item in users){
                Console.WriteLine($"id:{item["id"]} - name: {item["name"]} - number: {item["number"]}");
            }
            /* Application.EnableVisualStyles();
            Application.Run(new TelaInicio()); */
            Console.ReadKey();
        }
    }
}
