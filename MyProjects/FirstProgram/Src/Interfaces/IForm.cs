using System;
using System.Drawing;
using System.Windows.Forms;

namespace FirstProgram.Src.Interfaces
{
    public class IForm : Form
    {
        protected Font fontTitleStyle = new Font("Arial", 18, FontStyle.Bold);
        protected Font fontTextStyle = new Font("Arial", 10, FontStyle.Regular);
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
            panel.Anchor = AnchorStyles.Top;
            panel.WrapContents = true;
            panel.Dock = DockStyle.Fill;

            return panel;
        }

        
        ///<summary>Create a basic Button</summary>
        public Button iButton(String name, String text, Font font = null)
        {
            Button button = (Button) this.create(new Button(), name, text, font);
            return button;
        }

        
        ///<summary>Create a basic TextBox</summary>
        public TextBox iText(String name, String text = null, Font font = null)
        {
            TextBox textBox = (TextBox) this.create(new TextBox(), name, text, font);
            return textBox;
        }


        ///<summary>Create a basic NumberBox</summary>
        public NumericUpDown iNumber(String name, int decimalPlates = 0,String text = null, Font font = null)
        {
            NumericUpDown numberBox = (NumericUpDown) this.create(new NumericUpDown(), name, text, font);
            numberBox.DecimalPlaces = decimalPlates;
            return numberBox;
        }


        ///<summary>Create a basic Label</summary>
        public Label iLabel(String name, String text, Font font = null, ContentAlignment align = 0, bool autoSize = true)
        {
            Label label = (Label) this.create( new Label(), name, text, font, autoSize);
            if(align != 0) label.TextAlign = align;

            return label;
        }


        ///<summary>Create a basic Component</summary>
        public Control create(Control obj, String name, String text = null, Font font = null, bool autoSize = true)
        {
            obj.Name = name;
            obj.AutoSize = autoSize;
            if(text != null) obj.Text = text;
            if(font != null) obj.Font = font;
            else obj.Font = this.fontTextStyle;

            return obj;
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