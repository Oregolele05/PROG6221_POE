using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace CyberGuard
{
    // ══════════════════════════════════════════════════════════════════════
    // WpfChatDisplay — bridges CyberDesign to the WPF RichTextBox
    // WPF RichTextBox uses FlowDocument instead of AppendText()
    // This class wraps it so CyberDesign.DisplayMessage() still works
    // without modifying any of the existing logic classes
    // ══════════════════════════════════════════════════════════════════════
    public class CyberChatDisplay
    {
        private readonly RichTextBox _chatBox;

        public CyberChatDisplay(RichTextBox chatBox)
        {
            _chatBox = chatBox;
            // Clear default empty paragraph
            _chatBox.Document.Blocks.Clear();
        }

        // ── AppendText ────────────────────────────────────────────────────
        // Appends a coloured line to the WPF RichTextBox
        // Called by CyberDesign.DisplayMessage() for every output line
        // Accepts System.Drawing.Color and converts to WPF Color
        public void AppendText(string message,
                               System.Drawing.Color textColour,
                               System.Drawing.Color bgColour,
                               System.Windows.Forms.HorizontalAlignment alignment)
        {
            // Convert System.Drawing.Color → WPF Color
            var wpfFg = Color.FromRgb(textColour.R, textColour.G, textColour.B);
            var wpfBg = Color.FromRgb(bgColour.R, bgColour.G, bgColour.B);

            var run = new Run(message)
            {
                Foreground = new SolidColorBrush(wpfFg),
                Background = new SolidColorBrush(wpfBg)
            };

            // Map WinForms alignment to WPF TextAlignment
            TextAlignment wpfAlign;
            switch (alignment)
            {
                case System.Windows.Forms.HorizontalAlignment.Right:
                    wpfAlign = TextAlignment.Right;
                    break;
                case System.Windows.Forms.HorizontalAlignment.Center:
                    wpfAlign = TextAlignment.Center;
                    break;
                default:
                    wpfAlign = TextAlignment.Left;
                    break;
            }

            var para = new Paragraph(run)
            {
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                FontFamily = new FontFamily("Courier New"),
                FontSize = 12,
                FontWeight = FontWeights.Bold,
                TextAlignment = wpfAlign,
                Background = new SolidColorBrush(wpfBg)
            };

            _chatBox.Document.Blocks.Add(para);
            _chatBox.ScrollToEnd();
        }
    }
}