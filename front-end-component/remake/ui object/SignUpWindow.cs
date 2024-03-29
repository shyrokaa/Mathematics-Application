﻿using MathApp.secondary_objects;
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

namespace MathApp.ui_object
{
    /// <summary>
    /// Class <c>SignUpWindow</c> models a the sign up menu of the user interface and it's elements
    /// </summary>
    public partial class SignUpWindow : Form
    {
        private Setup _currentSetup;
        private static readonly HttpClient client = new HttpClient();


        /// <summary>
        /// Default constructor that also initializes the configuration elements
        /// </summary>
        public SignUpWindow(Setup currentSetup)
        {
            InitializeComponent();
            _currentSetup = currentSetup;
            updateSetup();
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
            this.button2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

            //textbox
            this.textBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);

            this.textBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

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
                    this.pictureBox3.Image = _currentSetup.DarkLock;
                    break;
                case "Light":
                    applyTheme(this._currentSetup.LightTheme);
                    this.pictureBox1.Image = _currentSetup.LightUser;
                    this.pictureBox2.Image = _currentSetup.LightLock;
                    this.pictureBox3.Image = _currentSetup.LightLock;
                    break;
            }

            this.textBox2.Text = "";
            this.textBox2.PasswordChar = '*';
            this.textBox2.MaxLength = 14;

            this.textBox3.Text = "";
            this.textBox3.PasswordChar = '*';
            this.textBox3.MaxLength = 14;

        }

        /// UI RELATED FUNCTIONS


        // Event to pass the user data to the main form
        public event EventHandler<UserEventArgs> UserSignedUp;

        /// <summary>
        /// Function that closes the sign up form and parses a POST request with the data to the user server
        /// </summary>
        private async void button2_Click(object sender, EventArgs e)
        {
            if (fieldValidator())
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
                HttpResponseMessage response = await client.PostAsync("http://localhost:8082/users/signup", new StringContent(userJson, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    // User login successful
                    MessageBox.Show("User login successful");

                    // Read the response content
                    string responseJson = await response.Content.ReadAsStringAsync();

                    // Deserialize the response to get the logged-in user details
                    User loggedInUser = JsonConvert.DeserializeObject<User>(responseJson);

                    // Raise the UserSignedUp event to pass the user data to the main form
                    UserSignedUp?.Invoke(this, new UserEventArgs(loggedInUser));

                    this.Close();
                }
                else
                {
                    // User login failed
                    MessageBox.Show("User creation failed");
                }
            }
        }

        /// <summary>
        /// Function that checks if the completed fields are valid for sending to the server
        /// </summary>
        private bool fieldValidator()
        {
            // empty field error
            if (this.textBox1.Text == "" || this.textBox2.Text == "" || this.textBox3.Text == "")
            {
                MessageBox.Show("Error: one of the fields is empty");
                return false;
            }
            // passwords do not match

            if (this.textBox2.Text != this.textBox3.Text)
            {
                MessageBox.Show("Error: passwords do not match");
                return false;
            }

            // TODO: username already exists 

            // TODO: weak password

            return true;
        }



    }
}
