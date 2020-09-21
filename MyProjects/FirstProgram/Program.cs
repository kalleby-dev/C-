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
            /* Application.EnableVisualStyles();
            Application.Run(new TelaInicio()); */
            Console.ReadKey();
        }
    }
}
