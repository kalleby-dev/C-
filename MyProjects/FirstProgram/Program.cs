using System;
using System.Windows.Forms;
using FirstProgram.Src.View;
using FirstProgram.Src.Interfaces;

namespace FirstProgram
{
    static class Program
    {

        static void Main()
        {
            IDatabase database = new IDatabase();
            Application.EnableVisualStyles();
            Application.Run(new TelaInicio());
        }
    }
}
