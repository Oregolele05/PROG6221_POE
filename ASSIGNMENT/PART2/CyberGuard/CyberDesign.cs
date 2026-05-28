using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace CyberGuard
{
    public class CyberDesign
    {
        protected RichTextBox ChatDisplay { get; set; }

        public void VoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer(@"C:\Users\gmkin\source\repos\Oregolele05\PROG6221_POE\PROG6221_POE\ASSIGNMENT\PART2\CyberGuard\greet.wav");
                player.Play();
            }
            catch (Exception ex)
            {
                BotWarn("Error playing sound: " + ex.Message);
            }
        }

        public void BotSay(string message)
            => DisplayMessage("🤖  " + message, Color.FromArgb(230, 57, 70));

        public void BotWarn(string message)
            => DisplayMessage("⚠   " + message, Color.FromArgb(230, 57, 70));

        public void BotHeader(string message)
            => DisplayMessage("\n══════ " + message + " ══════", Color.FromArgb(255, 140, 66));

        public void BotInfo(string message)
            => DisplayMessage("     " + message, Color.FromArgb(230, 57, 70));

        public void UserSay(string message)
            => DisplayMessage("👤  " + message, Color.FromArgb(179, 179, 179));

        // Restored to your original BotLine color
        public void BotLine()
            => DisplayMessage("───────────────────────────────────────────────────────────────────────────────────────────────────────", Color.FromArgb(230, 57, 70));

        // Restored to your exact original line-art logo style, perfectly centered
        // FIXED: Removed SelectionAlignment centering to fix the skewed frame borders. 
        // Uses consistent string lengths and precise padding spaces to center beautifully.
        public void LogoDisplay()
        {
            Color orangeAccent = Color.FromArgb(255, 140, 66);
            Color redAccent = Color.FromArgb(230, 57, 70);
            Color lightText = Color.FromArgb(237, 237, 237);

            // Slightly optimized margins specifically balanced for a clean look in Consolas 11pt
            DisplayMessage("", orangeAccent);
            DisplayMessage("  ╔═════════════════════════════════════════════════════════════════════════════════╗", orangeAccent);
            DisplayMessage("  ║    ____ _  _ ___  ____ ____ ____ _  _ ____ ____ ___                             ║", redAccent);
            DisplayMessage("  ║    |    |__| |__] |___ |__/ | __ |  | |__| |__/ |  \\                            ║", redAccent);
            DisplayMessage("  ║    |___  ||  |__] |___ |  \\ |__] |__| |  | |  \\ |__/                            ║", redAccent);
            DisplayMessage("  ║                                                                                 ║", orangeAccent);
            DisplayMessage("  ║                       Cyber Awareness & Education Chatbot                       ║", lightText);
            DisplayMessage("  ╚═════════════════════════════════════════════════════════════════════════════════╝", orangeAccent);
            DisplayMessage("", orangeAccent);
        }

        public void Box(string text)
        {
            Color orangeAccent = Color.FromArgb(255, 140, 66);
            int width = text.Length + 2;
            DisplayMessage("╔" + new string('═', width) + "╗", orangeAccent);
            DisplayMessage("║ " + text + " ║", orangeAccent);
            DisplayMessage("╚" + new string('═', width) + "╝", orangeAccent);
        }

        public void DisplayMessage(string message, Color colour)
        {
            if (ChatDisplay == null) return;

            ChatDisplay.SelectionColor = colour;
            ChatDisplay.AppendText(message + "\n");
            ChatDisplay.ScrollToCaret();
        }
    }
}