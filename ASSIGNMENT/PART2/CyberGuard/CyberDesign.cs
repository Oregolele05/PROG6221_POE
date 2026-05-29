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

        // Core Theme Colors mapping your CSS properties
        private readonly Color _themeBgColor = Color.FromArgb(30, 30, 30);    // CSS #1E1E1E (Dark Gray)
        private readonly Color _primaryRed = Color.FromArgb(230, 57, 70);    // CSS #E63946 (Cyber Red)
        private readonly Color _secondaryOrange = Color.FromArgb(255, 140, 66); // CSS #FF8C42 (Neon Orange)
        private readonly Color _lightTextColor = Color.FromArgb(237, 237, 237); // CSS #EDEDED (Bright Text)

        public void VoiceGreeting()
        {
            try
            {
                // Finds the bin\Debug or output folder dynamically
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
                // System fallback if file access fails
                SystemSounds.Asterisk.Play();
            }
        }

        // Bot Box - Styled with sleek, rounded arc corners and locked Dark Background
        public void BotSay(string message)
        {
            string rawText = "🤖 Bot: " + message;
            int width = rawText.Length + 2;

            DisplayMessage("╭" + new string('─', width) + "╮", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("│ " + rawText + " │", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("╰" + new string('─', width) + "╯", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
        }

        public void BotWarn(string message)
            => DisplayMessage("⚠   " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        public void BotHeader(string message)
            => DisplayMessage("\n══════ " + message + " ══════", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);

        public void BotInfo(string message)
            => DisplayMessage("     " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // User Box - Styled with sleek, rounded arc corners and locked Dark Background
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

        // Header Logo - Fully isolated inside the dark color scheme properties
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