using System;
using System.Windows.Controls;

namespace CyberGuard
{
    public partial class ActivityLogControl : UserControl
    {
        public event EventHandler BackToChatRequested;

        public ActivityLogControl() => InitializeComponent();

        private void BackToChat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BackToChatRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}