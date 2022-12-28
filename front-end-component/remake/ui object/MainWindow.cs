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

namespace remake
{
    public partial class MainWindow : Form
    {
        //current
        private JsonParser _configMaker;
        private Setup _currentSetup;

        public MainWindow()
        {
            this._configMaker = new JsonParser();
            InitializeComponent();
            this._currentSetup = _configMaker.parseData();     
            updateSetup();
        }

        private void applyTheme(ColorScheme c)
        {
            //background colors
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.panel1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel5.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            
            //text colors
            this.panel1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel5.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            //menu colors
            this.menuStrip1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            
        }

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

        private void updateUserPanel(bool isEnabled)
        {
            this.button7.Visible = isEnabled;
            this.button8.Visible = isEnabled;
            this.button9.Visible = isEnabled;

            this.pictureBox1.Visible = isEnabled;
            this.label1.Visible = isEnabled;
            this.label2.Visible = isEnabled;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void settings_Click(object sender, EventArgs e)
        {
            OptionsWindow temporaryOptionWindow = new OptionsWindow(this._currentSetup);
            temporaryOptionWindow.ShowDialog();
            
            this._currentSetup = _configMaker.parseData();
            this.updateSetup();
            //this.Update();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            //oppening the sign in form for parsing requests to the server
            UserWindow temporaryUserWindow = new UserWindow();
            temporaryUserWindow.ShowDialog();

            //updating the configuration for the main window
            
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
    }
}
