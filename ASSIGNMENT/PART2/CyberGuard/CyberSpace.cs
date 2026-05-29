using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace CyberGuard
{
    public delegate string TipProvider();

    public class CyberSpace : CyberDesign
    {
        private CyberUser user = new CyberUser();
        private CyberTips tips = new CyberTips();
        public string CurrentSection => user.Section;

        public void Initialise(RichTextBox chatDisplay)
        {
            ChatDisplay = chatDisplay;
        }

        public void WelcomeScreen()
        {
            LogoDisplay();
            BotLine();
            BotSay("Welcome to CyberGuard - your Cyber Awareness Chatbot!");
            BotSay("Before we begin, what is your name?");
            user.Section = "getname";
        }

        public void UserInteraction(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                BotWarn("\nPlease enter a valid name.");
                return;
            }
            if (input.Any(char.IsDigit))
            {
                BotWarn("\nA name cannot contain numeric values.");
                return;
            }

            // Explicit verification filter blocking unauthorized special symbol arrays
            string invalidChars = "!@#$%^&*()_=-<>.?/\\|+][{}\":;'~`_";
            if (input.Any(ch => invalidChars.Contains(ch)))
            {
                BotWarn("\nA name cannot contain special characters (like !, @, #, etc.).");
                return;
            }

            user.username = input.Trim();
            BotLine();
            Box("Welcome " + user.username + " nice to meet you!!");
            BotLine();
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            // Safely dump current time delta caches if coming back from active menus
            if (user.Section == "password" || user.Section == "phishing" || user.Section == "safebrowsing")
            {
                user.TrackTopic("");
            }

            user.Section = "main";
            BotSay("How can I help you today, " + user.username + "?");
            BotInfo("1. How are you?");
            BotInfo("2. What is your purpose?");
            BotInfo("3. What can I ask you about?");
            BotInfo("4. Exit");
        }

        public void ResponseSystem(string input)
        {
            string cleanInput = input.ToLower().Trim();

            // Root interceptor listener tracking global help commands
            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip")
               || cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                ShowMainMenu();
                return;
            }

            // Root interceptor tracking dedicated analytics report requests
            if (cleanInput.Contains("favourite topic") || cleanInput.Contains("favorite topic") || cleanInput.Contains("most interested"))
            {
                if (!string.IsNullOrEmpty(user.favTopic))
                {
                    BotSay("You have been most interested in " + user.favTopic + ". Would you like to know more about it?");
                }
                else
                {
                    BotSay("We haven't spent enough time on an individual topic yet for me to determine your favorite!");
                }
                ShowMainMenu();
                return;
            }

            string sentiment = tips.Sentiment(input);
            string keyword = tips.CheckKeywords(input);
            if (keyword != null)
            {
                user.QuestionCount++;
                if (sentiment != "neutral")
                {
                    BotSay(tips.SentimentResponse(sentiment));
                }
                BotInfo(keyword);
                ShowMainMenu();
                return;
            }

            if (cleanInput.Contains("how are you") || cleanInput == "1" || cleanInput == "one")
            {
                BotSay("I am doing okay I guess. Thanks for asking, " + user.username + ".");
                if (!string.IsNullOrEmpty(user.favTopic))
                {
                    BotSay("You have been most interested in " + user.favTopic + ". Would you like to know more about it?");
                }
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
            else
            {
                BotInfo("No extra tips available on that topic yet.");
                return;
            }

            BotInfo(getTip());
        }

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
            {
                user.TrackTopic("Password Safety");
                ShowPasswordMenu();
            }
            else if (cleanInput.Contains("phishing") || cleanInput == "2" || cleanInput == "two")
            {
                user.TrackTopic("Phishing");
                ShowPhishingMenu();
            }
            else if (cleanInput.Contains("browsing") || cleanInput == "3" || cleanInput == "three")
            {
                user.TrackTopic("Safe Browsing");
                ShowSafeBrowsingMenu();
            }
            else if (cleanInput.Contains("back") || cleanInput == "4" || cleanInput == "four")
            {
                ShowMainMenu();
            }
            else
            {
                BotWarn("I didn't quite understand that selection. Could you try choosing options 1 through 4?");
            }
        }

        // ==================== SUB MENU TRACKS ====================

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

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip")
                || cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" || cleanInput == "5" || cleanInput == "6" ||
                cleanInput.Contains("importance") || cleanInput.Contains("create") || cleanInput.Contains("2fa") || cleanInput.Contains("manager") || cleanInput.Contains("mistakes") || cleanInput.Contains("random"))
            {
                user.QuestionCount++;
            }

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

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip")
                || cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" || cleanInput == "5" || cleanInput == "6" ||
                cleanInput.Contains("what is") || cleanInput.Contains("identify") || cleanInput.Contains("types") || cleanInput.Contains("suspect") || cleanInput.Contains("report") || cleanInput.Contains("random"))
            {
                user.QuestionCount++;
            }

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

            if (cleanInput.Contains("tell me more") || cleanInput.Contains("another tip")
                || cleanInput.Contains("explain more") || cleanInput.Contains("more info"))
            {
                user.QuestionCount++;
                HandleFollowUp();
                BotLine();
                BotSay("Select another option (1-7) or type 'back' to return.");
                return;
            }

            if (cleanInput == "1" || cleanInput == "2" || cleanInput == "3" || cleanInput == "4" || cleanInput == "5" || cleanInput == "6" ||
                cleanInput.Contains("definition") || cleanInput.Contains("risks") || cleanInput.Contains("how to") || cleanInput.Contains("tools") || cleanInput.Contains("habits") || cleanInput.Contains("random"))
            {
                user.QuestionCount++;
            }

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

        public void ShowGoodbye()
        {
            user.TrackTopic(""); // Final log capturing last remaining time balances
            user.Section = "goodbye";

            BotLine();
            Box("SESSION TERMINATED BY USER");
            BotSay("Thank you for using CyberGuard, " + user.username + "!");

            BotHeader("YOUR ENGAGEMENT PROFILE STATS");
            BotInfo($"Total Educational Questions Asked: {user.QuestionCount}");
            if (!string.IsNullOrEmpty(user.favTopic))
            {
                BotInfo($"Your Most Discussed Topic: {user.favTopic}");
            }
            BotLine();

            BotInfo("Time Spent Breakdown per Topic:");
            foreach (var record in user.TopicDurations)
            {
                if (!string.IsNullOrEmpty(record.Key))
                {
                    BotInfo($"• {record.Key}: {record.Value.Seconds} seconds");
                }
            }
            BotLine();
        }
    }
}