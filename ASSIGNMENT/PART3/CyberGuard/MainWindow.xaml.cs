using System.Windows;
using System.Windows.Input;

namespace CyberGuard
{
    // ══════════════════════════════════════════════════════════════════════
    // MainWindow — WPF UI class
    // Creates one CyberSpace instance and routes all user input to it
    // Contains NO chatbot logic — only UI wiring and input routing
    // ══════════════════════════════════════════════════════════════════════
    public partial class MainWindow : Window
    {
        // Single instance of CyberSpace — holds all chatbot logic
        private CyberSpace space = new CyberSpace();

        // ── Constructor ───────────────────────────────────────────────────
        public MainWindow()
        {
            InitializeComponent();

            // Inject WPF RichTextBox wrapper into CyberSpace
            space.Initialise(new WpfChatDisplay(chatBox));

            // Play voice greeting on startup
            space.VoiceGreeting();

            // Show the welcome screen and logo
            space.WelcomeScreen();

            // Focus input box so user can type immediately
            txtInput.Focus();
        }

        // ── Send Button ───────────────────────────────────────────────────
        // Fires when user clicks SEND
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            // Show user's message in chat
            space.UserSay(input);
            txtInput.Clear();

            // Route to correct CyberSpace handler
            HandleInput(input);
        }

        // ── Enter Key ─────────────────────────────────────────────────────
        // Pressing Enter fires the same as clicking SEND
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSend_Click(sender, null);
                e.Handled = true;
            }
        }

        // ── Input Router ──────────────────────────────────────────────────
        // Reads CurrentSection from CyberSpace and calls the correct method
        // MainWindow has no chatbot logic — it only routes input here
        private void HandleInput(string input)
        {
            string lower = input.ToLower();

            switch (space.CurrentSection)
            {
                case "getname":
                    // Pass original — name should keep its capitalisation
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
                    DisableInput();
                    break;
            }

            // Disable input after goodbye
            if (space.CurrentSection == "goodbye")
                DisableInput();
        }

        // ── Disable Input ─────────────────────────────────────────────────
        // Called after goodbye — prevents typing after session ends
        private void DisableInput()
        {
            txtInput.IsEnabled = false;
            btnSend.IsEnabled = false;
        }
    }
}