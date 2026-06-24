using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberGuard
{
    public partial class QuizControl : UserControl
    {
        private CyberQuiz _quiz;
        private QuizQuestion _currentQuestion;
        public event EventHandler BackToChatRequested;

        public QuizControl(CyberQuiz quiz)
        {
            InitializeComponent();
            _quiz = quiz;
            ShowStartState();
        }

        private void ShowStartState()
        {
            lblQuestion.Text = "Press 'Start New Quiz' to begin.";
            OptionsPanel.Children.Clear();
            lblFeedback.Text = "";
            lblScore.Text = "";
            btnSubmit.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            _quiz.Start();
            btnStart.IsEnabled = false;
            btnSubmit.IsEnabled = true;
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            _currentQuestion = _quiz.GetCurrentQuestion();
            if (_currentQuestion == null)
            {
                EndQuiz();
                return;
            }

            lblQuestion.Text = _currentQuestion.Question;
            OptionsPanel.Children.Clear();

            // Build options based on question type
            if (_currentQuestion.IsTrueFalse)
            {
                var rbTrue = new RadioButton { Content = "True", GroupName = "QuizGroup", Tag = 0, Foreground = Brushes.White };
                var rbFalse = new RadioButton { Content = "False", GroupName = "QuizGroup", Tag = 1, Foreground = Brushes.White };
                OptionsPanel.Children.Add(rbTrue);
                OptionsPanel.Children.Add(rbFalse);
            }
            else if (_currentQuestion.IsMultiSelect)
            {
                for (int i = 0; i < _currentQuestion.Options.Count; i++)
                {
                    var cb = new CheckBox { Content = _currentQuestion.Options[i], Tag = i, Foreground = Brushes.White };
                    OptionsPanel.Children.Add(cb);
                }
            }
            else // MultipleChoice
            {
                for (int i = 0; i < _currentQuestion.Options.Count; i++)
                {
                    var rb = new RadioButton { Content = _currentQuestion.Options[i], GroupName = "QuizGroup", Tag = i, Foreground = Brushes.White };
                    OptionsPanel.Children.Add(rb);
                }
            }

            lblFeedback.Text = "";
            lblScore.Text = $"Score: {_quiz.CurrentScore}";
            btnSubmit.IsEnabled = true;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_currentQuestion == null) return;

            List<int> selected = new List<int>();

            if (_currentQuestion.IsMultiSelect)
            {
                foreach (var child in OptionsPanel.Children)
                {
                    if (child is CheckBox cb && cb.IsChecked == true)
                        selected.Add((int)cb.Tag);
                }
            }
            else // radio buttons
            {
                foreach (var child in OptionsPanel.Children)
                {
                    if (child is RadioButton rb && rb.IsChecked == true)
                    {
                        selected.Add((int)rb.Tag);
                        break;
                    }
                }
            }

            if (selected.Count == 0)
            {
                MessageBox.Show("Please select an option.");
                return;
            }

            bool correct = _quiz.SubmitAnswer(selected);
            lblFeedback.Text = correct ? "✅ Correct!" : $"❌ Wrong. The correct answer(s): " +
                               string.Join(", ", _currentQuestion.CorrectIndices.Select(i => _currentQuestion.Options[i]));
            lblFeedback.Text += $"\nExplanation: {_currentQuestion.Explanation}";
            btnSubmit.IsEnabled = false;

            // Auto‑advance after 2 seconds
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                var next = _quiz.GetCurrentQuestion();
                if (next != null)
                    ShowQuestion();
                else
                    EndQuiz();
            };
            timer.Start();
        }

        private void EndQuiz()
        {
            btnSubmit.IsEnabled = false;
            btnStart.IsEnabled = true;
            lblQuestion.Text = _quiz.GetResultMessage();
            OptionsPanel.Children.Clear();
            lblFeedback.Text = "";
            lblScore.Text = $"Final Score: {_quiz.CurrentScore}";
        }

        private void BackToChat_Click(object sender, RoutedEventArgs e)
        {
            BackToChatRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}