using System;
using System.Windows.Controls;
using System.Windows;

namespace CyberGuard
{
    public partial class ActivityLogControl : UserControl
    {
        public event EventHandler BackToChatRequested;

        public ActivityLogControl()
        {
            InitializeComponent();
            UpdateEmptyMessage();
            CyberLogger.Log.CollectionChanged += (s, e) => UpdateEmptyMessage();
        }

        private void UpdateEmptyMessage()
        {
            bool hasItems = CyberLogger.Log.Count > 0;
            lstLog.Visibility = hasItems ? Visibility.Visible : Visibility.Collapsed;
            emptyMsg.Visibility = hasItems ? Visibility.Collapsed : Visibility.Visible;
        }

        private void BackToChat_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BackToChatRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}