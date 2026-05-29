using System;
using System.Collections.Generic;

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
        { "wifi",      "Avoid using public Wi-Fi for sensitive transactions like online banking unless you are using a trusted VPN." },
        { "malware",   "Malware stands for malicious software. Install reputable anti-virus programs and keep them updated to defend your system." }
    };

    protected List<string> phishingTips = new List<string>()
    {
        "Look out for spelling mistakes and generic greetings like 'Dear Customer' in emails.",
        "Check the email address of the sender carefully. Scammers often use addresses that look similar to official ones.",
        "Never share sensitive information like passwords or PINs via email or text messages."
    };

    protected List<string> passwordTips = new List<string>()
    {
        "A strong password should be at least 12 characters long and include a mix of letters, numbers, and symbols.",
        "Consider using a password manager to securely store and generate complex passwords.",
        "Change your passwords immediately if you suspect an account has been compromised."
    };

    protected List<string> safeBrowsingTips = new List<string>()
    {
        "Be cautious when downloading files from unfamiliar websites. They could contain malware.",
        "Don't click on pop-up advertisements. Close them by clicking the 'X' on the window, not inside the ad.",
        "Clear your browser history and cookies regularly to help protect your online privacy."
    };

    protected List<string> confusedResponses = new List<string>()
    {
        "I'm not quite sure I follow. Could you try rephrasing that or choosing an option from the menu?",
        "Hmm, I didn't recognize any keywords there. Try asking about passwords, phishing, or safe browsing!",
        "I'm still learning! Could you specify your query using simpler terms or terms related to cyber safety?"
    };

    public string GetPhishingTip() => phishingTips[random.Next(phishingTips.Count)];
    public string GetPasswordTip() => passwordTips[random.Next(passwordTips.Count)];
    public string GetSafeBrowsingTip() => safeBrowsingTips[random.Next(safeBrowsingTips.Count)];
    public string GetRandomCofusedResponse() => confusedResponses[random.Next(confusedResponses.Count)];

    public string CheckKeywords(string input)
    {
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                return keywordResponses[keyword];
            }
        }
        return null;
    }

    public string Sentiment(string input)
    {
        input = input.ToLower();

        if (input.Contains("worried") || input.Contains("scared") ||
            input.Contains("afraid") || input.Contains("hacked") ||
            input.Contains("compromised") || input.Contains("stolen"))
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