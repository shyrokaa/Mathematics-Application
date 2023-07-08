using MathApp.secondary_objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MathApp.ui_object
{
    public partial class HistoryWindow : Form
    {
        // Event to pass the selected request data to the main form
        public event EventHandler<LoadEventArgs> LoadRequest;

        //the thematic that is currently loaded
        private readonly Setup _currentSetup;

        public HistoryWindow(List<SimplifiedRequest> requestHistory, Setup currentSetup)
        {
            InitializeComponent();

            _currentSetup = currentSetup;

            // Load the requestHistory into listBox1
            foreach (SimplifiedRequest request in requestHistory)
            {
                string itemText = $"{request.RequestType}: {request.RequestBody}";
                listBox1.Items.Add(itemText);
            }


            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            UpdateSetup();
        }



        //function that loads in the selected history element
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.SelectedIndex != -1)
                {
                    string selectedItem = listBox1.SelectedItem?.ToString();
                    if (selectedItem != null)
                    {

                        string type = "";
                        if (selectedItem.Contains("Plot"))
                        {
                            type = "Plot";
                        }
                        if (selectedItem.Contains("Calculator"))
                        {
                            type = "Calculator";
                        }
                        if (selectedItem.Contains("Equation"))
                        {
                            type = "Equation";
                        }
                        if (selectedItem.Contains("Integral"))
                        {
                            type = "Integral";
                        }

                        string[] words = selectedItem.Split(' ');
                        string body = string.Join(' ', words.Skip(1));

                        SimplifiedRequest simplifiedReq = new SimplifiedRequest(type, body);

                        LoadEventArgs args = new LoadEventArgs(simplifiedReq);
                        LoadRequest?.Invoke(this, args);
                    }
                    else
                    {
                        MessageBox.Show("Please select a valid item to load.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select an item to load.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        /// <summary>
        /// Function that applies a specific theme to the application
        /// </summary>
        private void ApplyTheme(ColorScheme c)
        {
            //placing the color scheme itself onto the window

            //background
            this.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.listBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            //text
            this.listBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

            //some buttons
            this.button1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

        }

        /// <summary>
        /// Function that applies the current configuration for the app
        /// </summary>
        private void UpdateSetup()
        {
            switch (this._currentSetup.SelectedTheme)
            {
                case "Dark":
                    ApplyTheme(this._currentSetup.DarkTheme);

                    break;
                case "Light":
                    ApplyTheme(this._currentSetup.LightTheme);

                    break;
            }
        }

    }
}
