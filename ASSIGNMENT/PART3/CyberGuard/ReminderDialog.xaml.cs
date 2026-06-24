using System;
using System.Windows;

namespace CyberGuard
{
    public partial class ReminderDialog : Window
    {
        public DateTime? SelectedDate => dpReminder.SelectedDate;

        public ReminderDialog()
        {
            InitializeComponent();
            dpReminder.SelectedDate = DateTime.Today.AddDays(1);
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!dpReminder.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date.");
                return;
            }
            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => DialogResult = false;
    }
}