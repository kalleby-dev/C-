using System;
using System.Drawing;
using System.Windows.Forms;

namespace FirstProgram.Src.Interfaces
{
    public class IForm : Form
    {
        protected Font fontTitleStyle = new Font("Arial", 18, FontStyle.Bold);
        protected Font fontTextStyle = new Font("Arial", 12, FontStyle.Regular);
        public IForm(){}
        protected IForm(string name, string title, Size size){
            this.Name = name;
            this.Text = title;
            this.Size = size;
            this.StartPosition = FormStartPosition.CenterScreen;

            Console.WriteLine($"Window: {name} - Ready");
        }


        ///<summary>
        /// Create a basic FlowPanel
        ///</summary>
        protected FlowLayoutPanel iFlexPanel(string name){
            FlowLayoutPanel panel = new FlowLayoutPanel();

            panel.Name = name;
            panel.TabIndex = 0;
            panel.AutoSize = true;
            panel.Location = new Point(0,0);

            return panel;
        }

        ///<summary>
        /// Create a basic Button
        ///</summary>
        public Button iButton(String name, String text, Font font = null){
            Control c = new Control();
            
            Button button = new Button();

            button = (Button)this.c(button, "Clique");
            c.Name = name;
            c.Text = text;
            c.AutoSize = true;

            
            if(font != null) c.Font = font;
            else c.Font = this.fontTextStyle;

            return button;
        }

        public Control c(Control obj, String text){
            Control a = obj;
            a.Text = text;
            return a;
        }

        
        ///<summary>
        /// Create a basic TextBox
        ///</summary>
        public TextBox iText(String name, Font font = null){
            TextBox textBox = new TextBox();

            textBox.Name = name;
            if(font != null) textBox.Font = font;
            else textBox.Font = this.fontTextStyle;

            return textBox;
        }


        ///<summary>
        /// Create a basic TextBox
        ///</summary>
        public Label iLabel(String name, String text, Font font = null, ContentAlignment align = 0, bool autoSize = true){
            Label label = new Label();

            label.Name = name;
            label.Text = text;

            if(font != null) label.Font = font;
            else label.Font = this.fontTextStyle;

            if(align != 0) label.TextAlign = align;
                
            label.AutoSize = autoSize;

            return label;
        }

        ///<summary>
        /// Search for a component into component list
        ///</summary>
       #nullable enable
        protected Control? getComponent(String keyname, Control.ControlCollection control){
            Control[] result = control.Find(keyname, false);
 
            foreach (Control item in result){
                if(item != null) return item;
            }
            
            return null;
        }
        #nullable disable
    }
}