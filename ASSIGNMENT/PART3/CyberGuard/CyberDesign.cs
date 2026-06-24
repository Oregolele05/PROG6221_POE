using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberGuard
{
    // ══════════════════════════════════════════════════════════════════════
    // CyberDesign — base class handling all visual display and styling
    // Updated for WPF — uses WpfChatDisplay instead of WinForms RichTextBox
    // CyberSpace inherits from this class
    // ══════════════════════════════════════════════════════════════════════
    public class CyberDesign
    {
        // WPF chat display wrapper — set by CyberSpace.Initialise()
        protected CyberChatDisplay ChatDisplay { get; set; }

        // ── Core Theme Colors (matching original WinForms design) ─────────
        private readonly Color _themeBgColor = Color.FromArgb(30, 30, 30);     // #1E1E1E Dark Gray
        private readonly Color _primaryRed = Color.FromArgb(230, 57, 70);    // #E63946 Cyber Red
        private readonly Color _secondaryOrange = Color.FromArgb(255, 140, 66);   // #FF8C42 Neon Orange
        private readonly Color _lightTextColor = Color.FromArgb(237, 237, 237);  // #EDEDED Bright Text

        // ── Voice Greeting ────────────────────────────────────────────────
        // Plays greet.wav from the same folder as the .exe
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
            catch
            {
                SystemSounds.Asterisk.Play();
            }
        }

        // ── Core Display Method ───────────────────────────────────────────
        // All output goes through here — appends coloured text to chat display
        public void DisplayMessage(string message, Color textColour, Color textBgColour, HorizontalAlignment alignment)
        {
            if (ChatDisplay == null) return;
            ChatDisplay.AppendText(message, textColour, textBgColour, alignment);
        }

        // ── BotSay ────────────────────────────────────────────────────────
        // Bot response in a styled box — red, aligned left
        public void BotSay(string message)
        {
            string rawText = "🤖 Bot: " + message;
            int width = rawText.Length + 2;
            DisplayMessage("╭" + new string('─', width) + "╮", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("│ " + rawText + " │", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("╰" + new string('─', width) + "╯", _primaryRed, _themeBgColor, HorizontalAlignment.Left);
        }

        // ── BotWarn ───────────────────────────────────────────────────────
        // Warning message — red, left aligned
        public void BotWarn(string message)
            => DisplayMessage("⚠   " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // ── BotHeader ─────────────────────────────────────────────────────
        // Section header — orange, left aligned
        public void BotHeader(string message)
            => DisplayMessage("\n══════ " + message + " ══════", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);

        // ── BotInfo ───────────────────────────────────────────────────────
        // Info bullet — red, left aligned
        public void BotInfo(string message)
            => DisplayMessage("     " + message, _primaryRed, _themeBgColor, HorizontalAlignment.Left);

        // ── UserSay ───────────────────────────────────────────────────────
        // User message in styled box — orange, right aligned
        public void UserSay(string message)
        {
            string rawText = "You: " + message + " 👤";
            int width = rawText.Length + 2;
            DisplayMessage("╭" + new string('─', width) + "╮", _secondaryOrange, _themeBgColor,HorizontalAlignment.Right);
            DisplayMessage("│ " + rawText + " │", _secondaryOrange, _themeBgColor, HorizontalAlignment.Right);
            DisplayMessage("╰" + new string('─', width) + "╯", _secondaryOrange, _themeBgColor, HorizontalAlignment.Right);
        }

        // ── BotLine ───────────────────────────────────────────────────────
        // Divider line — red, left aligned
        public void BotLine()
            => DisplayMessage("────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────",
                              _primaryRed, _themeBgColor, System.Windows.Forms.HorizontalAlignment.Left);

        // ── LogoDisplay ───────────────────────────────────────────────────
        // ASCII logo — orange/red, center aligned
        public void LogoDisplay()
        {
            if (ChatDisplay == null) return;
            DisplayMessage("", _secondaryOrange, _themeBgColor, System.Windows.Forms.HorizontalAlignment.Left);
            DisplayMessage("                    ╔═════════════════════════════════════════════════════════════════════════════════╗", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              ____ _  _ ___  ____ ____ ____ _  _ ____ ____ ___                   ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              |    |__| |__] |___ |__/ | __ |  | |__| |__/ |  \\                  ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║              |___  ||  |__] |___ |  \\ |__] |__| |  | |  \\ |__/                  ║", _primaryRed, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║                                                                                 ║", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ║                       Cyber Awareness & Education Chatbot                       ║", _lightTextColor, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("                    ╚═════════════════════════════════════════════════════════════════════════════════╝", _secondaryOrange, _themeBgColor, HorizontalAlignment.Center);
            DisplayMessage("", _secondaryOrange, _themeBgColor,     HorizontalAlignment.Left);
        }

        // ── Box ───────────────────────────────────────────────────────────
        // Wraps text in a rounded box — orange, left aligned
        public void Box(string text)
        {
            int width = text.Length + 2;
            DisplayMessage("╭" + new string('─', width) + "╮", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("│ " + text + " │", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
            DisplayMessage("╰" + new string('─', width) + "╯", _secondaryOrange, _themeBgColor, HorizontalAlignment.Left);
        }
    }
}