using System;
using System.Windows.Forms;

namespace FirstProgram.Src.View
{
    public class TelaInicio : Form
    {
        public TelaInicio(){
            this.Text = "Minha Janela";
            Button b = new Button();
            b.Text = "Clique em mim!";
            b.Click += new EventHandler(onButtonClick);
            Controls.Add(b);
        }

        private void onButtonClick(object sender, EventArgs e){
            MessageBox.Show("OLA MUNDO!!");
        }


    }
}