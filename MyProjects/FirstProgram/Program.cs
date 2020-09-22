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
            Console.WriteLine(user.Data["name"]);
            /* Application.EnableVisualStyles();
            Application.Run(new TelaInicio()); */
            Console.ReadKey();
        }
    }
}
