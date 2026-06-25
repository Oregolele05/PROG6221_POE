using System;
using System.Windows;
using System.Windows.Controls;

namespace CyberGuard
{
    public partial class MainWindow : Window
    {
        private readonly CyberTaskManager _taskManager;
        private readonly CyberQuiz _quiz;
        private readonly CyberSpace _space;
        private readonly ChatControl _chatControl;
        private readonly TaskControl _taskControl;
        private readonly QuizControl _quizControl;
        private readonly PasswordCheckerControl _passwordCheckerControl;
        private readonly ActivityLogControl _logControl;

        public MainWindow()
        {
            try
            {
                InitializeComponent();

                _taskManager = new CyberTaskManager();
                _taskManager.Initialise(); // May throw if MySQL isn't running or DB error

                _quiz = new CyberQuiz();
                _space = new CyberSpace(_taskManager, _quiz);

                _chatControl = new ChatControl(_space);
                _taskControl = new TaskControl(_taskManager);
                _quizControl = new QuizControl(_quiz);
                _passwordCheckerControl = new PasswordCheckerControl();
                _logControl = new ActivityLogControl();

                _taskControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
                _quizControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
                _passwordCheckerControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
                _logControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;

                MainContent.Content = _chatControl;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup Error:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                                "CyberGuard Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Optionally, you can close the app:
                // Application.Current.Shutdown();
                // Or keep the window open with an error message in the content.
                // For simplicity, we'll set the main content to a text block.
                MainContent.Content = new TextBlock
                {
                    Text = $"Error loading application:\n{ex.Message}",
                    Foreground = System.Windows.Media.Brushes.Red,
                    FontSize = 16,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(20)
                };
            }
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _chatControl;
        private void TasksButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _taskControl;
        private void QuizButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _quizControl;
        private void PasswordCheckerButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _passwordCheckerControl;
        private void LogButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _logControl;
    }
}