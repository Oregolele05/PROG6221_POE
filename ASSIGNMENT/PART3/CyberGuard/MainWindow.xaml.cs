using System.Windows;

namespace CyberGuard
{
    public partial class MainWindow : Window
    {
        private CyberTaskManager _taskManager;
        private CyberQuiz _quiz;
        private CyberSpace _space;
        private ChatControl _chatControl;
        private TaskControl _taskControl;
        private QuizControl _quizControl;
        private ActivityLogControl _logControl;
        private PasswordCheckerControl _passwordCheckerControl;

        public MainWindow()
        {
            InitializeComponent();

            _taskManager = new CyberTaskManager();
            _taskManager.Initialise();

            _quiz = new CyberQuiz();
            _space = new CyberSpace(_taskManager, _quiz);

            _chatControl = new ChatControl(_space);
            _taskControl = new TaskControl(_taskManager);
            _quizControl = new QuizControl(_quiz);
            _logControl = new ActivityLogControl();
            _passwordCheckerControl = new PasswordCheckerControl();

            // Hook Back to Chat events
            _taskControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
            _quizControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
            _logControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;
            _passwordCheckerControl.BackToChatRequested += (s, e) => MainContent.Content = _chatControl;

            MainContent.Content = _chatControl;
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _chatControl;
        private void TasksButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _taskControl;
        private void QuizButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _quizControl;
        private void LogButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _logControl;
        private void PasswordCheckerButton_Click(object sender, RoutedEventArgs e) => MainContent.Content = _passwordCheckerControl;
    }
}