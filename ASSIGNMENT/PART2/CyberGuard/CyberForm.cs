using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace CyberGuard
{
    public partial class CyberForm : Form
    {
        CyberSpace space = new CyberSpace();
        public CyberForm()
        {
            InitializeComponent();
            //this codes links cyberspace with richtextbox
            space.Initialise(richTextBox);
            //this plays voice greeting on form load
            space.VoiceGreeting();
            //this displays the logo on form load
            space.WelcomeScreen();
            this.txtUserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserInput_KeyDown);
            txtUserInput.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CyberForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string input = txtUserInput.Text.ToLower().Trim();
            if (string.IsNullOrEmpty(input))
            {
                return; // Ignore empty input
            }
            space.UserSay(input);
            txtUserInput.Clear();

            HandleInput(input);
        }
        private void txtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSend_Click(sender, e);
                e.SuppressKeyPress = true; // Prevent the ding sound
            }
        }
        private void txtUserInput_TextChanged(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
        private void HandleInput(string input)
        {
            string lower = input.ToLower();

            switch (space.CurrentSection)
            {
                case "getname":
                    // Pass original input — name should keep its capitalisation
                    space.UserInteraction(input);
                    break;

                case "main":
                    space.ResponseSystem(lower);
                    break;

                case "topicmenu":
                    space.HandleTopicMenu(lower);
                    break;

                case "password":
                    space.HandlePassword(lower);
                    break;

                case "phishing":
                    space.HandlePhishing(lower);
                    break;

                case "safebrowsing":
                    space.HandleSafeBrowsing(lower);
                    break;

                case "goodbye":
                    // Session ended — disable input
                    DisableInput();
                    break;
            }

            // Disable input if goodbye was reached
            if (space.CurrentSection == "goodbye")
                DisableInput();
        }

        private void DisableInput()
        {
            txtUserInput.Enabled = false;
            btnSend.Enabled = false;
        }
        
    }
}
