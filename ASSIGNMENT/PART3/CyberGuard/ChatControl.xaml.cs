using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberGuard
{
    public partial class ChatControl : UserControl
    {
        private readonly CyberSpace _space;

        public ChatControl(CyberSpace space)
        {
            InitializeComponent();
            _space = space;
            _space.Initialise(new CyberChatDisplay(chatBox));
            _space.VoiceGreeting();
            _space.WelcomeScreen();
            txtInput.Focus();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;
            _space.UserSay(input);
            txtInput.Clear();
            string lower = input.ToLower();
            switch (_space.CurrentSection)
            {
                case "getname": _space.UserInteraction(input); break;
                case "main": _space.ResponseSystem(lower); break;
                case "topicmenu": _space.HandleTopicMenu(lower); break;
                case "password": _space.HandlePassword(lower); break;
                case "phishing": _space.HandlePhishing(lower); break;
                case "safebrowsing": _space.HandleSafeBrowsing(lower); break;
                case "goodbye": DisableInput(); break;
            }
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) { btnSend_Click(sender, null); e.Handled = true; }
        }

        private void DisableInput()
        {
            txtInput.IsEnabled = false;
            btnSend.IsEnabled = false;
        }
    }
}