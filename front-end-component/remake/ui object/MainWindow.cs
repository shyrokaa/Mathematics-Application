using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MathApp.secondary_objects;
using MathApp.ui_object;

namespace remake
{
    /// <summary>
    /// Class <c>MainWindow</c> models a the main user interface of the program and it's elements
    /// </summary>
    public partial class MainWindow : Form
    {
        private JsonParser _configMaker;
        private Setup _currentSetup;

        /// <summary>
        /// Default constructor that also initializes the configuration elements
        /// </summary>
        public MainWindow()
        {
            this._configMaker = new JsonParser();
            InitializeComponent();
            this._currentSetup = _configMaker.parseData();     
            updateSetup();
        }


        // API ACCESS RELATED FUNCTIONS
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }


        /// ACCESSING OTHER MENUS FOR INFORMATION/CONFIGURATION

        /// <summary>
        /// Function that opens the settings menu
        /// </summary>
        private void settings_Click(object sender, EventArgs e)
        {
            OptionsWindow temporaryOptionWindow = new OptionsWindow(this._currentSetup);
            temporaryOptionWindow.ShowDialog();

            this._currentSetup = _configMaker.parseData();
            this.updateSetup();
        }


        /// <summary>
        /// Opens the SignIn Window
        /// </summary>
        private void button8_Click_1(object sender, EventArgs e)
        {
            SignInWindow temporarySignInWindow = new SignInWindow(this._currentSetup);
            temporarySignInWindow.ShowDialog();

        }

        /// <summary>
        /// Opens the SignUp Window
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            SignUpWindow temporarySignUpWindow = new SignUpWindow(_currentSetup);
            temporarySignUpWindow.ShowDialog();
        }


        /// CUSTOMIZATION RELATED FUNCTIONS

        /// <summary>
        /// Function that applies a specific theme to the application
        /// </summary>
        private void applyTheme(ColorScheme c)
        {
            //background colors
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel5.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);

            //text colors
            this.panel1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel5.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            //menu colors
            this.menuStrip1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

            //some buttons
            this.signOut_btn.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.signIn_btn.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.signUp_btn.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button12.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
        }

        /// <summary>
        /// Function that applies the current configuration for the app
        /// </summary>
        private void updateSetup()
        {
            switch (this._currentSetup.selectedTheme)
            {
                case "Dark":
                    //applying dark theme
                    applyTheme(this._currentSetup.darkTheme);
                    break;
                case "Light":
                    //applying white theme
                    applyTheme(this._currentSetup.lightTheme);
                    break;
            }
            //user panel update -> too many controls for one function
            updateUserPanel(!this._currentSetup.networkDisabled);
        }

        /// <summary>
        /// Function that updates the user panel depending on the network configuration of the app
        /// </summary>
        private void updateUserPanel(bool isEnabled)
        {
            this.signOut_btn.Visible = isEnabled;
            this.signIn_btn.Visible = isEnabled;
            this.signUp_btn.Visible = isEnabled;

            this.pictureBox1.Visible = isEnabled;
            this.label1.Visible = isEnabled;
            this.label2.Visible = isEnabled;
        }



    }
}
