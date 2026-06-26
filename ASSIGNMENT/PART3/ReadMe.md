# 🛡️ CyberGuard — Cyber Awareness Chatbot

**PROG6221 POE – Part 3**  
**WPF Application | MySQL | NLP Simulation**

---

## 📋 Description

CyberGuard is a WPF desktop chatbot built in C# using .NET Framework 4.7.2. It is specifically engineered to interactively educate users on cybersecurity pillars—including **Password Safety**, **Phishing Awareness**, and **Safe Browsing**—while providing practical tools for task management, a cybersecurity quiz, real‑time password strength analysis, and a complete activity log.

The application uses **MySQL** to persist tasks and features a sidebar navigation for seamless switching between the chat, task manager, quiz, password checker, and log viewer. It also includes a robust NLP simulation with keyword recognition, sentiment detection, memory, and follow‑up handling.

---

## ✨ Features

| Category | Features |
|----------|----------|
| **🤖 Chatbot** | – Personalised greeting (name collection) <br> – Topic menus: Password Safety, Phishing, Safe Browsing <br> – Keyword recognition (15+ cybersecurity terms) <br> – Sentiment detection (worried, confused, frustrated, curious) <br> – Memory: remembers name, favourite topic, time per topic <br> – Follow‑up handling ("Tell me more", "Another tip") |
| **📋 Task Manager** | – Add tasks (title + description + due date) <br> – Parse natural language: `"Add task - Title, Description - ..., Due date - 26 June 2026"` <br> – Complete / Delete tasks <br> – Set reminders via DatePicker or chat <br> – MySQL database persistence |
| **🎮 Cybersecurity Quiz** | – 15 questions covering phishing, passwords, safe browsing, social engineering <br> – 3 question types: Multiple Choice, True/False, Multi‑Select <br> – Immediate feedback with explanations <br> – Score tracking and encouraging final message <br> – Auto‑advance to next question |
| **🔐 Password Checker** | – Real‑time strength analysis (length, uppercase, lowercase, digits, special chars) <br> – Visual progress bar (0‑100%) with colour coding <br> – Actionable suggestions to improve password |
| **📜 Activity Log** | – Records all significant actions with timestamps <br> – Live updates via ObservableCollection <br> – Limited to 20 entries to stay concise <br> – Accessible via `"Show activity log"` chat command or Log panel |


### Chat Commands

| Command | Example | Action |
|---------|---------|--------|
| **Add Task** | `Add task - Enable 2FA, Description - Set up authenticator, Due date - 30 June 2026` | Creates a task with title, description, and due date |
| **Show Tasks** | `Show my tasks` | Lists all tasks |
| **Complete Task** | `Complete task 3` | Marks task 3 as completed |
| **Delete Task** | `Delete task 2` | Deletes task 2 |
| **Set Reminder** | `Set reminder 1 in 5 days` | Sets a reminder for task 1 |
| **Start Quiz** | `Start quiz` or `Quiz me` | Begins the cybersecurity quiz |
| **Activity Log** | `Show activity log` or `What have you done for me?` | Displays recent actions |
| **Topics** | `Tell me about phishing` | Provides information on a topic |
| **Exit** | `Exit` | Ends the session with a summary |

### Contextual Streams
- Type `"tell me more"`, `"explain more"`, or `"another tip"` to receive follow‑up educational info.

### Termination
- Type `"exit"` or select option `4` to view your custom engagement metrics (time spent per topic, total questions asked, favourite topic).

### GUI Navigation
- Use the **sidebar** (Chat, Tasks, Quiz, Password, Log) to switch between panels.
- Each panel has a **"Back to Chat"** button for easy navigation.

---

## 📂 Project Structure
CyberGuard/
│
├── Core Classes
│ ├── CyberDesign.cs # Base display logic (colours, formatting)
│ ├── CyberChatDisplay.cs # WPF RichTextBox bridge
│ ├── CyberSpace.cs # Main chatbot engine (conversation flow)
│ ├── CyberUser.cs # Session memory (name, favourite topic, time)
│ ├── CyberTips.cs # NLP, keywords, tips, sentiment
│ ├── CyberTask.cs # Task entity
│ ├── CyberTaskManager.cs # MySQL CRUD operations
│ ├── CyberQuiz.cs # Quiz engine (15 questions, 3 types)
│ └── CyberLogger.cs # Static activity logger
│
├── XAML Controls (GUI)
│ ├── ChatControl.xaml/.cs # Chat interface
│ ├── TaskControl.xaml/.cs # Task management panel
│ ├── QuizControl.xaml/.cs # Quiz panel
│ ├── PasswordCheckerControl.xaml/.cs # Password strength checker
│ ├── ActivityLogControl.xaml/.cs # Activity log viewer
│ └── ReminderDialog.xaml/.cs # Date picker dialog
│
├── Main
│ ├── App.xaml/.cs # Application entry point
│ ├── MainWindow.xaml/.cs # Navigation hub (sidebar)
│ └── Properties/ # Assembly info, resources
│
└── Resources
└── greet.wav # Optional voice greeting

---

## ❗ Troubleshooting

| Problem | Solution |
|---------|----------|
| **MySQL connection error** | Ensure MySQL is running (`services.msc` → MySQL80). Check the password in `CyberTaskManager.cs`. |
| **"Access denied" for root** | Use the correct root password set during MySQL installation. |
| **App crashes on startup** | A message box will show the error. Common cause: MySQL not running or wrong connection string. |
| **Tasks not saving** | Confirm MySQL is running and the connection string is correct. |
| **`StaticResource` errors** | All styles are defined in `App.xaml`. Ensure no duplicate keys exist. |

---

## 📺 Presentation & Repository

- **YouTube Presentation:** [https://youtu.be/your-link-here](https://youtu.be/your-link-here)
- **GitHub Repository:** [https://github.com/yourusername/CyberGuard](https://github.com/yourusername/CyberGuard)


