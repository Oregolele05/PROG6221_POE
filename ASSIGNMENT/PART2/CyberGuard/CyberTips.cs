using System;
using System.Collections.Generic;

namespace CyberGuard
{
    public class CyberTips
    {
        protected Random random = new Random();

        protected Dictionary<string, string> keywordResponses = new Dictionary<string, string>()
        {
            // Password Safety keywords
            { "password",  "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords." },
            { "2fa",       "Two-Factor Authentication adds a second layer of security. Enable it on all your important accounts." },
            { "backup",    "Regular backups protect you from data loss. Always store a copy somewhere safe and offline." },

            // Phishing keywords
            { "phishing",  "Phishing emails often create urgency. Always verify the sender before clicking any links." },
            { "scam",      "Scammers often disguise themselves as trusted organisations. Never send money or share personal info with unverified contacts." },
            { "spam",      "Never click links or open attachments in spam emails. Mark them and delete immediately." },

            // Safe Browsing keywords
            { "browser",   "Keep your browser updated and avoid clicking suspicious links or pop-up ads." },
            { "https",     "Always check for HTTPS and a padlock icon in the address bar before entering personal info." },
            { "vpn",       "A VPN encrypts your internet connection and hides your IP address. Use one on public Wi-Fi." },
            { "wifi",      "Avoid using public Wi-Fi for sensitive tasks. Use a VPN to encrypt your connection." },
            { "cookie",    "Clear your cookies and cache regularly to protect your browsing privacy." },
            { "malware",   "Malware can enter your device through downloads and email attachments. Keep your antivirus updated." },
        };


        protected List<string> phishingTips = new List<string>()
        {
            "Be cautious of emails asking for personal information. Scammers disguise themselves as trusted organisations.",
            "Always hover over links before clicking — the real URL often reveals a fake site.",
            "Legitimate companies will never ask for your password via email.",
            "Check for spelling mistakes in email addresses — 'paypa1.com' is not PayPal.",
            "If an email creates urgency like 'Act now or your account closes', treat it as suspicious."
        };

        protected List<string> passwordTips = new List<string>()
        {
            "Use a passphrase like 'Coffee@Sunrise!2024' — it is stronger than random strings.",
            "Never reuse passwords across multiple sites. A breach on one exposes all your accounts.",
            "Enable two-factor authentication wherever possible.",
            "A password manager like Bitwarden generates and stores strong passwords for you.",
            "Change passwords immediately if a service you use gets breached."
        };

        protected List<string> safeBrowsingTips = new List<string>()
        {
            "Always check for HTTPS and a padlock icon before entering any personal info.",
            "Avoid clicking pop-up ads — they are a common way to install malware.",
            "Use a reputable browser like Chrome, Firefox, or Edge and keep it updated.",
            "Install an ad blocker to reduce your exposure to malicious advertisements.",
            "Log out of accounts when done, especially on shared or public computers."
        };

        protected List<string> confusedResponses = new List<string>()
        {
            "I'm not sure I understand. Can you try rephrasing?",
            "Hmm, I didn't quite catch that. Could you ask differently?",
            "I don't have info on that yet. Try asking about phishing, passwords, or safe browsing.",
            "That's outside what I know right now. Ask me about a cybersecurity topic!"
        };

        //the following methods get random answers
        public string GetPhishingTip()
        {
            return phishingTips[random.Next(phishingTips.Count)];
        }

        public string GetPasswordTip()
        {
            return passwordTips[random.Next(passwordTips.Count)];
        }

        public string GetSafeBrowsingTip()
        {
            return safeBrowsingTips[random.Next(safeBrowsingTips.Count)];
        }

        public string GetRandomCofusedResponse()
        {
            return confusedResponses[random.Next(confusedResponses.Count)];
        }

        public string CheckKeywords(string input)
        {
            foreach (var keyword in keywordResponses)
            {
                if (input.Contains(keyword.Key))
                {
                    return keyword.Value;
                }

            }
            return null;
        }
        public string Sentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("anxious") || input.Contains("afraid") ||
                input.Contains("nervous") || input.Contains("unsafe"))
                return "worried";

            if (input.Contains("confused") || input.Contains("lost") ||
                input.Contains("unsure") || input.Contains("help me") ||
                input.Contains("don't understand"))
                return "confused";

            if (input.Contains("frustated") || input.Contains("annoyed") ||
                input.Contains("angry") || input.Contains("sick of"))
                return "frustated";

            if (input.Contains("curious") || input.Contains("interested") ||
                input.Contains("want to know") || input.Contains("explain") ||
                input.Contains("tell me"))
                return "curious";

            return "neutral";
        }

        public string SentimentResponse(string sentiment)
        {
            switch (sentiment)
            {
                case "worried":
                    return "I understand that you might be feeling worried. Remember, I'm here to help you with any questions or concerns you have about online safety.";
                case "confused":
                    return confusedResponses[random.Next(confusedResponses.Count)];
                case "frustated":
                    return "I can see that you're feeling frustrated. Let's work together to find the information you need and make things easier for you.";
                case "curious":
                    return "It's great to see your curiosity! Feel free to ask me anything about online safety, and I'll do my best to provide you with helpful information.";
                default:
                    return "Here's what I found for you:";
            }
        }
    }
}
