using System;
using System.Media;
using System.Windows.Forms;
using System.Drawing;

namespace CyberGuard
{
    public class CyberDesign
    {
        protected RichTextBox ChatDisplay { get; set; }
        //this is for the voice greeting
        public void VoiceGreeting()
        {
            try
            {
                string wavPath = System.IO.Path.Combine(
                 AppDomain.CurrentDomain.BaseDirectory, "greet.wav");
                if(System.IO.File.Exists(wavPath))
                {
                    SoundPlayer player = new SoundPlayer(wavPath);
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
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
