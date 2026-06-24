using System.Collections.Generic;
using System.Linq;

namespace CyberGuard
{
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        MultiSelect
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public List<int> CorrectIndices { get; set; }
        public string Explanation { get; set; }
        public QuestionType Type { get; set; }
        public bool IsTrueFalse => Type == QuestionType.TrueFalse;
        public bool IsMultiSelect => Type == QuestionType.MultiSelect;
        public bool IsSingleChoice => Type == QuestionType.MultipleChoice;
    }

    public class CyberQuiz
    {
        private List<QuizQuestion> _questions = new List<QuizQuestion>();
        private int _currentIndex = -1;
        private int _score = 0;
        public bool IsActive { get; private set; } = false;
        public int TotalQuestions => _questions.Count;
        public int CurrentScore => _score;

        public CyberQuiz() => LoadQuestions();

        private void LoadQuestions()
        {
            _questions = new List<QuizQuestion>
            {
                // Multiple Choice (1)
                new QuizQuestion
                {
                    Question = "What is the most common form of cyber attack?",
                    Options = new List<string> { "Phishing", "Malware", "Ransomware", "Social Engineering" },
                    CorrectIndices = new List<int> { 0 },
                    Explanation = "Phishing is the most common because it exploits human error.",
                    Type = QuestionType.MultipleChoice
                },
                // Multiple Choice (2)
                new QuizQuestion
                {
                    Question = "Which of the following is a strong password?",
                    Options = new List<string> { "123456", "password", "P@ssw0rd!", "qwerty" },
                    CorrectIndices = new List<int> { 2 },
                    Explanation = "A strong password includes uppercase, lowercase, numbers, and symbols.",
                    Type = QuestionType.MultipleChoice
                },
                // True/False (1)
                new QuizQuestion
                {
                    Question = "Using the same password for multiple accounts is safe.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Reusing passwords increases the risk of multiple accounts being compromised.",
                    Type = QuestionType.TrueFalse
                },
                // Multiple Choice (3)
                new QuizQuestion
                {
                    Question = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A second password", "A code sent to your phone", "A biometric scan", "A security question" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "2FA adds a code from a separate device for extra security.",
                    Type = QuestionType.MultipleChoice
                },
                // True/False (2)
                new QuizQuestion
                {
                    Question = "You should click on links in emails from unknown senders.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Links in unknown emails may lead to phishing sites or malware.",
                    Type = QuestionType.TrueFalse
                },
                // Multiple Choice (4)
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string> { "A programming language", "Manipulating people into revealing info", "A type of malware", "A network protocol" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Social engineering tricks people into giving away confidential information.",
                    Type = QuestionType.MultipleChoice
                },
                // True/False (3)
                new QuizQuestion
                {
                    Question = "Public Wi-Fi is always safe to use for online banking.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Public Wi-Fi is insecure; use a VPN for sensitive transactions.",
                    Type = QuestionType.TrueFalse
                },
                // Multiple Choice (5)
                new QuizQuestion
                {
                    Question = "What does HTTPS stand for?",
                    Options = new List<string> { "HyperText Transfer Protocol Secure", "High Tech Transfer Protocol Secure", "Hyper Transfer Text Secure", "None of the above" },
                    CorrectIndices = new List<int> { 0 },
                    Explanation = "HTTPS encrypts data between your browser and the website.",
                    Type = QuestionType.MultipleChoice
                },
                // True/False (4)
                new QuizQuestion
                {
                    Question = "Antivirus software alone is enough to protect you from all cyber threats.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Antivirus helps but you also need safe browsing habits and updates.",
                    Type = QuestionType.TrueFalse
                },
                // Multiple Choice (6)
                new QuizQuestion
                {
                    Question = "What is ransomware?",
                    Options = new List<string> { "A type of firewall", "Software that encrypts your files and demands payment", "A password manager", "A secure email service" },
                    CorrectIndices = new List<int> { 1 },
                    Explanation = "Ransomware locks your files and demands money to unlock them.",
                    Type = QuestionType.MultipleChoice
                },
                // True/False (5)
                new QuizQuestion
                {
                    Question = "You should update your software regularly.",
                    Options = new List<string> { "True", "False" },
                    CorrectIndices = new List<int> { 0 },
                    Explanation = "Updates fix security vulnerabilities.",
                    Type = QuestionType.TrueFalse
                },
                // Multiple Choice (7)
                new QuizQuestion
                {
                    Question = "What is the best way to avoid phishing emails?",
                    Options = new List<string> { "Reply to them", "Click the links to check", "Verify the sender and look for red flags", "Ignore all emails" },
                    CorrectIndices = new List<int> { 2 },
                    Explanation = "Always verify the sender and be cautious of urgent requests.",
                    Type = QuestionType.MultipleChoice
                },
                // --- Multi‑Select Questions ---
                new QuizQuestion
                {
                    Question = "Which of the following are signs of a phishing email? (Select all that apply)",
                    Options = new List<string> { "Urgent language", "Mismatched URL", "Personalised greeting", "Request for password" },
                    CorrectIndices = new List<int> { 0, 1, 3 },
                    Explanation = "Phishing emails often create urgency, have mismatched links, and ask for passwords.",
                    Type = QuestionType.MultiSelect
                },
                new QuizQuestion
                {
                    Question = "Which of the following are strong password practices? (Select all that apply)",
                    Options = new List<string> { "Using a password manager", "Changing password every month", "Using a passphrase", "Sharing with friends" },
                    CorrectIndices = new List<int> { 0, 2 },
                    Explanation = "Password managers and passphrases are recommended. Frequent changes are not always necessary, and sharing is risky.",
                    Type = QuestionType.MultiSelect
                },
                new QuizQuestion
                {
                    Question = "What should you do if you suspect a data breach? (Select all that apply)",
                    Options = new List<string> { "Change passwords", "Notify your bank", "Ignore it", "Enable 2FA" },
                    CorrectIndices = new List<int> { 0, 1, 3 },
                    Explanation = "Take immediate action: change passwords, notify bank, enable 2FA. Ignoring makes it worse.",
                    Type = QuestionType.MultiSelect
                }
            };
        }

        public void Start()
        {
            _currentIndex = 0;
            _score = 0;
            IsActive = true;
            CyberLogger.Add("Quiz started.");
        }

        public QuizQuestion GetCurrentQuestion()
        {
            if (!IsActive || _currentIndex >= _questions.Count)
                return null;
            return _questions[_currentIndex];
        }

        public bool SubmitAnswer(List<int> selectedIndices)
        {
            if (!IsActive) return false;
            var q = _questions[_currentIndex];
            bool correct = q.CorrectIndices.OrderBy(i => i).SequenceEqual(selectedIndices.OrderBy(i => i));
            if (correct) _score++;
            CyberLogger.Add($"Quiz: {(correct ? "Correct" : "Wrong")} for Q{_currentIndex + 1}");
            _currentIndex++;
            if (_currentIndex >= _questions.Count)
            {
                IsActive = false;
                CyberLogger.Add($"Quiz completed: {_score}/{_questions.Count}");
            }
            return correct;
        }

        public string GetResultMessage()
        {
            int total = _questions.Count;
            int score = _score;
            string feedback = score == total ? "🌟 Perfect! You're a cybersecurity pro!" :
                              score >= total * 0.7 ? "Great job! Keep learning!" :
                              "Keep studying to stay safe online!";
            return $"You scored {score} out of {total}.\n{feedback}";
        }
    }
}