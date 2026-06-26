using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CyberGuard
{
    public delegate string TipProvider();

    public class CyberSpace : CyberDesign
    {
        private CyberUser user = new CyberUser();
        private CyberTips tips = new CyberTips();
        private CyberTaskManager _taskManager;
        private CyberQuiz _quiz;

        public string CurrentSection => user.Section;

        // ── Constructor ──────────────────────────────────────────────
        public CyberSpace(CyberTaskManager taskManager, CyberQuiz quiz)
        {
            _taskManager = taskManager;
            _quiz = quiz;
        }

        // ── Initialise ──────────────────────────────────────────────
        public void Initialise(CyberChatDisplay chatDisplay)
        {
            ChatDisplay = chatDisplay;
            _taskManager.Initialise();
            CyberLogger.Add("Chatbot started.");
        }

        // ── Welcome ──────────────────────────────────────────────────
        public void WelcomeScreen()
        {
            LogoDisplay();
            BotLine();
            BotSay("Welcome to CyberGuard - your Cyber Awareness Chatbot!");
            BotSay("Before we begin, what is your name?");
            user.Section = "getname";
        }

        // ── User Interaction (name) ─────────────────────────────────
        public void UserInteraction(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            { BotWarn("Please enter a valid name."); return; }
            if (input.Any(char.IsDigit))
            { BotWarn("A name cannot contain numeric values."); return; }
            string invalidChars = "!@#$%^&*()_=-<>.?/\\|+][{}\":;'~`_";
            if (input.Any(ch => invalidChars.Contains(ch)))
            { BotWarn("A name cannot contain special characters."); return; }

            user.username = input.Trim();
            BotLine();
            Box($"Welcome {user.username} nice to meet you!!");
            BotLine();
            ShowMainMenu();
        }

        // ── Main Menu ────────────────────────────────────────────────
        public void ShowMainMenu()
        {
            if (user.Section == "password" || user.Section == "phishing" || user.Section == "safebrowsing")
                user.TrackTopic("");

            user.Section = "main";
            BotSay($"How can I help you today, {user.username}?");
            BotInfo("1. How are you?");
            BotInfo("2. What is your purpose?");
            BotInfo("3. What can I ask you about?");
            BotInfo("4. Exit");
            BotInfo("(Or ask about tasks, quiz, activity log, etc.)");
        }

        // ── Response System ──────────────────────────────────────────
        public void ResponseSystem(string input)
        {
            string cleanInput = input.ToLower().Trim();

            // ── Pending action (multi-step) ─────────────────────────
            if (!string.IsNullOrEmpty(pendingAction))
            {
                HandlePendingInput(cleanInput);
                return;
            }

            // ── Quiz active ──────────────────────────────────────────
            if (_quiz.IsActive)
            {
                ProcessQuizAnswer(cleanInput);
                return;
            }

            // ── NLP: Task / Reminder / Quiz / Log commands ──────────
            if (cleanInput.Contains("add task") || cleanInput.Contains("new task") ||
                cleanInput.Contains("create task") || cleanInput.Contains("remind me to"))
            {
                HandleTaskCommand(input);
                return;
            }
            if (cleanInput.Contains("show task") || cleanInput.Contains("list task") ||
                cleanInput.Contains("view task") || cleanInput.Contains("my tasks"))
            {
                ShowTasks();
                return;
            }
            if (cleanInput.Contains("complete task") || cleanInput.Contains("mark task"))
            {
                HandleCompleteTask(input);
                return;
            }
            if (cleanInput.Contains("delete task") || cleanInput.Contains("remove task"))
            {
                HandleDeleteTask(input);
                return;
            }
            if (cleanInput.Contains("set reminder") || cleanInput.Contains("remind in"))
            {
                HandleSetReminder(input);
                return;
            }
            if (cleanInput.Contains("quiz") || cleanInput.Contains("game"))
            {
                StartQuiz();
                return;
            }
            if (cleanInput.Contains("activity log") || cleanInput.Contains("what have you done") ||
                cleanInput.Contains("show log") || cleanInput.Contains("recent actions"))
            {
                ShowActivityLog();
                return;
            }

            // ── Declared favourite topic memory ─────────────────────
            if (cleanInput.Contains("interested in") || cleanInput.Contains("favourite topic is") || cleanInput.Contains("favorite topic is"))
            {
                string matchedTopic = "";
                if (cleanInput.Contains("password")) matchedTopic = "Password Safety";
                else if (cleanInput.Contains("phishing")) matchedTopic = "Phishing";
                else if (cleanInput.Contains("browsing") || cleanInput.Contains("web")) matchedTopic = "Safe Browsing";

                if (!string.IsNullOrEmpty(matchedTopic))
                {
                    user.declaredFavTopic = matchedTopic;
                    BotSay($"Great! I'll remember that you're interested in {matchedTopic}. It's a crucial part of staying safe online.");
                    ShowMainMenu();
                    return;
                }
            }

            // ── Follow-up detection ──────────────────────────────────
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip") ||
                cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                if (!string.IsNullOrEmpty(user.declaredFavTopic))
                    BotInfo($"💡 As someone interested in {user.declaredFavTopic}, you might want to keep this in mind.");
                ShowMainMenu();
                return;
            }

            // ── Favourite topic recall ──────────────────────────────
            if (cleanInput.Contains("favourite topic") || cleanInput.Contains("favorite topic") || cleanInput.Contains("most interested"))
            {
                if (!string.IsNullOrEmpty(user.favTopic))
                {
                    string sourceContext = !string.IsNullOrEmpty(user.declaredFavTopic) ? "you stated earlier" : "runtime tracking";
                    BotSay($"According to {sourceContext}, your favorite topic is {user.favTopic}. Let me know if you'd like to review it!");
                }
                else
                    BotSay("We haven't spent enough time on an individual topic yet for me to determine your favorite!");
                ShowMainMenu();
                return;
            }

            // ── Sentiment + keyword recognition ──────────────────────
            string sentiment = tips.Sentiment(input);
            string keyword = tips.CheckKeywords(input);
            if (keyword != null)
            {
                user.QuestionCount++;
                if (sentiment != "neutral") BotSay(tips.SentimentResponse(sentiment));
                BotInfo(keyword);
                ShowMainMenu();
                return;
            }

            // ── Numbered menu options ────────────────────────────────
            if (cleanInput.Contains("how are you") || cleanInput == "1" || cleanInput == "one")
            {
                BotSay($"I am doing okay I guess. Thanks for asking, {user.username}.");
                if (!string.IsNullOrEmpty(user.favTopic))
                    BotSay($"Since your favorite topic seems to be {user.favTopic}, we could jump right back in if you're ready!");
                ShowMainMenu();
            }
            else if (cleanInput.Contains("purpose") || cleanInput == "2" || cleanInput == "two")
            {
                BotSay("My purpose is to educate individuals and organizations to recognize, prevent, and respond to cyber threats.");
                ShowMainMenu();
            }
            else if (cleanInput.Contains("about") || cleanInput == "3" || cleanInput == "three")
            {
                ShowTopicMenu();
            }
            else if (cleanInput.Contains("exit") || cleanInput == "4" || cleanInput == "four")
            {
                ShowGoodbye();
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
                ShowMainMenu();
            }
        }

        // ── Follow-up Handler ────────────────────────────────────────
        public void HandleFollowUp()
        {
            if (string.IsNullOrEmpty(user.lastTopic))
            {
                BotWarn("We haven't discussed a specific topic yet. Ask me about phishing, passwords, or safe browsing.");
                return;
            }

            BotSay("Here's another tip on " + user.lastTopic + ":");
            TipProvider getTip;
            string target = user.lastTopic.ToLower();

            if (target.Contains("phishing")) getTip = tips.GetPhishingTip;
            else if (target.Contains("password")) getTip = tips.GetPasswordTip;
            else if (target.Contains("browsing")) getTip = tips.GetSafeBrowsingTip;
            else { BotInfo("No extra tips available on that topic yet."); return; }

            BotInfo(getTip());
        }

        // ── Topic Menu ────────────────────────────────────────────────
        public void ShowTopicMenu()
        {
            user.Section = "topicmenu";
            BotLine();
            BotSay("You can ask me about the following topics:");
            BotInfo("1. Password Safety");
            BotInfo("2. Phishing");
            BotInfo("3. Safe Browsing");
            BotInfo("4. Exit to Main Menu");
        }

        public void HandleTopicMenu(string input)
        {
            string cleanInput = input.ToLower().Trim();

            if (cleanInput.Contains("password") || cleanInput == "1" || cleanInput == "one")
            { user.TrackTopic("Password Safety"); ShowPasswordMenu(); }
            else if (cleanInput.Contains("phishing") || cleanInput == "2" || cleanInput == "two")
            { user.TrackTopic("Phishing"); ShowPhishingMenu(); }
            else if (cleanInput.Contains("browsing") || cleanInput == "3" || cleanInput == "three")
            { user.TrackTopic("Safe Browsing"); ShowSafeBrowsingMenu(); }
            else if (cleanInput.Contains("back") || cleanInput == "4" || cleanInput == "four")
                ShowMainMenu();
            else
                BotWarn("I didn't quite understand that. Could you try choosing options 1 through 4?");
        }

        // ── Password Menu ────────────────────────────────────────────
        public void ShowPasswordMenu()
        {
            user.Section = "password";
            BotHeader("PASSWORD SAFETY");
            TipProvider getTip = tips.GetPasswordTip;
            BotSay("Quick tip: " + getTip());
            BotLine();
            BotSay("What would you like to know about Password Safety?");
            BotInfo("1.  Importance of Strong Passwords");
            BotInfo("2.  How to Create a Strong Password");
            BotInfo("3.  What is Two-Factor Authentication (2FA)?");
            BotInfo("4.  Why use a Password Manager?");
            BotInfo("5.  Common Mistakes to Avoid");
            BotInfo("6.  Give me a random tip");
            BotInfo("7.  Go Back");
        }

        public void HandlePassword(string input)
        {
            string cleanInput = input.ToLower().Trim();

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip") ||
                cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" ||
                cleanInput == "5" || cleanInput == "6" || cleanInput.Contains("importance") ||
                cleanInput.Contains("create") || cleanInput.Contains("2fa") ||
                cleanInput.Contains("manager") || cleanInput.Contains("mistakes") || cleanInput.Contains("random"))
                user.QuestionCount++;

            if (cleanInput.Contains("importance") || cleanInput == "1")
            {
                BotHeader("IMPORTANCE");
                BotInfo("Passwords are your first line of defense against cyber threats.");
            }
            else if (cleanInput.Contains("create") || cleanInput == "2")
            {
                BotHeader("CREATION STRATEGY");
                BotInfo("Use long passphrases with three or more random connected words.");
            }
            else if (cleanInput.Contains("2fa") || cleanInput == "3")
            {
                BotHeader("TWO-FACTOR AUTHENTICATION");
                BotInfo("2FA adds a second layer of security by requiring a code from your mobile.");
            }
            else if (cleanInput.Contains("manager") || cleanInput == "4")
            {
                BotHeader("PASSWORD MANAGERS");
                BotInfo("Managers encrypt individual application login profiles safely within vaults.");
            }
            else if (cleanInput.Contains("mistakes") || cleanInput == "5")
            {
                BotHeader("COMMON MISTAKES");
                BotInfo("Reusing matching verification strings across personal networks.");
            }
            else if (cleanInput.Contains("random") || cleanInput == "6")
            {
                TipProvider getTip = tips.GetPasswordTip;
                BotSay("Random Password Tip: " + getTip());
            }
            else if (cleanInput.Contains("back") || cleanInput == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
                return;
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to return.");
        }

        // ── Phishing Menu ─────────────────────────────────────────────
        public void ShowPhishingMenu()
        {
            user.Section = "phishing";
            BotHeader("PHISHING AWARENESS");
            TipProvider getTip = tips.GetPhishingTip;
            BotSay("Quick tip: " + getTip());
            BotLine();
            BotSay("What would you like to know about Phishing?");
            BotInfo("1.  What is Phishing?");
            BotInfo("2.  How to Identify Phishing Attempts");
            BotInfo("3.  Common Types of Phishing Attacks");
            BotInfo("4.  What to do if you suspect a Scam");
            BotInfo("5.  How to report malicious messages");
            BotInfo("6.  Give me a random tip");
            BotInfo("7.  Go Back");
        }

        public void HandlePhishing(string input)
        {
            string cleanInput = input.ToLower().Trim();

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip") ||
                cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" ||
                cleanInput == "5" || cleanInput == "6" || cleanInput.Contains("what is") ||
                cleanInput.Contains("identify") || cleanInput.Contains("types") ||
                cleanInput.Contains("suspect") || cleanInput.Contains("report") || cleanInput.Contains("random"))
                user.QuestionCount++;

            if (cleanInput.Contains("what is") || cleanInput == "1")
            {
                BotHeader("PHISHING DEFINITION");
                BotInfo("Phishing involves mimicking trusted companies to harvest identities.");
            }
            else if (cleanInput.Contains("identify") || cleanInput == "2")
            {
                BotHeader("IDENTIFICATION METRICS");
                BotInfo("Look out for artificial urgency indicators and mismatched domains.");
            }
            else if (cleanInput.Contains("types") || cleanInput == "3")
            {
                BotHeader("ATTACK TYPES");
                BotInfo("Spear phishing (targeted profile attacks) and Smishing (SMS phishing).");
            }
            else if (cleanInput.Contains("suspect") || cleanInput == "4")
            {
                BotHeader("SUSPICION STRATEGY");
                BotInfo("Avoid opening links. Independently verify the contact directly.");
            }
            else if (cleanInput.Contains("report") || cleanInput == "5")
            {
                BotHeader("REPORTING CHANNELS");
                BotInfo("Forward suspicious activity directly to your IT service desk.");
            }
            else if (cleanInput.Contains("random") || cleanInput == "6")
            {
                TipProvider getTip = tips.GetPhishingTip;
                BotSay("Random Phishing Tip: " + getTip());
            }
            else if (cleanInput.Contains("back") || cleanInput == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
                return;
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to return.");
        }

        // ── Safe Browsing Menu ───────────────────────────────────────
        public void ShowSafeBrowsingMenu()
        {
            user.Section = "safebrowsing";
            BotHeader("SAFE BROWSING");
            TipProvider getTip = tips.GetSafeBrowsingTip;
            BotSay("Quick tip: " + getTip());
            BotLine();
            BotSay("What would you like to know about Safe Browsing?");
            BotInfo("1.  Definition");
            BotInfo("2.  Common Risks Online");
            BotInfo("3.  How to Browse Safely");
            BotInfo("4.  Tools That Help");
            BotInfo("5.  Good Habits");
            BotInfo("6.  Give me a random tip");
            BotInfo("7.  Go Back");
        }

        public void HandleSafeBrowsing(string input)
        {
            string cleanInput = input.ToLower().Trim();

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip") ||
                cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" ||
                cleanInput == "5" || cleanInput == "6" || cleanInput.Contains("definition") ||
                cleanInput.Contains("risks") || cleanInput.Contains("how to") ||
                cleanInput.Contains("tools") || cleanInput.Contains("habits") || cleanInput.Contains("random"))
                user.QuestionCount++;

            if (cleanInput.Contains("definition") || cleanInput == "1")
            {
                BotHeader("DEFINITION");
                BotInfo("Safe browsing is the practice of navigating the internet securely to protect");
                BotInfo("your devices, personal information, and identity from cyber threats.");
            }
            else if (cleanInput.Contains("risks") || cleanInput == "2")
            {
                BotHeader("COMMON RISKS ONLINE");
                BotInfo("1. Malicious websites that download malware automatically.");
                BotInfo("2. Fake shopping sites designed to steal payment details.");
            }
            else if (cleanInput.Contains("how to") || cleanInput == "3")
            {
                BotHeader("HOW TO BROWSE SAFELY");
                BotInfo("1. Always check for HTTPS and a padlock icon in the address bar.");
                BotInfo("2. Use a VPN on public Wi-Fi networks.");
            }
            else if (cleanInput.Contains("tools") || cleanInput == "4")
            {
                BotHeader("TOOLS THAT HELP");
                BotInfo("1. VPN (Virtual Private Network) - Encrypts your internet connection.");
                BotInfo("2. Password Manager              - Autofills only on legitimate sites.");
            }
            else if (cleanInput.Contains("habits") || cleanInput == "5")
            {
                BotHeader("GOOD HABITS");
                BotInfo("1. Log out of accounts when done, especially on shared devices.");
                BotInfo("2. Clear cookies and cache regularly.");
            }
            else if (cleanInput.Contains("random") || cleanInput == "6")
            {
                TipProvider getTip = tips.GetSafeBrowsingTip;
                BotSay("Random Browsing Tip: " + getTip());
            }
            else if (cleanInput.Contains("back") || cleanInput == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
                return;
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to return.");
        }

        // ── Goodbye ───────────────────────────────────────────────────
        public void ShowGoodbye()
        {
            user.TrackTopic("");
            user.Section = "goodbye";
            BotLine();
            Box("SESSION TERMINATED BY USER");
            BotSay("Thank you for using CyberGuard, " + user.username + "!");
            BotHeader("YOUR ENGAGEMENT PROFILE STATS");
            BotInfo($"Total Educational Questions Asked: {user.QuestionCount}");
            if (!string.IsNullOrEmpty(user.favTopic))
                BotInfo($"Your Most Discussed Topic: {user.favTopic}");
            BotLine();
            BotInfo("Time Spent Breakdown per Topic:");
            foreach (var record in user.TopicDurations)
                if (!string.IsNullOrEmpty(record.Key))
                    BotInfo($"• {record.Key}: {record.Value.Seconds} seconds");
            BotLine();
        }

        // ──────────────────────────────────────────────────────────────
        // ── TASK METHODS (ENHANCED PARSING) ────────────────────────────
        // ──────────────────────────────────────────────────────────────

        private string pendingAction = "";
        private CyberTask pendingTask = null;

        private void HandleTaskCommand(string input)
        {
            // Try to parse title and due date
            if (ParseTaskInput(input, out string title, out DateTime? dueDate))
            {
                if (string.IsNullOrEmpty(title))
                {
                    BotWarn("Please specify a task description.");
                    return;
                }

                int ID = _taskManager.AddTask(title, "No description", dueDate);
                CyberLogger.Add($"Task added: '{title}' (ID {ID})" + (dueDate.HasValue ? $" due {dueDate.Value.ToShortDateString()}" : ""));
                BotSay($"Task added with ID {ID}: '{title}'" + (dueDate.HasValue ? $" with reminder on {dueDate.Value.ToShortDateString()}" : "."));
                BotSay("Use 'show tasks' to view all tasks, or 'complete task <id>' to mark as done.");
                return;
            }

            // Fallback: simple add without due date
            string lower = input.ToLower();
            string simpleTitle = "";
            int start = -1;

            if (lower.Contains("add task"))
                start = lower.IndexOf("add task") + "add task".Length;
            else if (lower.Contains("new task"))
                start = lower.IndexOf("new task") + "new task".Length;
            else if (lower.Contains("create task"))
                start = lower.IndexOf("create task") + "create task".Length;
            else if (lower.Contains("remind me to"))
                start = lower.IndexOf("remind me to") + "remind me to".Length;

            if (start >= 0 && start < input.Length)
                simpleTitle = input.Substring(start).Trim();
            else
            {
                BotWarn("I didn't understand the task. Please say: 'add task <description>' or 'add task <description> due <date>'.");
                return;
            }

            if (string.IsNullOrEmpty(simpleTitle))
            {
                BotWarn("Task description is empty.");
                return;
            }

            int id = _taskManager.AddTask(simpleTitle, "No description");
            CyberLogger.Add($"Task added: '{simpleTitle}' (ID {id})");
            BotSay($"Task added with ID {id}: '{simpleTitle}'. Would you like to set a reminder? (yes/no)");
            pendingAction = "addtask";
            pendingTask = new CyberTask { Id = id, Title = simpleTitle };
        }

        /// <summary>
        /// Parses a user input like "Add task - Submit assignment, Due date - 26 June 2026"
        /// Extracts title and due date.
        /// </summary>
        private bool ParseTaskInput(string input, out string title, out DateTime? dueDate)
        {
            title = "";
            dueDate = null;

            // Look for common separators: "due", "deadline", "by", "due date", "due on"
            string[] dateKeywords = { "due date", "deadline", "due on", "by", "due" };
            string lowerInput = input.ToLower();

            int dateIndex = -1;
            string dateKeyword = "";
            foreach (var keyword in dateKeywords)
            {
                int idx = lowerInput.IndexOf(keyword);
                if (idx != -1)
                {
                    dateIndex = idx;
                    dateKeyword = keyword;
                    break;
                }
            }

            if (dateIndex != -1)
            {
                // Split into title part and date part
                string before = input.Substring(0, dateIndex).Trim();
                string after = input.Substring(dateIndex + dateKeyword.Length).Trim();

                // Remove "add task", "new task", "create task", "remind me to" from before
                string[] removePrefixes = { "add task", "new task", "create task", "remind me to" };
                foreach (var prefix in removePrefixes)
                {
                    if (before.ToLower().Contains(prefix))
                    {
                        int idx2 = before.ToLower().IndexOf(prefix);
                        before = before.Substring(idx2 + prefix.Length).Trim();
                        break;
                    }
                }

                title = before.Trim().TrimStart('-', ' ', ':'); // remove any leading dash or colon

                // Parse the date
                if (TryParseDate(after, out DateTime parsedDate))
                {
                    dueDate = parsedDate;
                }
                else
                {
                    // If date parsing fails, keep the entire input as title
                    title = input.Trim().TrimStart('-', ' ', ':');
                    // Remove "add task" from title if present
                    foreach (var prefix in removePrefixes)
                    {
                        if (title.ToLower().Contains(prefix))
                        {
                            int idx2 = title.ToLower().IndexOf(prefix);
                            title = title.Substring(idx2 + prefix.Length).Trim();
                            break;
                        }
                    }
                    dueDate = null;
                    return true;
                }

                if (string.IsNullOrEmpty(title))
                {
                    // If title is empty, put the whole thing as title
                    title = input.Trim().TrimStart('-', ' ', ':');
                    foreach (var prefix in removePrefixes)
                    {
                        if (title.ToLower().Contains(prefix))
                        {
                            int idx2 = title.ToLower().IndexOf(prefix);
                            title = title.Substring(idx2 + prefix.Length).Trim();
                            break;
                        }
                    }
                }
                return true;
            }

            // No date keyword found – treat entire input as title (but remove "add task" etc.)
            string raw = input.Trim();
            string[] removePrefixes2 = { "add task", "new task", "create task", "remind me to" };
            foreach (var prefix in removePrefixes2)
            {
                if (raw.ToLower().Contains(prefix))
                {
                    int idx2 = raw.ToLower().IndexOf(prefix);
                    raw = raw.Substring(idx2 + prefix.Length).Trim();
                    break;
                }
            }
            title = raw;
            dueDate = null;
            return true;
        }

        /// <summary>
        /// Tries to parse a date from various formats.
        /// Supports: "26 June 2026", "2026-06-26", "06/26/2026", "tomorrow", "today", "in 5 days", etc.
        /// </summary>
        private bool TryParseDate(string dateString, out DateTime result)
        {
            result = DateTime.MinValue;
            if (string.IsNullOrEmpty(dateString))
                return false;

            dateString = dateString.Trim().ToLower();

            // Handle relative dates: "today", "tomorrow", "in 5 days"
            if (dateString.Contains("today"))
            {
                result = DateTime.Today;
                return true;
            }
            if (dateString.Contains("tomorrow"))
            {
                result = DateTime.Today.AddDays(1);
                return true;
            }
            if (dateString.Contains("in"))
            {
                // Try to parse "in X days"
                var match = Regex.Match(dateString, @"in\s+(\d+)\s+days?");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int days))
                {
                    result = DateTime.Today.AddDays(days);
                    return true;
                }
            }

            // Try standard date parsing
            if (DateTime.TryParse(dateString, out result))
                return true;

            // Try to parse "26 June 2026" style
            string[] formats = { "d MMMM yyyy", "d MMM yyyy", "MMMM d, yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "dd/MM/yyyy" };
            if (DateTime.TryParseExact(dateString, formats, System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, out result))
                return true;

            return false;
        }

        private void ShowTasks()
        {
            var tasks = _taskManager.GetAllTasks();
            if (tasks.Count == 0)
            {
                BotSay("You have no tasks. Use 'add task <title>' to create one.");
                return;
            }
            BotHeader("Your Cybersecurity Tasks");
            foreach (var t in tasks)
                BotInfo(t.ToString());
            ShowMainMenu();
        }

        private void HandleCompleteTask(string input)
        {
            int id = ExtractId(input);
            if (id <= 0)
            {
                BotWarn("Please specify the task ID to complete, e.g., 'complete task 3'.");
                return;
            }
            bool success = _taskManager.CompleteTask(id);
            if (success)
            {
                CyberLogger.Add($"Task {id} completed.");
                BotSay($"Task {id} marked as completed!");
            }
            else
                BotWarn($"Task {id} not found.");
        }

        private void HandleDeleteTask(string input)
        {
            int id = ExtractId(input);
            if (id <= 0)
            {
                BotWarn("Please specify the task ID to delete, e.g., 'delete task 3'.");
                return;
            }
            bool success = _taskManager.DeleteTask(id);
            if (success)
            {
                CyberLogger.Add($"Task {id} deleted.");
                BotSay($"Task {id} deleted.");
            }
            else
                BotWarn($"Task {id} not found.");
        }

        private void HandleSetReminder(string input)
        {
            int id = ExtractId(input);
            if (id <= 0)
            {
                BotWarn("Please specify the task ID and days, e.g., 'set reminder 2 in 5 days'.");
                return;
            }
            int days = ExtractDays(input);
            if (days <= 0)
            {
                BotWarn("Please specify a valid number of days, e.g., 'in 5 days'.");
                return;
            }
            DateTime reminder = DateTime.Today.AddDays(days);
            bool success = _taskManager.SetReminder(id, reminder);
            if (success)
            {
                CyberLogger.Add($"Reminder set for task {id} on {reminder.ToShortDateString()}.");
                BotSay($"Reminder set for task {id} on {reminder.ToShortDateString()}.");
            }
            else
                BotWarn($"Task {id} not found.");
        }

        private int ExtractId(string input)
        {
            var words = input.Split(' ');
            foreach (var w in words)
                if (int.TryParse(w, out int id))
                    return id;
            return -1;
        }

        private int ExtractDays(string input)
        {
            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Equals("in", StringComparison.OrdinalIgnoreCase) && i + 1 < words.Length)
                {
                    if (int.TryParse(words[i + 1], out int days))
                        return days;
                }
                if (int.TryParse(words[i], out int d) && i + 1 < words.Length && words[i + 1].Contains("day"))
                    return d;
            }
            return -1;
        }

        private void HandlePendingInput(string input)
        {
            string lower = input.ToLower();
            if (pendingAction == "addtask")
            {
                if (lower.Contains("yes") || lower.Contains("sure") || lower.Contains("ok"))
                {
                    BotSay("How many days from today would you like the reminder? (e.g., '3 days')");
                    pendingAction = "setreminder";
                }
                else if (lower.Contains("no") || lower.Contains("nope") || lower.Contains("nah"))
                {
                    BotSay("Okay, no reminder set.");
                    pendingAction = "";
                    pendingTask = null;
                    ShowMainMenu();
                }
                else
                {
                    BotWarn("Please answer yes or no.");
                }
            }
            else if (pendingAction == "setreminder")
            {
                int days = ExtractDays(input);
                if (days <= 0)
                {
                    BotWarn("Please specify number of days, e.g., '3 days'.");
                    return;
                }
                if (pendingTask != null)
                {
                    bool success = _taskManager.SetReminder(pendingTask.Id, DateTime.Today.AddDays(days));
                    if (success)
                    {
                        CyberLogger.Add($"Reminder set for task {pendingTask.Id} in {days} days.");
                        BotSay($"Reminder set for '{pendingTask.Title}' in {days} days.");
                    }
                    else
                        BotWarn("Failed to set reminder. Task may have been deleted.");
                }
                pendingAction = "";
                pendingTask = null;
                ShowMainMenu();
            }
        }

        // ──────────────────────────────────────────────────────────────
        // ── QUIZ METHODS ──────────────────────────────────────────────
        // ──────────────────────────────────────────────────────────────

        public void StartQuiz()
        {
            if (_quiz.IsActive)
            {
                BotWarn("Quiz already in progress. Finish it first.");
                return;
            }
            if (_quiz.TotalQuestions == 0)
            {
                BotWarn("Quiz questions not loaded.");
                return;
            }
            _quiz.Start();
            BotHeader("QUIZ STARTED");
            DisplayQuizQuestion();
        }

        private void DisplayQuizQuestion()
        {
            var q = _quiz.GetCurrentQuestion();
            if (q == null)
            {
                EndQuiz();
                return;
            }
            BotSay($"Question {_quiz.CurrentScore + 1} of {_quiz.TotalQuestions}");
            BotSay(q.Question);
            if (q.IsTrueFalse)
            {
                BotInfo("1. True");
                BotInfo("2. False");
            }
            else
            {
                for (int i = 0; i < q.Options.Count; i++)
                    BotInfo($"{i + 1}. {q.Options[i]}");
            }
            BotSay("Type your answer (number or text).");
        }

        public void ProcessQuizAnswer(string input)
        {
            var q = _quiz.GetCurrentQuestion();
            if (q == null) { EndQuiz(); return; }

            int selected = -1;
            if (int.TryParse(input, out int num) && num >= 1 && num <= (q.IsTrueFalse ? 2 : q.Options.Count))
                selected = num - 1;
            else
            {
                string lower = input.ToLower();
                if (q.IsTrueFalse)
                {
                    if (lower.Contains("true") || lower == "1") selected = 0;
                    else if (lower.Contains("false") || lower == "2") selected = 1;
                }
                else
                {
                    for (int i = 0; i < q.Options.Count; i++)
                        if (q.Options[i].ToLower().Contains(lower) || lower.Contains(q.Options[i].ToLower()))
                        { selected = i; break; }
                }
            }

            if (selected == -1)
            {
                BotWarn("Invalid answer. Please enter the number or text of your choice.");
                return;
            }

            bool correct = _quiz.SubmitAnswer(new List<int> { selected });
            BotSay(correct ? "✅ Correct!" : $"❌ Wrong. The correct answer was: {q.Options[q.CorrectIndices[0]]}");
            BotInfo("Explanation: " + q.Explanation);

            var next = _quiz.GetCurrentQuestion();
            if (next != null)
                DisplayQuizQuestion();
            else
                EndQuiz();
        }

        private void EndQuiz()
        {
            if (_quiz.IsActive) return;
            BotHeader("QUIZ COMPLETE");
            BotSay(_quiz.GetResultMessage());
            ShowMainMenu();
        }

        // ──────────────────────────────────────────────────────────────
        // ── ACTIVITY LOG ──────────────────────────────────────────────
        // ──────────────────────────────────────────────────────────────

        private void ShowActivityLog()
        {
            BotHeader("Activity Log");
            var logEntries = CyberLogger.Log;
            if (logEntries.Count == 0)
                BotInfo("No actions logged yet.");
            else
            {
                int take = Math.Min(10, logEntries.Count);
                var recent = logEntries.Skip(logEntries.Count - take).ToList();
                for (int i = 0; i < recent.Count; i++)
                    BotInfo($"{i + 1}. {recent[i]}");
            }
            ShowMainMenu();
        }
    }
}