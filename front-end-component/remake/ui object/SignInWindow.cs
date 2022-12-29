using MathApp.secondary_objects;
using MathApp.ui_object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace remake
{
    // This object should only be accessible when the network mode is enabled -> it also provides other functionalities for other classes
    public partial class SignInWindow : Form
    {
        private Setup _currentSetup;
        public SignInWindow(Setup currentSetup)
        {
            InitializeComponent();
            _currentSetup = currentSetup;
            updateSetup();
        }


        /// <summary>
        /// Function that returns the user object that was initialized (Requires the form to be closed) 
        /// </summary>
        private void getCurrentUser()
        {

        }

        /// <summary>
        /// Function that makes a request to the server and logs in the user upon success
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // This method should make calls to the server if networking is disabled, otherwise it should just not be accesible
            MessageBox.Show("fake user log in successful");
            
            //populating an user object with the information that was gathered from the api
             

            this.Close();
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {

        }

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
            this.panel1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

            //menu
            this.panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            //buttons
            this.button1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
        
            //textbox
            this.textBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);

            this.textBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

        }

        /// <summary>
        /// Function that applies the current configuration for the app
        /// </summary>
        private void updateSetup()
        {
            switch (this._currentSetup.selectedTheme)
            {
                case "Dark":
                    applyTheme(this._currentSetup.darkTheme);
                    this.pictureBox1.Image = _currentSetup.darkUser;
                    this.pictureBox2.Image = _currentSetup.darkLock;
                    break;
                case "Light":
                    applyTheme(this._currentSetup.lightTheme);
                    this.pictureBox1.Image = _currentSetup.lightUser;
                    this.pictureBox2.Image = _currentSetup.lightLock;
                    break;
            }

            this.textBox2.Text = "";
            this.textBox2.PasswordChar = '*';
            this.textBox2.MaxLength = 14;

        }

        /// <summary>
        /// Function that applies a specific theme to the application
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            SignUpWindow temporarySignUpWindow = new SignUpWindow(_currentSetup);
            temporarySignUpWindow.ShowDialog();
            
        }
    }
}
