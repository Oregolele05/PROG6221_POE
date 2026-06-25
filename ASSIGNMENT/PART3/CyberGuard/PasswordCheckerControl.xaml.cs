using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberGuard
{
    public partial class PasswordCheckerControl : UserControl
    {
        public event EventHandler BackToChatRequested;

        public PasswordCheckerControl()
        {
            InitializeComponent();
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string pwd = txtPassword.Password;
            if (string.IsNullOrEmpty(pwd))
            {
                strengthBar.Value = 0;
                lblStrength.Text = "Strength: (type a password above)";
                lblFeedback.Text = "Suggestions will appear here as you type.";
                return;
            }

            int score = 0;
            if (pwd.Length >= 8) score += 20;
            if (pwd.Length >= 12) score += 20;
            if (pwd.Any(char.IsUpper)) score += 15;
            if (pwd.Any(char.IsLower)) score += 15;
            if (pwd.Any(char.IsDigit)) score += 15;
            if (pwd.Any(ch => !char.IsLetterOrDigit(ch))) score += 15;

            score = Math.Min(100, score);
            strengthBar.Value = score;

            string strengthText = score >= 80 ? "Strong" : score >= 50 ? "Medium" : "Weak";
            strengthBar.Foreground = score >= 80 ? new SolidColorBrush(Colors.Green) :
                                    score >= 50 ? new SolidColorBrush(Colors.Orange) :
                                    new SolidColorBrush(Colors.Red);
            lblStrength.Text = $"Strength: {strengthText} ({score}%)";

            string feedback = "";
            if (pwd.Length < 8) feedback += "• Make it at least 8 characters long.\n";
            if (!pwd.Any(char.IsUpper)) feedback += "• Add uppercase letters.\n";
            if (!pwd.Any(char.IsLower)) feedback += "• Add lowercase letters.\n";
            if (!pwd.Any(char.IsDigit)) feedback += "• Include numbers.\n";
            if (!pwd.Any(ch => !char.IsLetterOrDigit(ch))) feedback += "• Use special characters (e.g., !@#$).\n";
            if (string.IsNullOrEmpty(feedback)) feedback = "Great password!";
            lblFeedback.Text = feedback;
        }

        private void BackToChat_Click(object sender, RoutedEventArgs e)
        {
            BackToChatRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}