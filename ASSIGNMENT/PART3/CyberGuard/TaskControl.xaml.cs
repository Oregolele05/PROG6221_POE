using System;
using System.Windows;
using System.Windows.Controls;

namespace CyberGuard
{
    public partial class TaskControl : UserControl
    {
        private readonly CyberTaskManager _taskManager;
        public event EventHandler BackToChatRequested;

        public TaskControl(CyberTaskManager taskManager)
        {
            InitializeComponent();
            _taskManager = taskManager;
            RefreshTasks();
        }

        private void RefreshTasks()
        {
            var tasks = _taskManager.GetAllTasks();
            lvTasks.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            if (string.IsNullOrEmpty(title)) { MessageBox.Show("Title is required."); return; }
            string desc = txtDesc.Text.Trim();
            DateTime? reminder = dpReminder.SelectedDate;
            int id = _taskManager.AddTask(title, desc, reminder);
            CyberLogger.Add($"Task added via GUI: '{title}' (ID {id})");
            RefreshTasks();
            txtTitle.Clear(); txtDesc.Clear(); dpReminder.SelectedDate = null;
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvTasks.SelectedItem as CyberTask;
            if (selected == null) { MessageBox.Show("Select a task first."); return; }
            _taskManager.CompleteTask(selected.Id);
            CyberLogger.Add($"Task {selected.Id} completed via GUI.");
            RefreshTasks();
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvTasks.SelectedItem as CyberTask;
            if (selected == null) { MessageBox.Show("Select a task first."); return; }
            if (MessageBox.Show($"Delete task '{selected.Title}'?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _taskManager.DeleteTask(selected.Id);
                CyberLogger.Add($"Task {selected.Id} deleted via GUI.");
                RefreshTasks();
            }
        }

        private void SetReminder_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvTasks.SelectedItem as CyberTask;
            if (selected == null) { MessageBox.Show("Select a task first."); return; }
            var dialog = new ReminderDialog();
            if (dialog.ShowDialog() == true)
            {
                _taskManager.SetReminder(selected.Id, dialog.SelectedDate.Value);
                CyberLogger.Add($"Reminder set for task {selected.Id} via GUI.");
                RefreshTasks();
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e) => RefreshTasks();

        private void BackToChat_Click(object sender, RoutedEventArgs e)
        {
            BackToChatRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}