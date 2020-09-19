using System;
using System.Drawing;
using System.Windows.Forms;

using FirstProgram.Src.Interfaces;

namespace FirstProgram.Src.View
{
    public class TelaInicio : IForm
    {
        public TelaInicio() : base ("Programa em CS", "Minha Janela", new Size(400, 350))
        {
            Console.WriteLine("SETUP_Rendering Interface");
            this.initializeInterface();
            Console.WriteLine("SETUP_Application - Ready\n\n");
        }

        protected void initializeInterface(){
            FlowLayoutPanel flexPanel = this.iFlexPanel("flexPanel");
                
            // Initialize a subpanel in order to agroup subcomponents
            FlowLayoutPanel flexBox = this.iFlexPanel("userInputBox");
            flexBox.FlowDirection = FlowDirection.TopDown;
            flexBox.Controls.Add(this.iLabel("lblTitle", "Registrar Livro"));

            // Username input container
            FlowLayoutPanel userNameInput = this.iFlexPanel("userNameInput");
            userNameInput.FlowDirection = FlowDirection.LeftToRight;
            userNameInput.Controls.Add(this.iLabel("lblName", "Nome:", null, ContentAlignment.BottomRight, false));
            userNameInput.Controls.Add(this.txtName);

            // Add components into subpanel
            this.btnSend.Click += new EventHandler(onSendClick);
            flexBox.Controls.Add(userNameInput);
            flexBox.Controls.Add(this.btnSend);

            
            //Update interface
            flexPanel.Controls.Add(flexBox);
            Console.WriteLine("SETUP_Components - Ready");


            this.Controls.Add(flexPanel);
            Console.WriteLine("SETUP_Interface - Ready");
        }


        private void onSendClick(object sender, EventArgs e){
            String text = this.txtName.Text;
            Console.WriteLine($"APP_MSG: {text}");
        }

        // Components declaration
        private TextBox txtName = new IForm().iText("txtName");
        private Button btnSend = new IForm().iButton("btnSend", "Enviar"); 
    }
}