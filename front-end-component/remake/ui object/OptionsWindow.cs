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


        /// CUSTOMIZATION RELATED FUNCTIONS

        /// <summary>
        /// Function that applies a specific theme to the application
        /// </summary>
        private void applyTheme(ColorScheme c)
        {
            //placing the color scheme itself onto the window

            //background
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            //text
            this.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
           
            this.panel2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            //menu
           
            this.panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);


          


            this.button1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

        }


        /// <summary>
        /// Function that applies the current configuration for the app
        /// </summary>
        private void updateSetup()
        {
            switch (this._currentSetup.SelectedTheme)
            {
                case "Dark":
                    applyTheme(this._currentSetup.DarkTheme);
                    this.radioButton1.Checked = true;
                    this.radioButton2.Checked = false;
                    break;
                case "Light":
                    applyTheme(this._currentSetup.LightTheme);
                    this.radioButton1.Checked = false;
                    this.radioButton2.Checked = true;
                    break;
            }

           
        }


        /// UI RELATED FUNCTIONS

        /// <summary>
        /// Function that closes the current window and sends the changes to the config.json file
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                _configMaker.UpdateJson( radioButton1.Text);
            else
                _configMaker.UpdateJson( radioButton2.Text);

            this.Close();
        }

        /// <summary>
        /// Function that closes the current window without doing any modification to the config.json file
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OptionsWindow_Load(object sender, EventArgs e)
        {

        }
    }
}