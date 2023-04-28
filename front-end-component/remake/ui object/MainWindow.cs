using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MathApp.parsers;
using MathApp.Properties;
using MathApp.secondary_objects;
using MathApp.ui_object;
using MathNet.Numerics.LinearAlgebra.Factorization;
using Microsoft.VisualBasic.Logging;
using NCalc;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace remake
{


    // helper for the equation mode handling
    public enum EquationMode
    {
        Plot,
        Equation,
        Calculator,
        Integral
    }

    /// <summary>
    /// Class <c>MainWindow</c> models a the main user interface of the program and it's elements
    /// </summary>
    public partial class MainWindow : Form
    {

        // this handles the modes in which the application is running
        private int _selectedMode;


        private JsonParser _configMaker;
        private Setup _currentSetup;


        private EquationMode _equationMode;

        // variables used for the application states
        private Boolean _offlineMode;
        private Boolean _signedIn = false;


        // chart used in the data display - added here due to the toolbox not being able to see the refference to it
        private PlotModel _plotModel;
        private PlotView _plotView;


        /// <summary>
        /// Default constructor that also initializes the configuration elements
        /// </summary>
        public MainWindow()
        {
            this._configMaker = new JsonParser();
            InitializeComponent();
            this._currentSetup = _configMaker.ParseData();
            this._equationMode = 0;

            this._plotModel = new PlotModel();
            this._plotView = new PlotView();

            this._plotView.Model = this._plotModel;

            this._plotView.Dock = DockStyle.Fill;
            this.chart_panel.Controls.Add(this._plotView);

            // Add an empty series to the plot model
            this._plotModel.Series.Add(new LineSeries());

            updateSetup();
            updateApplicationState();
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

            this._currentSetup = _configMaker.ParseData();
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
            this.panel4.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel6.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel7.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);
            this.panel9.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_panel_bg);

            //ribbons
            this.panel3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
            this.panel5.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
            this.panel8.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);


            //text colors
            this.panel1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel5.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel6.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel7.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel8.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.panel9.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.menuStrip1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

            //input boxes
            this.textBox1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox4.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox5.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox6.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox7.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);
            this.textBox8.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);

            this.textBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox5.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox6.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox7.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox8.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

            //menu colors
            this.menuStrip1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

            //some buttons
            this.button1.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button2.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button3.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button4.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button7.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);
            this.button11.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_menu_bg);

        }


        private void setButtonTheme(ColorScheme c)
        {
            this.button1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);


            switch (this._equationMode)
            {
                case EquationMode.Plot:
                    this.button1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.Equation:
                    this.button2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.Calculator:
                    this.button3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.Integral:
                    this.button4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// Function that updates the interface components based on the user login state and the equation mode that is selected
        /// </summary>
        private void updateApplicationState()
        {
            if (_offlineMode)
            {
                // hide user login controls and replace them with something else

            }
            else
            {
                if (_signedIn)
                {
                    // hide sign in and sign up buttons
                    this.signIn_btn.Visible = false;
                    this.signUp_btn.Visible = false;
                    this.signOut_btn.Visible = true;
                }
                else
                {
                    // hide the sign out button
                    this.signIn_btn.Visible = true;
                    this.signUp_btn.Visible = true;

                    this.signOut_btn.Visible = false;
                }
            }

            switch (this._equationMode)
            {
                case EquationMode.Plot:
                    // show interval
                    this.label8.Visible = true;

                    this.textBox3.Visible = true;
                    this.textBox4.Visible = true;
                    // hide genetic algorithm inputs
                    this.label3.Visible = false;
                    this.label7.Visible = false;
                    this.label10.Visible = false;
                    this.label11.Visible = false;

                    this.textBox5.Visible = false;
                    this.textBox6.Visible = false;
                    this.textBox7.Visible = false;
                    this.textBox8.Visible = false;

                    break;
                case EquationMode.Equation:
                    // show interval
                    this.label8.Visible = true;

                    this.textBox3.Visible = true;
                    this.textBox4.Visible = true;

                    // show genetic algorithm inputs
                    this.label3.Visible = true;
                    this.label7.Visible = true;
                    this.label10.Visible = true;
                    this.label11.Visible = true;

                    this.textBox5.Visible = true;
                    this.textBox6.Visible = true;
                    this.textBox7.Visible = true;
                    this.textBox8.Visible = true;


                    break;
                case EquationMode.Calculator:
                    // hide interval
                    this.label8.Visible = false;

                    this.textBox3.Visible = false;
                    this.textBox4.Visible = false;
                    // hide genetic algorithm inputs
                    this.label3.Visible = false;
                    this.label7.Visible = false;
                    this.label10.Visible = false;
                    this.label11.Visible = false;

                    this.textBox5.Visible = false;
                    this.textBox6.Visible = false;
                    this.textBox7.Visible = false;
                    this.textBox8.Visible = false;
                    break;
                case EquationMode.Integral:
                    // hide interval
                    this.label8.Visible = false;

                    this.textBox3.Visible = false;
                    this.textBox4.Visible = false;
                    // hide genetic algorithm inputs
                    this.label3.Visible = false;
                    this.label7.Visible = false;
                    this.label10.Visible = false;
                    this.label11.Visible = false;

                    this.textBox5.Visible = false;
                    this.textBox6.Visible = false;
                    this.textBox7.Visible = false;
                    this.textBox8.Visible = false;
                    break;
            }
        }



        /// <summary>
        /// Function that applies the current configuration for the app
        /// </summary>
        private void updateSetup()
        {
            switch (this._currentSetup.SelectedTheme)
            {
                case "Dark":
                    //applying dark theme
                    applyTheme(this._currentSetup.DarkTheme);
                    setButtonTheme(this._currentSetup.DarkTheme);
                    this.pictureBox2.Image = _currentSetup.DarkLogo;

                    break;
                case "Light":
                    //applying white theme
                    applyTheme(this._currentSetup.LightTheme);
                    setButtonTheme(this._currentSetup.LightTheme);
                    this.pictureBox2.Image = _currentSetup.LightLogo;
                    break;
            }

            //user panel update -> too many controls for one function
            updateUserPanel(!this._currentSetup.NetworkDisabled);
            //equation mode handler -> used for the equation solving process

        }


        private void resetTextBoxes()
        {
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.textBox5.Text = string.Empty;
            this.textBox6.Text = string.Empty;
            this.textBox7.Text = string.Empty;
            this.textBox8.Text = string.Empty;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Plot;
            updateSetup();
            updateApplicationState();
            resetTextBoxes();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Equation;
            updateSetup();
            updateApplicationState();
            resetTextBoxes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Calculator;
            updateSetup();
            updateApplicationState();
            resetTextBoxes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Integral;
            updateSetup();
            updateApplicationState();
            resetTextBoxes();
        }



        //this is the function that sends requests based on the equation mode
        private void button12_Click(object sender, EventArgs e)
        {
            switch (this._equationMode)
            {
                case EquationMode.Plot:
                    // Get the function string from textbox1
                    handlePlot(sender, e);
                    break;

                // Code for other cases
                case EquationMode.Equation:
                    handleEquation(sender, e);
                    break;
                case EquationMode.Calculator:
                    handleCalculator(sender, e);
                    break;
                case EquationMode.Integral:
                    // ...
                    break;
                default:
                    // ...
                    break;
            }
        }


        private void handlePlot(object sender, EventArgs e)
        {
            string functionString = this.textBox1.Text;

            // Read the min and max values from the textboxes
            double min, max;
            if (!double.TryParse(this.textBox4.Text, out min) || !double.TryParse(this.textBox3.Text, out max))
            {
                MessageBox.Show("Invalid input. Please enter valid numbers for min and max.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate that the min and max values form a valid interval
            if (min >= max)
            {
                MessageBox.Show("Invalid interval. Max value must be greater than min value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a FunctionSeries from the function string using the PlotRequestParser method from RequestHandler class
            RequestHandler requestHandler = new RequestHandler();
            FunctionSeries functionSeries = requestHandler.PlotRequestParser($"f(x)={functionString}", min, max);

            // Check if functionSeries is null, meaning an error occurred during parsing
            if (functionSeries == null)
            {
                return;
            }

            // Hide the plot view while clearing the series
            this._plotView.Visible = false;

            // Clear the plot series
            this._plotView.Model.Series.Clear();

            // Add the function series to the plot
            this._plotView.Model.Series.Add(functionSeries);

            // Show the plot view again and refresh the plot
            this._plotView.Visible = true;
            this._plotView.InvalidatePlot(true);
        }

        private void handleEquation(object sender, EventArgs e)
        {
            // Get the input parameters from the text boxes
            string equation = this.textBox1.Text;
            string interval_bottom = this.textBox4.Text;
            string interval_top = this.textBox3.Text;
            string nr_vals = this.textBox4.Text;
            string ng_generations = this.textBox6.Text;
            string nr_parents = this.textBox7.Text;
            string population_size = this.textBox8.Text;

            // Set up the Python script arguments
            string[] args = new string[] { equation, interval_bottom, interval_top, nr_vals, ng_generations, nr_parents, population_size };

            // Call the Python script and capture the output
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = "genetic_algorithm.py " + String.Join(" ", args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    // Update the solution in the UI
                    this.textBox2.Text = result;
                }
            }
        }



        private void handleCalculator(object sender, EventArgs e)
        {
            string mathExpression = this.textBox1.Text;

            // Evaluate the math expression using the CalculatorRequestParser method from RequestHandler class
            RequestHandler requestHandler = new RequestHandler();
            double? result = requestHandler.CalculatorRequestParser(mathExpression);

            // Check if result is null, meaning an error occurred during parsing or evaluation
            if (result == null)
            {
                this.textBox2.Text = "";
                return;
            }

            // Update the result in the UI
            this.textBox2.Text = result.ToString();
        }


        private void button7_Click(object sender, EventArgs e)
        {
            // Clear the plot series
            this._plotView.Model.Series.Clear();

            // Refresh the plot
            this._plotView.InvalidatePlot(true);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Create a SaveFileDialog to prompt the user for a save location
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog.Title = "Save plot as image";

            // Show the SaveFileDialog and check if the user clicked OK
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the file format based on the extension
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                switch (Path.GetExtension(saveFileDialog.FileName).ToLower())
                {
                    case ".png":
                        format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case ".jpg":
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case ".bmp":
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                }

                // Create a bitmap of the plot view
                Bitmap bmp = new Bitmap(this._plotView.Width, this._plotView.Height);
                this._plotView.DrawToBitmap(bmp, new Rectangle(0, 0, this._plotView.Width, this._plotView.Height));

                // Save the bitmap to the file location chosen by the user
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    bmp.Save(stream, format);
                }

                // Display a message box confirming that the file was saved
                MessageBox.Show($"Plot saved as {Path.GetFileName(saveFileDialog.FileName)}", "Save successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!this._signedIn)
            {
                MessageBox.Show("Please Sign In in order to access your command history!");
            }
        }




        private void signIn_btn_Click(object sender, EventArgs e)
        {
            SignInWindow temporarySignInWindow = new SignInWindow(this._currentSetup);
            temporarySignInWindow.ShowDialog();
        }

        private void signUp_btn_Click(object sender, EventArgs e)
        {
            SignUpWindow temporarySignUpWindow = new SignUpWindow(this._currentSetup);
            temporarySignUpWindow.ShowDialog();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }


}

