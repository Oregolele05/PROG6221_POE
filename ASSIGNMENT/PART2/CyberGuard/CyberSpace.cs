using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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
            user.username = input.Trim();
            BotLine();
            Box("Welcome " + user.username + " nice to meet you!!");
            BotLine();
            ShowMainMenu();
        }

        public void ShowMainMenu()
        {
            user.Section = "main";
            BotSay("How can I help you today, " + user.username + "?");
            BotInfo("1. How are you?");
            BotInfo("2. What is your purpose?");
            BotInfo("3. What can I ask you about?");
            BotInfo("4. Exit");
        }

        public void ResponseSystem(string input)
        {
            if (input.Contains("tell me more") || input.Contains("another tip")
               || input.Contains("explain more") || input.Contains("more info"))
            {
                HandleFollowUp();
                ShowMainMenu();
                return;
            }
            string sentiment = tips.Sentiment(input);
            string keyword = tips.CheckKeywords(input);
            if (keyword != null)
            {
                if (sentiment != "neutral")
                {
                    BotSay(tips.SentimentResponse(sentiment));
                }
                BotInfo(keyword);
                ShowMainMenu();
                return;
            }

            if (input.Contains("how are you") || input == "1" || input == "one")
            {
                BotSay("I am doing okay I guess. Thanks for asking, " + user.username + ".");
                if (!string.IsNullOrEmpty(user.favTopic))
                {
                    BotSay("You have been most interested in " + user.favTopic + ". Would you like to know more about it?");
                }
                ShowMainMenu();
            }
            else if (input.Contains("purpose") || input == "2" || input == "two")
            {
                BotSay("My purpose is to educate individuals and organizations to reognize, prevent, and respond to cyber threats, thereby reducing the risk of security breaches and protecting sensitive data.");
                ShowMainMenu();
            }
            else if (input.Contains("about") || input == "3" || input == "three")
            {
                ShowTopicMenu();
            }
            else if (input.Contains("exit") || input == "4" || input == "four")
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
                BotWarn("We haven't discussed a topic yet. Ask me about phishing, passwords, or safe browsing.");
                return;
            }

            BotSay("Here's another tip on " + user.lastTopic + ":");

            TipProvider getTip;
            if (user.lastTopic == "Phishing")
            {
                getTip = tips.GetPhishingTip;
            }
            else if (user.lastTopic == "Password Safety")
            {
                getTip = tips.GetPasswordTip;
            }
            else if (user.lastTopic == "Safe Browsing")
            {
                getTip = tips.GetSafeBrowsingTip;
            }
            else
            {
                BotInfo("No extra tips on that topic yet.");
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
            if (input.Contains("password") || input == "1" || input == "one")
            {
                user.TrackTopic("Password Safety");
                ShowPasswordMenu();
            }
            else if (input.Contains("phishing") || input == "2" || input == "two")
            {
                user.TrackTopic("Phishing");
                ShowPhishingMenu();
            }
            else if (input.Contains("safe browsing") || input.Contains("browsing") || input == "3" || input == "three")
            {
                user.TrackTopic("Safe Browsing");
                ShowSafeBrowsingMenu();
            }
            else if (input.Contains("back") || input == "4")
            {
                ShowMainMenu();
            }
            else
            {
                BotWarn("I didn't quite understand that. Could you rephrase?");
            }
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
            string sentiment = tips.Sentiment(input);
            if (sentiment != null)
            {
                BotSay(tips.SentimentResponse(sentiment));
            }
            if (input.Contains("definition") || input == "1")
            {
                BotHeader("DEFINITION");
                BotInfo("Safe browsing is the practice of navigating the internet securely to protect");
                BotInfo("your devices, personal information, and identity from cyber threats.");
            }
            else if (input.Contains("common risks") || input == "2")
            {
                BotHeader("COMMON RISKS ONLINE");
                BotInfo("1. Malicious websites that download malware automatically.");
                BotInfo("2. Fake shopping sites designed to steal payment details.");
                BotInfo("3. Unsecured public Wi-Fi allowing hackers to intercept your data.");
                BotInfo("4. Browser extensions that spy on your activity.");
                BotInfo("5. Pop-up scams pretending to be virus warnings.");
            }
            else if (input.Contains("how to") || input.Contains("safely") || input == "3")
            {
                BotHeader("HOW TO BROWSE SAFELY");
                BotInfo("1. Always check for HTTPS and a padlock icon in the address bar.");
                BotInfo("2. Avoid clicking pop-up ads or suspicious links.");
                BotInfo("3. Use a reputable browser like Chrome, Firefox, or Edge.");
                BotInfo("4. Keep your browser and plugins updated.");
                BotInfo("5. Use a VPN on public Wi-Fi networks.");
            }
            else if (input.Contains("tools") || input.Contains("help") || input == "4")
            {
                BotHeader("TOOLS THAT HELP");
                BotInfo("1. VPN (Virtual Private Network) - Encrypts your internet connection.");
                BotInfo("2. Password Manager              - Autofills only on legitimate sites.");
                BotInfo("3. Antivirus Software           - Blocks known malicious sites.");
                BotInfo("4. Browser Safe Browsing Mode   - Warns you before visiting dangerous sites.");
            }
            else if (input.Contains("good") || input.Contains("habits") || input == "5")
            {
                BotHeader("GOOD HABITS");
                BotInfo("1. Log out of accounts when done, especially on shared devices.");
                BotInfo("2. Clear cookies and cache regularly.");
                BotInfo("3. Never save passwords in your browser on a shared computer.");
            }
            else if (input.Contains("random") || input.Contains("tip") || input == "6")
            {
                TipProvider getTip = tips.GetSafeBrowsingTip;
                BotSay("Random Browsing Tip: " + getTip());
            }
            else if (input.Contains("go back") || input.Contains("back") || input == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to change sub-topics.");
        }

        // ==========================================
        // ADDED MISSING PASSWORD MENU SYSTEM
        // ==========================================
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
            string sentiment = tips.Sentiment(input);
            if (sentiment != "neutral")
            {
                BotSay(tips.SentimentResponse(sentiment));
            }

            if (input.Contains("importance") || input == "1")
            {
                BotHeader("IMPORTANCE");
                BotInfo("Passwords are your first line of defense against cyber threats.");
                BotInfo("Weak credentials let automatic scripts crack your profile instantly.");
            }
            else if (input.Contains("create") || input == "2")
            {
                BotHeader("CREATION STRATEGY");
                BotInfo("1. Use long passphrases with three or more random connected words.");
                BotInfo("2. Implement distinct symbols, capitalization rules, and integers.");
            }
            else if (input.Contains("2fa") || input == "3")
            {
                BotHeader("TWO-FACTOR AUTHENTICATION");
                BotInfo("2FA forces checking sequences onto mobile links before unlocking logins.");
            }
            else if (input.Contains("manager") || input == "4")
            {
                BotHeader("PASSWORD MANAGERS");
                BotInfo("Managers encrypt individual application login profiles safely within structural vaults.");
            }
            else if (input.Contains("mistakes") || input == "5")
            {
                BotHeader("COMMON MISTAKES");
                BotInfo("1. Reusing matching verification strings across personal networks.");
                BotInfo("2. Leaving credential parameters written down on unprotected surfaces.");
            }
            else if (input.Contains("random") || input == "6")
            {
                TipProvider getTip = tips.GetPasswordTip;
                BotSay("Random Password Tip: " + getTip());
            }
            else if (input.Contains("go back") || input == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to return.");
        }

        // ==========================================
        // ADDED MISSING PHISHING MENU SYSTEM
        // ==========================================
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
            string sentiment = tips.Sentiment(input);
            if (sentiment != "neutral")
            {
                BotSay(tips.SentimentResponse(sentiment));
            }

            if (input.Contains("what is") || input == "1")
            {
                BotHeader("PHISHING DEFINITION");
                BotInfo("Phishing involves mimicking trusted companies to harvest identities.");
            }
            else if (input.Contains("identify") || input == "2")
            {
                BotHeader("IDENTIFICATION METRICS");
                BotInfo("1. Look out for forced artificial urgency indicators.");
                BotInfo("2. Check for mismatched email domains or bad grammar rules.");
            }
            else if (input.Contains("types") || input == "3")
            {
                BotHeader("ATTACK TYPES");
                BotInfo("1. Spear phishing: Targets chosen profiles explicitly.");
                BotInfo("2. Smishing: Malicious content distributed via SMS text messages.");
            }
            else if (input.Contains("suspect") || input == "4")
            {
                BotHeader("SUSPICION STRATEGY");
                BotInfo("Avoid opening links or downloads. Independently reach out to entities via verified help lines.");
            }
            else if (input.Contains("report") || input == "5")
            {
                BotHeader("REPORTING CHANNELS");
                BotInfo("Forward suspicious activity patterns directly to your organization's IT service desk.");
            }
            else if (input.Contains("random") || input == "6")
            {
                TipProvider getTip = tips.GetPhishingTip;
                BotSay("Random Phishing Tip: " + getTip());
            }
            else if (input.Contains("go back") || input == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                TipProvider confused = tips.GetRandomCofusedResponse;
                BotWarn(confused());
            }

            BotLine();
            BotSay("Select another option (1-7) or type 'back' to return.");
        }

        // ==========================================
        // ADDED MISSING GOODBYE ENGINE STATE CLOSURE
        // ==========================================
        public void ShowGoodbye()
        {
            user.Section = "goodbye";
            BotLine();
            Box("SESSION TERMINATED BY USER");
            BotSay("Thank you for using CyberGuard, " + user.username + "!");
            if (!string.IsNullOrEmpty(user.favTopic))
            {
                BotSay("You spent the most time looking into: " + user.favTopic.ToUpper());
                BotSay("Remember to follow those guidelines out on the web!");
            }
            BotSay("System turning off. Keep your firewall active and stay alert online.");
            BotLine();
        }
    }
}