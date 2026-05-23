using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Linq;


namespace CyberGuard
{
    //used for random tip selection throughtout CyberSpace
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
            //this code will validate the user input for name
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
            BotSay("Or just type freely. I recognise cybersecurity keywords");
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
                BotSay(tips.SentimentResponse(sentiment));
                BotInfo(keyword);
                ShowMainMenu();
                return;
            }

            //this will return how the chatbot is doing and also validate the user input for the how are you question
            if (input.Contains("how are you") || input == "1" || input == "one")
            {
                BotSay("I am doing okay I guess. Thanks for asking, " + user.username + ".");
                //shows fav topic if the user has viewed any topics yet
                if (!string.IsNullOrEmpty(user.favTopic))
                {
                    BotSay("You have been most interested in " + user.favTopic + ". Would you like to know more about it?");
                }
                ShowMainMenu();
            }
            //this will return the purpose of the chatbot and also validate the user input for the purpose question
            else if (input.Contains("purpose") || input == "2" || input == "two")
            {
                BotSay("My purpose is to educate individuals and organizations to reognize, prevent, and respond to cyber threats, thereby reducing the risk of security breaches and protecting sensitive data.");
                ShowMainMenu();
            }
            //This will return what the user can ask about and will also validate the user input for the topics
            else if (input.Contains("about") || input == "3" || input == "three")
            {
                ShowTopicMenu();
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
            if (user.lastTopic == "Phishing") getTip = tips.GetPhishingTip;
            else if (user.lastTopic == "Password Safety") getTip = tips.GetPasswordTip;
            else if (user.lastTopic == "Safe Browsing") getTip = tips.GetSafeBrowsingTip;
            else { BotInfo("No extra tips on that topic yet."); return; }

            BotInfo(getTip());
        }

        public void ShowTopicMenu()
        {
            user.Section = "topicMenu";
            BotLine();
            BotSay("You can ask me about the following topics:");
            BotInfo("1. Password Safety");
            BotInfo("2. Phishing");
            BotInfo("3. Safe Browsing");
            BotInfo("4. Exit to Main Menu");
        }

        public void HandleTopicMenu(string input)
        {
            //this will return information about password safety
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
            //check for sentiment and respond accordingly
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
                BotInfo("2. Password Manager             - Autofills only on legitimate sites.");
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
                BotWarn("I didn't catch that. Choose from 1 to 7.");
            }

            ShowSafeBrowsingMenu();
        }

        public void ShowPhishingMenu()
        {
            user.Section = "phishing";
            BotHeader("PHISHING");
            TipProvider getTip = tips.GetPhishingTip;
            BotSay("Quick tip: " + getTip());
            BotLine();
            BotSay("What would you like to know about Phishing?");
            BotInfo("1.  Definition");
            BotInfo("2.  Common Types");
            BotInfo("3.  Risks");
            BotInfo("4.  How to Spot It");
            BotInfo("5.  How to Stay Safe");
            BotInfo("6.  Give me a random tip");
            BotInfo("7.  Go Back");
        }

        public void HandlePhishing(string input)
        {
            //check for sentiment and respond accordingly
            string sentiment = tips.Sentiment(input);
            if (sentiment != null)
            {
                BotSay(tips.SentimentResponse(sentiment));
            }

            if (input.Contains("definition") || input.Contains("what is") || input == "1")
            {
                BotHeader("DEFINITION");
                BotInfo("Phishing is a type of cyberattack where attackers impersonate trusted sources");
                BotInfo("to trick people into revealing sensitive information or installing malware.");
            }
            //these are the common types of phishing

            else if (input.Contains("common types") || input.Contains("types") || input == "2")
            {
                BotHeader("COMMON TYPES OF PHISHING");
                BotInfo("1.Email Phishing: Fraudulent emails that appear to be from reputable sources.");
                BotInfo("2.Spear Phishing: Targeted attacks aimed at specific individuals or organizations.");
                BotInfo("3.Smishing: Phishing attempts via SMS text messages.");
                BotInfo("4.Vishing: Voice phishing over phone calls.");
                BotInfo("5.Clone Phishing: Creating a nearly identical copy of a legitimate email with malicious links.");

            }
            //these are the risks of phishing
            else if (input.Contains("risks") || input.Contains("dangers") || input == "3")
            {
                BotHeader("RISKS OF PHISHING");
                BotInfo("1.Identity Theft: Stealing personal information to commit fraud.");
                BotInfo("2.Financial Loss: Gaining access to bank accounts or credit cards.");
                BotInfo("3.Data Breaches: Compromising sensitive company data.");
                BotInfo("4.Malware Infection: Installing harmful software on your device.");
            }
            //this is how to spot phishing attacks
            else if (input.Contains("spot") || input == "4")
            {
                BotHeader("HOW TO SPOT PHISHING");
                BotInfo("1.Check the sender's email address for legitimacy.");
                BotInfo("2. Look for spelling and grammar mistakes in the message.");
                BotInfo("3.Hover over links to see the actual URL before clicking.");
                BotInfo("4.Be cautious of urgent or threatening language.");
            }
            //this is how to stay safe from phishing attacks
            else if (input.Contains("stay safe") || input == "5")
            {
                BotHeader("HOW TO STAY SAFE FROM PHISHING");
                BotInfo("1.Never click on links or download attachments from unknown senders.");
                BotInfo("2.Use anti-phishing software and keep it updated.");
                BotInfo("3.Verify requests for sensitive information through a separate channel.");
                BotInfo("4.Educate yourself and others about common phishing tactics.");
            }
            else if (input.Contains("random") || input.Contains("tip") || input == "6")
            {
                TipProvider getTip = tips.GetPhishingTip;
                BotSay("Here's a random phishing tip: " + getTip());
            }
            //this will go back to main menu
            else if (input.Contains("go back") || input.Contains("back") || input == "7")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                BotWarn("I didn't quite understand that. Could you rephrase?");
            }
            ShowPhishingMenu();
        }

        public void ShowPasswordMenu()
        {
            user.Section = "password";
            BotHeader("Password Safety");
            //immediate tip about password safety
            TipProvider getTip = tips.GetPasswordTip;
            BotSay("Here are some tips to keep your passwords safe: " + getTip());
            BotLine();
            BotSay("What would you like to know about password safety?");
            BotInfo("1. Definition");
            BotInfo("2. Common Risks");
            BotInfo("3. What Makes a Strong Password");
            BotInfo("4. Best Practices");
            BotInfo("5. How Hackers Crack Passwords");
            BotInfo("6. Give me a random tip");
            BotInfo("7. Go back");
        }
        public void HandlePassword(string input)
        {
            //check for sentiment and respond accordingly
            string sentiment = tips.Sentiment(input);
            if (sentiment != null)
            {
                BotSay(tips.SentimentResponse(sentiment));
            }
            //this if the definition
            if (input.Contains("definition") || input.Contains("what is") || input == "1")
            {
                BotHeader("DEFINITION");
                BotSay("Password safety refers to the practices and technologies used to protect");
                BotSay(" passwords from being stolen, guessed, or compromised.");
            }
            //these are the common risks or dangers
            else if (input.Contains("common risks") || input.Contains("dangers") || input == "2")
            {
                BotHeader("COMMON RISKS");
                BotSay("1. Brute Force Attacks: Hackers try every possible combination until they succeed.");
                BotSay("2. Credential Stuffing: Using leaked passwords from one site to access others.");
                BotSay("3. Keyloggers: Malware that records everything you type");
                BotSay("4. Shoulder Surfing: Someone physically watching you type your password.");
                BotSay("5. Data Breaches: Your password being exposed when a company is hacked.");

            }
            //this is how to make a strong password
            else if (input.Contains("strong") && input.Contains("password") || input == "3")
            {
                BotHeader("WHAT MAKES A STRONG PASSWORDS");
                BotSay("1. At least 12 characters long");
                BotSay("2. Mix of uppercase, lowercase, numbers and symbols.");
                BotSay("3. No personal info like your name, birthday or pet's name.");
                BotSay("4. Not a common word or sequence");

            }
            //these are the best practices of password safety
            else if (input.Contains("best") || input.Contains("practices") || input == "4")
            {
                BotHeader("BEST PRACTICES");
                BotSay("1. Use a unique password for every account.");
                BotSay("2. Enable Multi-Factor Authentication wherever possible.");
                BotSay("3. Use a password manager to store them securely.");
                BotSay("4. Change passwords immediately if you suspect a breach.");
                BotSay("5. Never share your password with anyone, even IT support.");
            }
            //these is how hackers crack passwords
            else if (input.Contains("hackers") || input.Contains("crack") || input == "5")
            {
                BotHeader("HOW HACKERS CRACK PASSWORDS");
                BotSay("1. Dictionary attacks using common words and phrases.");
                BotSay("2. Buying leaked credentials from the dark web.");
                BotSay("3. Social engineering - tricking you into revealing it yourself.");
            }
            else if (input.Contains("random") || input.Contains("tip") || input == "6")
            {
                TipProvider getTip = tips.GetPasswordTip;
                BotSay("Here's a random password safety tip: " + getTip());
            }
            //this will go back to the main menu
            else if (input.Contains("go back") || input == "6")
            {
                BotSay("Returning to the topic menu...");
                ShowTopicMenu();
                return;
            }
            else
            {
                BotWarn("I didn't quite understand that. Could you rephrase?");
            }
            ShowPasswordMenu();
        }
        public void ShowGoodbye()
        {
            BotLine();
            Box("Goodbye, " + user.username + "! Stay safe online!");
            BotLine();

            if (user.topicsViewed.Count > 0)
            {
                BotSay("During our session you learned about: " + string.Join(", ", user.topicsViewed));
                if (!string.IsNullOrEmpty(user.favTopic))
                    BotSay("Your most visited topic was: " + user.favTopic + ". Keep learning to stay protected!");
            }

            BotSay("Remember: Stay vigilant, stay informed, stay safe!");
            user.Section = "goodbye";
        }
    }
}