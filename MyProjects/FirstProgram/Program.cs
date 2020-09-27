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
            /* Buscar */
/*             var param = new UserModel().createParameter();
            param["?name"] = "Daniel";
            var user  = new UserModel().find("name = ?name", param).fetch();
            Console.WriteLine($"id:{user.Data["id"]} - name: {user.Data["name"]} - number: {user.Data["number"]}");

            param.Clear(); */

            /* Alterar */
/*             user.set("name", "Kallyne");
            user.set("number", "5000");

            Console.WriteLine(user.save());
            Console.WriteLine($"id:{user.Data["id"]} - name: {user.Data["name"]} - number: {user.Data["number"]}");
  */        
            UserModel user = new UserModel();
            user.findById("99");
/*             user.set("name", "Joao");
            user.set("number", "55");
            Console.WriteLine(user.save()); */
            Console.WriteLine($"id:{user.get("id")} - name: {user.get("name")} - number: {user.get("number")}");
            
            /* Listagem Completa */
            Console.WriteLine("-----------");
            var users = new UserModel().find().fetch(true);//.fetch(true);
            foreach (var item in users){
                Console.WriteLine($"id:{item.get("id")} - name: {item.get("name")} - number: {item.get("number")}");
            }


            /* Application.EnableVisualStyles();
            Application.Run(new TelaInicio()); */
            Console.ReadKey();
        }
    }
}
