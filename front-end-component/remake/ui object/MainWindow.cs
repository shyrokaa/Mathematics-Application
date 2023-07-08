using MathApp.parsers;
using MathApp.secondary_objects;
using MathApp.ui_object;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Text;


namespace remake
{

    // helper for the equation mode handling
    public enum EquationMode
    {
        PLOT,
        EQUATION,
        CALCULATOR,
        INTEGRAL
    }

    /// <summary>
    /// Class <c>MainWindow</c> models a the main user interface of the program and it's elements
    /// </summary>
    public partial class MainWindow : Form
    {
        // UI windows
        private SignInWindow _signInWindow;
        private SignUpWindow _signUpWindow;
        private HistoryWindow _historyWindow;
        
        // modes
        private EquationMode _equationMode;

        // helpers and other variables
        private readonly PlotModel _plotModel;
        private readonly PlotView _plotView;
        private readonly JsonParser _configMaker;
        private Setup _currentSetup;

        private Boolean _signedIn = false;
        private MathApp.secondary_objects.User _currentUser;


        /// <summary>
        /// Default constructor that also initializes the configuration elements
        /// </summary>
        public MainWindow()
        {
            this._configMaker = new JsonParser();
            InitializeComponent();

            // initialize parsers/plotters/etc
            this._currentSetup = _configMaker.ParseData();
            this._equationMode = 0;
            this._plotModel = new PlotModel();
            this._plotView = new PlotView();
            this._plotView.Model = this._plotModel;
            this._plotView.Dock = DockStyle.Fill;
            this.chart_panel.Controls.Add(this._plotView);

            // Add an empty series to the plot model
            this._plotModel.Series.Add(new LineSeries());

            UpdateSetup();
            UpdateApplicationState();
        }

        // API ACCESS RELATED FUNCTIONS
        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FlowLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void FlowLayoutPanel1_Paint_2(object sender, PaintEventArgs e)
        {

        }


        /// ACCESSING OTHER MENUS FOR INFORMATION/CONFIGURATION

        /// <summary>
        /// Function that opens the settings menu
        /// </summary>
        private void Settings_Click(object sender, EventArgs e)
        {
            OptionsWindow temporaryOptionWindow = new OptionsWindow(this._currentSetup);
            temporaryOptionWindow.ShowDialog();

            this._currentSetup = _configMaker.ParseData();
            this.UpdateSetup();
        }


        /// <summary>
        /// Opens the SignIn Window
        /// </summary>
        private void Button8_Click_1(object sender, EventArgs e)
        {
            SignInWindow temporarySignInWindow = new SignInWindow(this._currentSetup);
            temporarySignInWindow.ShowDialog();

        }

        /// <summary>
        /// Opens the SignUp Window
        /// </summary>
        private void Button9_Click(object sender, EventArgs e)
        {
            SignUpWindow temporarySignUpWindow = new SignUpWindow(_currentSetup);
            temporarySignUpWindow.ShowDialog();
        }

        /// CUSTOMIZATION RELATED FUNCTIONS

        /// <summary>
        /// Function that applies a specific theme to the application
        /// </summary>
        private void ApplyTheme(ColorScheme c)
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
            this.textBox9.BackColor = System.Drawing.ColorTranslator.FromHtml(c.form_bg);

            this.textBox1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox5.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox6.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox7.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox8.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.textBox9.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);

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


