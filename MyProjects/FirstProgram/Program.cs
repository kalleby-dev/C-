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
            //ooa mundo
            UserModel user = new UserModel();
            //Criar exemplo
            Console.WriteLine("---\nInserindo");
            user.set("name", "Marcelo");
            user.set("number", "35");
            user.save();
            Console.WriteLine($"id:{user.get("id")} - name: {user.get("name")} - number: {user.get("number")}");

            //Alterar
            Console.WriteLine("---\nAlterando");
            user.findById("104");
            user.set("name", "Francisco");
            user.set("number", "44");
            user.save();
            Console.WriteLine($"id:{user.get("id")} - name: {user.get("name")} - number: {user.get("number")}");

            //Listagem
            Console.WriteLine("---\nListando");
            var users = new UserModel().find().fetch();
            foreach (var item in users){
                Console.WriteLine($"id:{item.get("id")} - name: {item.get("name")} - number: {item.get("number")}");
            }

            //Remoção
            Console.WriteLine("---\nRemovendo");
            var param = user.createParameter();
            param["?name"] = "Marcelo";
            user.find("name = ?name", param);
            user.remove();
            
            //Listagem
            Console.WriteLine("---\nListando novamente");
            users = new UserModel().find().fetch();
            foreach (var item in users){
                Console.WriteLine($"id:{item.get("id")} - name: {item.get("name")} - number: {item.get("number")}");
            }


            //Application.EnableVisualStyles();
            //Application.Run(new TelaInicio());
            Console.ReadKey();
        }
    }
}
