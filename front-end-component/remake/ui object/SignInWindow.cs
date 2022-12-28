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
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // This method should make calls to the server if networking is disabled, otherwise it should just not be accesible
            this.Close();
        }

        private void UserWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
