using System;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace CyberGuard
{
    // ══════════════════════════════════════════════════════════════════════
    // Program — entry point of the WPF application
    // Launches the MainWindow
    // ══════════════════════════════════════════════════════════════════════
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application app = new Application();
            app.Run(new MainWindow());
        }
    }
}