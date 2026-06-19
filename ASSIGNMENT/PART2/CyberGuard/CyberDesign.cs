using System;
<<<<<<< HEAD
=======
using System.Drawing;
using System.IO;
>>>>>>> b0669dda2cf2c1cac766f8f08cfbdfa39e545e94
using System.Media;
using System.Windows.Forms;

namespace CyberGuard
{
    public class CyberDesign
    {
        protected RichTextBox ChatDisplay { get; set; }
<<<<<<< HEAD
        //this is for the voice greeting
=======

        // Core Theme Colors
        private readonly Color _themeBgColor = Color.FromArgb(30, 30, 30);    // CSS #1E1E1E (Dark Gray)
        private readonly Color _primaryRed = Color.FromArgb(230, 57, 70);    // CSS #E63946 (Cyber Red)
        private readonly Color _secondaryOrange = Color.FromArgb(255, 140, 66); // CSS #FF8C42 (Neon Orange)
        private readonly Color _lightTextColor = Color.FromArgb(237, 237, 237); // CSS #EDEDED (Bright Text)

>>>>>>> b0669dda2cf2c1cac766f8f08cfbdfa39e545e94
        public void VoiceGreeting()
        {
            try
            {
<<<<<<< HEAD
                string wavPath = System.IO.Path.Combine(
                 AppDomain.CurrentDomain.BaseDirectory, "greet.wav");
                if(System.IO.File.Exists(wavPath))
                {
                    SoundPlayer player = new SoundPlayer(wavPath);
                    player.PlaySync();
=======
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string soundPath = Path.Combine(baseDir, "greet.wav");

                if (File.Exists(soundPath))
                {
                    using (SoundPlayer player = new SoundPlayer(soundPath))
                    {
                        player.Play();
                    }
>>>>>>> b0669dda2cf2c1cac766f8f08cfbdfa39e545e94
                }
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                BotWarn("Error playing sound: " + ex.Message);
            }
        }
        // Bot response — cyan
        public void BotSay(string message)
            => DisplayMessage("🤖  " + message, Color.Cyan);

        // Warning or error — red
        public void BotWarn(string message)
            => DisplayMessage("⚠   " + message, Color.Tomato);

        // Section header — gold
        public void BotHeader(string message)
            => DisplayMessage("\n══════ " + message + " ══════", Color.Gold);

        // Info bullet point — light cyan
        public void BotInfo(string message)
            => DisplayMessage("     " + message, Color.LightCyan);

        // User's own message — green
        public void UserSay(string message)
            => DisplayMessage("👤  " + message, Color.LightGreen);

        // Separator line — dim gray
        public void BotLine()
            => DisplayMessage("─────────────────────────────────────────", Color.DimGray);


        //this is for the logo design
        public void LogoDisplay()
        {
            DisplayMessage("╔════════════════════════════════════════════════════════════════════════════════════╗\r\n║  ██████╗██╗   ██╗██████╗ ███████╗██████╗  ██████╗ ██╗   ██╗ █████╗ ██████╗ ██████╗ ║\r\n║ ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔════╝ ██║   ██║██╔══██╗██╔══██╗██╔══██╗║\r\n║ ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝██║  ███╗██║   ██║███████║██████╔╝██║  ██║║\r\n║ ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██║   ██║██║   ██║██╔══██║██╔══██╗██║  ██║║\r\n║ ╚██████╗   ██║   ██████╔╝███████╗██║  ██║╚██████╔╝╚██████╔╝██║  ██║██║  ██║██████╔╝║\r\n║  ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═════╝ ║\r\n╚════════════════════════════════════════════════════════════════════════════════════╝", Color.DodgerBlue);
        }
        public void Box(string text)
        {
            int width = text.Length + 2;
            //this will wrap the users welcome message in a box
            DisplayMessage("╔" + new string('═', width) + "╗", Color.DodgerBlue);
            DisplayMessage("║ " + text + " ║", Color.DodgerBlue);
            DisplayMessage("╚" + new string('═', width) + "╝", Color.DodgerBlue);
        }
        public void DisplayMessage(string message, Color colour)
        {
            if (ChatDisplay == null)
            {
                return;
            }
            ChatDisplay.SelectionColor = colour;
            ChatDisplay.AppendText(message + "\n");
            ChatDisplay.ScrollToCaret();
        }
    }
}
=======
                
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
>>>>>>> b0669dda2cf2c1cac766f8f08cfbdfa39e545e94