        private void SetButtonTheme(ColorScheme c)
        {
            this.button1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);
            this.button4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_text);


            switch (this._equationMode)
            {
                case EquationMode.PLOT:
                    this.button1.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.EQUATION:
                    this.button2.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.CALCULATOR:
                    this.button3.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                case EquationMode.INTEGRAL:
                    this.button4.ForeColor = System.Drawing.ColorTranslator.FromHtml(c.form_ribbon);
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// Function that updates the interface components based on the user login state and the equation mode that is selected
        /// </summary>
        private void UpdateApplicationState()
        {
            if (_signedIn)
            {
                // hide sign in and sign up buttons
                this.signIn_btn.Visible = false;
                this.signUp_btn.Visible = false;
                this.signOut_btn.Visible = true;

                //show user name display and picture
                this.label1.Visible = true;


                //change the text to what user is signed in currently
                this.label1.Text = "Hello, " + this._currentUser.Username + " !";

            }
            else
            {
                // hide the sign out button
                this.signIn_btn.Visible = true;
                this.signUp_btn.Visible = true;
                this.signOut_btn.Visible = false;


                // hide user name display and picture
                this.label1.Visible = false;



            }


            switch (this._equationMode)
            {
                case EquationMode.PLOT:
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

                    //hide the integral
                    this.label12.Visible = false;
                    this.textBox9.Visible = false;

                    break;
                case EquationMode.EQUATION:
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

                    //hide the integral
                    this.label12.Visible = false;
                    this.textBox9.Visible = false;
                    break;
                case EquationMode.CALCULATOR:
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

                    //hide the integral
                    this.label12.Visible = false;
                    this.textBox9.Visible = false;

                    break;
                case EquationMode.INTEGRAL:
                    // show interval
                    this.label8.Visible = true;

                    this.textBox3.Visible = true;
                    this.textBox4.Visible = true;

                    //show integral specific controls
                    this.label12.Visible = true;
                    this.textBox9.Visible = true;


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
        private void UpdateSetup()
        {
            switch (this._currentSetup.SelectedTheme)
            {
                case "Dark":
                    //applying dark theme
                    ApplyTheme(this._currentSetup.DarkTheme);
                    SetButtonTheme(this._currentSetup.DarkTheme);
                    this.pictureBox2.Image = _currentSetup.DarkLogo;

                    break;
                case "Light":
                    //applying white theme
                    ApplyTheme(this._currentSetup.LightTheme);
                    SetButtonTheme(this._currentSetup.LightTheme);
                    this.pictureBox2.Image = _currentSetup.LightLogo;
                    break;
            }

            //user panel update -> too many controls for one function
            UpdateUserPanel(!this._currentSetup.NetworkDisabled);
            //equation mode handler -> used for the equation solving process

        }


        private void ResetTextBoxes()
        {
            this.textBox1.Text = string.Empty;
            this.textBox2.Text = string.Empty;
            this.textBox3.Text = string.Empty;
            this.textBox4.Text = string.Empty;
            this.textBox5.Text = "1";
            this.textBox6.Text = "100";
            this.textBox7.Text = "5";
            this.textBox8.Text = "100";
        }


        /// <summary>
        /// Function that updates the user panel depending on the network configuration of the app
        /// </summary>
        private void UpdateUserPanel(bool isEnabled)
        {
            this.signOut_btn.Visible = isEnabled;
            this.signIn_btn.Visible = isEnabled;
            this.signUp_btn.Visible = isEnabled;


            this.label1.Visible = isEnabled;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.PLOT;
            UpdateSetup();
            UpdateApplicationState();
            ResetTextBoxes();


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.EQUATION;
            UpdateSetup();
            UpdateApplicationState();
            ResetTextBoxes();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.CALCULATOR;
            UpdateSetup();
            UpdateApplicationState();
            ResetTextBoxes();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this._equationMode = EquationMode.INTEGRAL;
            UpdateSetup();
            UpdateApplicationState();
            ResetTextBoxes();
        }

        // this is the function that sends requests based on the equation mode
        private void Button12_Click(object sender, EventArgs e)
        {
            // Requests are sent to their specific handlers
            switch (this._equationMode)
            {
                case EquationMode.PLOT:
                    HandlePlot(sender, e);
                    break;
                case EquationMode.EQUATION:
                    HandleEquation(sender, e);
                    break;
                case EquationMode.CALCULATOR:
                    HandleCalculator(sender, e);
                    break;
                case EquationMode.INTEGRAL:
                    HandleIntegral(sender, e);
                    break;
                default:
                    break;
            }

            // Automatically save the requested data if a user is logged in
            if (this._signedIn)
            {
                try
                {
                    // Step 1: Get the ID of the user that made the request
                    HttpClient client = new HttpClient();
                    string username = this._currentUser.Username;

                    // Create a dictionary to hold the username
                    Dictionary<string, string> usernameDict = new Dictionary<string, string>()
                    {
                        { "Username", username }
                    };

                    // Serialize the dictionary to JSON
                    string usernameJson = JsonConvert.SerializeObject(usernameDict);

                    // Send a POST request to the /get_id endpoint to retrieve the user ID
                    var userIdResponse = client.PostAsync("http://localhost:8082/users/get_id", new StringContent(usernameJson, Encoding.UTF8, "application/json")).Result;
                    string userId = userIdResponse.Content.ReadAsStringAsync().Result.Trim('"');

                    // Step 2: Create the request body for the POST request
                    Request newRequest = new Request
                    {
                        Owner = userId,
                        RequestType = this._equationMode.ToString(),
                        RequestBody = textBox1.Text // Replace with the actual request body
                    };

                    // Step 3: Serialize the request object to JSON
                    string requestJson = JsonConvert.SerializeObject(newRequest);
                    var content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                    // Send a POST request to the save-file server to save the request
                    var response = client.PostAsync("http://localhost:8083/requests/save", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // Request saved successfully
                        MessageBox.Show("Request saved successfully");
                    }
                    else
                    {
                        // Request save failed
                        MessageBox.Show("Failed to save request");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the request saving process
                    MessageBox.Show("An error occurred while saving the request: " + ex.Message);
                }
            }
        }




        private void HandlePlot(object sender, EventArgs e)
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
        private async void HandleEquation(object sender, EventArgs e)
        {
            // Get the input parameters from the text boxes
            string equation = this.textBox1.Text;
            string interval_bottom = this.textBox4.Text;
            string interval_top = this.textBox3.Text;
            string nr_vals = this.textBox5.Text;
            string ng_generations = this.textBox6.Text;
            string nr_parents = this.textBox7.Text;
            string population_size = this.textBox8.Text;

            // Call the equation request parser method using the request handler
            RequestHandler requestHandler = new RequestHandler();
            string result = await requestHandler.EquationRequestParserAsync(equation, interval_bottom, interval_top, nr_vals, ng_generations, nr_parents, population_size);

            // Update the UI with the result
            this.textBox2.Text = result;
        }

        private async void HandleIntegral(object sender, EventArgs e)
        {
            string function = this.textBox1.Text;
            string interval_bottom = this.textBox4.Text;
            string interval_top = this.textBox3.Text;
            string nr_trapezoids = this.textBox9.Text;

            // Call the equation request parser method using the request handler
            RequestHandler requestHandler = new RequestHandler();
            string result = await requestHandler.IntegralRequestParserAsync(function, interval_bottom, interval_top, nr_trapezoids);
            this.textBox2.Text = result;
        }



        private void HandleCalculator(object sender, EventArgs e)
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


        private void Button7_Click(object sender, EventArgs e)
        {
            // Clear the plot series
            this._plotView.Model.Series.Clear();

            // Refresh the plot
            this._plotView.InvalidatePlot(true);

            this.textBox2.Text = " ";
        
        }

        private void Button11_Click(object sender, EventArgs e)
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

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (!this._signedIn)
            {
                MessageBox.Show("Please Sign In in order to access your command history!");
            }
            else
            {
                try
                {
                    // getting the request history from the server

                    //step 1: getting the user id

                    string username = this._currentUser.Username;

                    // Create a dictionary to hold the username
                    Dictionary<string, string> usernameDict = new Dictionary<string, string>()
                    {
                        { "Username", username }
                    };

                    HttpClient client = new HttpClient();
                    // Serialize the dictionary to JSON
                    string usernameJson = JsonConvert.SerializeObject(usernameDict);

                    // Send a POST request to the /get_id endpoint to retrieve the user ID
                    var userIdResponse = client.PostAsync("http://localhost:8082/users/get_id", new StringContent(usernameJson, Encoding.UTF8, "application/json")).Result;
                    string userId = userIdResponse.Content.ReadAsStringAsync().Result.Trim('"');

                    // step 2: turning the user id into a JSON for the request
                    var userIdJson = new { Owner = userId };
                    string userIdJsonString = JsonConvert.SerializeObject(userIdJson);


                    // step 3: getting the request history for the specific user

                    var content = new StringContent(userIdJsonString, Encoding.UTF8, "application/json");
                    var response = client.PostAsync("http://localhost:8083/requests/get_all_requests", content).Result;



                    if (response.IsSuccessStatusCode)
                    {
                        // Retrieve the request history from the response body
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        List<SimplifiedRequest> requestHistory = JsonConvert.DeserializeObject<List<SimplifiedRequest>>(responseBody);

                        // Open the history window and pass the request history
                        _historyWindow = new HistoryWindow(requestHistory, this._currentSetup);
                        _historyWindow.LoadRequest += HistoryWindow_ObjectLoaded;
                        _historyWindow.ShowDialog();


                    }
                    else
                    {
                        // Failed to retrieve request history
                        MessageBox.Show("Failed to retrieve request history");
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during the request history retrieval process
                    MessageBox.Show("An error occurred while retrieving the request history: " + ex.Message);
                }
            }
        }


        private void SignIn_btn_Click(object sender, EventArgs e)
        {
            _signInWindow = new SignInWindow(this._currentSetup);
            _signInWindow.UserLoggedIn += SignInWindow_UserLoggedIn;
            _signInWindow.ShowDialog();
        }

        private void SignUp_btn_Click(object sender, EventArgs e)
        {
            _signUpWindow = new SignUpWindow(this._currentSetup);
            _signUpWindow.UserSignedUp += SignUpWindow_UserSignedUp;
            _signUpWindow.ShowDialog();
        }

        // EVENTS

        private void SignInWindow_UserLoggedIn(object sender, UserEventArgs e) //basically an function that is called when the sign in window closes - event
        {
            // Handle the logged-in user data received from the SignInWindow
            MathApp.secondary_objects.User loggedInUser = e.User;
            // Use the user data as needed in the main form

            // Unsubscribe from the UserLoggedIn event
            _signInWindow.UserLoggedIn -= SignInWindow_UserLoggedIn;

            //adding information to the window objects and updating the interface accordingly
            this._signedIn = true;
            this._currentUser = loggedInUser;

            UpdateApplicationState();

        }

        private void SignUpWindow_UserSignedUp(object sender, UserEventArgs e)
        {
            // Handle the logged-in user data received from the SignInWindow
            MathApp.secondary_objects.User loggedInUser = e.User;
            // Use the user data as needed in the main form

            // Unsubscribe from the UserSignedUp event
            _signUpWindow.UserSignedUp -= SignUpWindow_UserSignedUp;

            

            // Adding information to the window objects and updating the interface accordingly
            this._signedIn = true;
            this._currentUser = loggedInUser;

            UpdateApplicationState();
        }

        private void HistoryWindow_ObjectLoaded(object sender, LoadEventArgs e)
        {
            // Handle the logged-in user data received from the SignInWindow
            MathApp.secondary_objects.SimplifiedRequest request = e.SimplifiedRequest;
            // Use the user data as needed in the main form

            // Unsubscribe from the UserSignedUp event
            _historyWindow.LoadRequest -= HistoryWindow_ObjectLoaded;


            // Switching to the proper work panel and request

            switch(request.RequestType)
            {
                case "Plot":
                    break;
                    this._equationMode = EquationMode.PLOT;
                case "Calculator":
                    this._equationMode = EquationMode.CALCULATOR;
                    break;
                case "Equation":
                    this._equationMode = EquationMode.EQUATION;
                    break;
                case "Integral":
                    this._equationMode = EquationMode.INTEGRAL;
                    break;
            }

            UpdateSetup();
            UpdateApplicationState();
            ResetTextBoxes();

            textBox1.Text = request.RequestBody;

          
        }



        private void SignOut_btn_Click_1(object sender, EventArgs e)
        {
            this._signedIn = false;
            MessageBox.Show("You have signed out");
            UpdateApplicationState();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

    }

}

