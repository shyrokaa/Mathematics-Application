using MathApp.secondary_objects;

namespace remake
{
    public partial class OptionsWindow : Form
    {
        private Setup _currentSetup;
        private JsonParser _configMaker;


        public OptionsWindow(Setup currentSetup)
        {
            InitializeComponent();
            _configMaker = new JsonParser();
            _currentSetup = currentSetup;
            updateSetup();        
        }

        private void applyTheme(ColorScheme c)
        {
            //placing the color scheme itself onto the window
            
            //background
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            //text
            this.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            //menu

        }

        private void updateSetup()
        {
            switch (this._currentSetup.selectedTheme)
            {
                case "Dark":
                    //applying dark theme
                    applyTheme(this._currentSetup.darkTheme);
                    this.radioButton1.Checked = true;
                    this.radioButton2.Checked = false;
                    break;
                case "Light":
                    //applying white theme
                    applyTheme(this._currentSetup.lightTheme);
                    this.radioButton1.Checked = false;
                    this.radioButton2.Checked = true;
                    break;
            }
            
            if (this._currentSetup.networkDisabled)
                this.checkBox1.Checked = true;
            else
                this.checkBox1.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this menu should close the window and parse the changes to the json for easily updating the window

            //updating the configuration globally
            if(radioButton1.Checked)
                _configMaker.updateJson(this.checkBox1.Checked,radioButton1.Text);
            else
                _configMaker.updateJson(this.checkBox1.Checked, radioButton2.Text);

            //closing up the window
            this.Close();
        }
    }
}