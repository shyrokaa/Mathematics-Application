using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using MathApp.secondary_objects;
using MathApp.ui_object;
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
                    break;
                case "Light":
                    //applying white theme
                    applyTheme(this._currentSetup.LightTheme);
                    setButtonTheme(this._currentSetup.LightTheme);
                    break;
            }

            //user panel update -> too many controls for one function
            updateUserPanel(!this._currentSetup.NetworkDisabled);
            //equation mode handler -> used for the equation solving process

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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Equation;
            updateSetup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Calculator;
            updateSetup();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.Integral;
            updateSetup();
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
                    // ...
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







    }


}

