using MathApp.secondary_objects;
using MathApp.ui_object;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using User = MathApp.secondary_objects.User;

namespace remake
{
    // This object should only be accessible when the network mode is enabled -> it also provides other functionalities for other classes
    public partial class SignInWindow : Form
    {
        private Setup _currentSetup;
        private static readonly HttpClient client = new HttpClient();


        // Event to pass the logged-in user data to the main form
        public event EventHandler<UserEventArgs> UserLoggedIn;

        public SignInWindow(Setup currentSetup)
        {
            InitializeComponent();
            _currentSetup = currentSetup;
            updateSetup();
        }

        /// <summary>
        /// Function that makes a request to the server and logs in the user upon success
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            // Create a user object with the gathered information
            User user = new User
            {
                Username = textBox1.Text,
                Password = textBox2.Text
            };

            // Serialize the user object to JSON
            string userJson = JsonConvert.SerializeObject(user);

            // Send a POST request to the user login endpoint
            HttpResponseMessage response = await client.PostAsync("http://localhost:8082/users/login", new StringContent(userJson, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                // User login successful
                MessageBox.Show("User login successful");

                // Read the response content
                string responseJson = await response.Content.ReadAsStringAsync();

                // Deserialize the response to get the logged-in user details
                User loggedInUser = JsonConvert.DeserializeObject<User>(responseJson);

                // Raise the UserLoggedIn event to pass the user data to the main form
                UserLoggedIn?.Invoke(this, new UserEventArgs(loggedInUser));

                this.Close();
            }
            else
            {
                // User login failed
                MessageBox.Show("User login failed");
            }
        }

        /// <summary>
        /// Function that opens the SignUpWindow if the user wanted to sign up instead
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            SignUpWindow temporarySignUpWindow = new SignUpWindow(_currentSetup);
            temporarySignUpWindow.ShowDialog();

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
            switch (this._currentSetup.SelectedTheme)
            {
                case "Dark":
                    applyTheme(this._currentSetup.DarkTheme);
                    this.pictureBox1.Image = _currentSetup.DarkUser;
                    this.pictureBox2.Image = _currentSetup.DarkLock;
                    break;
                case "Light":
                    applyTheme(this._currentSetup.LightTheme);
                    this.pictureBox1.Image = _currentSetup.LightUser;
                    this.pictureBox2.Image = _currentSetup.LightLock;
                    break;
            }

            this.textBox2.Text = "";
            this.textBox2.PasswordChar = '*';
            this.textBox2.MaxLength = 14;

        }

        private void UserWindow_Load(object sender, EventArgs e)
        {

        }

    }
}
