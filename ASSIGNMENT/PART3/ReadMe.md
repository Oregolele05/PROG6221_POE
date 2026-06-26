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


---

## 📊 Class Descriptions

| **Class** | **Purpose** |
|-----------|-------------|
| `CyberDesign.cs` | Base class for all visual output; handles colours, message formatting, logo display |
| `CyberChatDisplay.cs` | Bridges CyberDesign to the WPF RichTextBox; converts colours and handles alignment |
| `CyberSpace.cs` | Main chatbot engine; manages conversation flow, NLP parsing, task/quiz/log handlers |
| `CyberUser.cs` | Session memory; stores username, current section, favourite topic, time per topic |
| `CyberTips.cs` | NLP and content repository; keyword detection, sentiment analysis, random tips |
| `CyberTask.cs` | Task entity model; properties: Id, Title, Description, ReminderDate, IsCompleted |
| `CyberTaskManager.cs` | MySQL data access; AddTask, GetAllTasks, CompleteTask, DeleteTask, SetReminder |
| `CyberQuiz.cs` | Quiz engine; 15 questions, 3 types, scoring, feedback, result messages |
| `CyberLogger.cs` | Static activity logger; ObservableCollection, timestamps, limited to 20 entries |

---

## 🖥️ XAML Controls

| **Control** | **Purpose** |
|-------------|-------------|
| `ChatControl.xaml` | Main chat interface with RichTextBox, input box, and Send button |
| `TaskControl.xaml` | Task management GUI; ListView, input fields, action buttons (Add, Complete, Delete) |
| `QuizControl.xaml` | Quiz panel; dynamically displays questions with RadioButtons or CheckBoxes |
| `PasswordCheckerControl.xaml` | Password strength checker; progress bar, real‑time analysis, suggestions |
| `ActivityLogControl.xaml` | Log viewer; binds to CyberLogger.Log, shows empty message when no entries |
| `ReminderDialog.xaml` | Simple popup dialog with DatePicker for setting reminders |

---

## 🔧 Main Files

| **File** | **Purpose** |
|----------|-------------|
| `App.xaml` | Application entry point; defines global styles and resources |
| `MainWindow.xaml` | Navigation hub; sidebar with buttons that switch between controls |
| `Program.cs` | Application bootstrap; starts the WPF application |

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

- **YouTube Presentation:** https://youtu.be/LNNYVTJw_sI
- **GitHub Repository:** 


