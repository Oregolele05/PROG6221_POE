using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberGuard
{
    public class CyberDesign
    {
        protected RichTextBox ChatDisplay { get; set; }

        // Core Theme Colors
        private readonly Color _themeBgColor = Color.FromArgb(30, 30, 30);    // CSS #1E1E1E (Dark Gray)
        private readonly Color _primaryRed = Color.FromArgb(230, 57, 70);    // CSS #E63946 (Cyber Red)
        private readonly Color _secondaryOrange = Color.FromArgb(255, 140, 66); // CSS #FF8C42 (Neon Orange)
        private readonly Color _lightTextColor = Color.FromArgb(237, 237, 237); // CSS #EDEDED (Bright Text)

        public void VoiceGreeting()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string soundPath = Path.Combine(baseDir, "greet.wav");

                if (File.Exists(soundPath))
                {
                    using (SoundPlayer player = new SoundPlayer(soundPath))
                    {
                        player.Play();
                    }
                }
            }
            catch (Exception ex)
            {
                
                SystemSounds.Asterisk.Play();
            }
        }

        
        //will return the bots response in a styled box format with the bot icon and red color scheme, aligned to the left
        public void BotSay(string message)
        {
            string rawText = "🤖 Bot: " + message;
            int width = rawText.Length + 2;

            DisplayMessage("╭" + new string('─', width) + "╮", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("│ " + rawText + " │", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("╰" + new string('─', width) + "╯", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
        }

        //will warn for a invalid input or unrecognized command in a red color scheme with a warning icon, aligned to the left
        public void BotWarn(string message)
            => DisplayMessage("⚠   " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // will display a header message in a bright orange color scheme with a decorative line, aligned to the left
        public void BotHeader(string message)
            => DisplayMessage("\n══════ " + message + " ══════", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);

        // will display informational messages in a lighter red color scheme with an info icon, aligned to the left
        public void BotInfo(string message)
            => DisplayMessage("     " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // will return the user's message in a styled box format with the user icon and orange color scheme, aligned to the right
        public void UserSay(string message)
        {
            string rawText = "You: " + message + " 👤";
            int width = rawText.Length + 2;

            DisplayMessage("╭" + new string('─', width) + "╮", _secondaryOrange, _themeBgColor, HorizontalAlignment.Right);
            DisplayMessage("│ " + rawText + " │", _secondaryOrange, _themeBgColor, HorizontalAlignment.Right);
            DisplayMessage("╰" + new string('─', width) + "╯", _secondaryOrange, _themeBgColor, HorizontalAlignment.Right);
        }

        // Divider Line - Explicitly painted over the dark background to prevent any white streaks
        public void BotLine()
            => DisplayMessage("────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────", _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // will display the chatbot's logo in a stylized ASCII art format with the primary red and secondary orange color scheme, aligned to the center
        public void LogoDisplay()
        {
            if (ChatDisplay == null) return;

            DisplayMessage("", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("                    ╔═════════════════════════════════════════════════════════════════════════════════╗", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              ____ _  _ ___  ____ ____ ____ _  _ ____ ____ ___                   ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              |    |__| |__] |___ |__/ | __ |  | |__| |__/ |  \\                  ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              |___  ||  |__] |___ |  \\ |__] |__| |  | |  \\ |__/                  ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║                                                                                 ║", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║                       Cyber Awareness & Education Chatbot                       ║", _lightTextColor, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ╚═════════════════════════════════════════════════════════════════════════════════╝", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
        }

        // wraps welcome message
        public void Box(string text)
        {
            int width = text.Length + 2;
            DisplayMessage("╭" + new string('─', width) + "╮", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("│ " + text + " │", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("╰" + new string('─', width) + "╯", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
        }

        // Standard Message Painter Handler Configuration - Renders text instantly
        public void DisplayMessage(string message, Color textColour, Color textBgColour, HorizontalAlignment alignment)
        {
            if (ChatDisplay == null) return;

            ChatDisplay.SelectionAlignment = alignment;
            ChatDisplay.SelectionColor = textColour;
            ChatDisplay.SelectionBackColor = textBgColour; // Explicitly sets the behind-text color to eliminate white highlights
            ChatDisplay.AppendText(message + "\n");
            ChatDisplay.ScrollToCaret();
        }
    }
}