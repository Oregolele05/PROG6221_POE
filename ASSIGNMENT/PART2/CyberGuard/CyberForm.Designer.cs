namespace CyberGuard
{
    partial class CyberForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.lnlInput = new System.Windows.Forms.Label();
            this.txtUserInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 1. Top Navigation Title Bar (Emulating Nav Gradient/Color)
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            // Background hex color #E63946 (Cyber Red)
            this.label1.BackColor = System.Drawing.Color.FromArgb(230, 57, 70); 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1200, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "CYBERGUARD — Cyber Awareness Bot";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 2. Main Chat Area Control Box
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // Background hex color #1E1E1E (Deep Card Panel Gray)
            this.richTextBox.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Font = new System.Drawing.Font("Courier New", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // Text hex color #EDEDED (Bright Gray)
            this.richTextBox.ForeColor = System.Drawing.Color.FromArgb(237, 237, 237);
            this.richTextBox.Location = new System.Drawing.Point(15, 60);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.WordWrap = false;
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.richTextBox.Size = new System.Drawing.Size(1170, 350);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";

            // 3. User Input Inline Label
            this.lnlInput.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left));
            this.lnlInput.AutoSize = true;
            this.lnlInput.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // Text hex color #FF8C42 (Neon Orange Accent Label)
            this.lnlInput.ForeColor = System.Drawing.Color.FromArgb(255, 140, 66);
            this.lnlInput.Location = new System.Drawing.Point(15, 433);
            this.lnlInput.Name = "lnlInput";
            this.lnlInput.Size = new System.Drawing.Size(44, 23);
            this.lnlInput.TabIndex = 1;
            this.lnlInput.Text = "You:";

            // 4. Interactive User Input Field Box
            this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            // Background hex color #1E1E1E (Deep Card Panel Gray)
            this.txtUserInput.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // Text hex color #EDEDED (Bright Gray)
            this.txtUserInput.ForeColor = System.Drawing.Color.FromArgb(237, 237, 237);
            this.txtUserInput.Location = new System.Drawing.Point(65, 430);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(1000, 32);
            this.txtUserInput.TabIndex = 2;

            // 5. Action Send Button
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
            // Background hex color #E63946 (Cyber Red Button Theme)
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(230, 57, 70);
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(1080, 429);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(105, 32);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;

            // Global Form Workspace Background Setup
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            // Background hex color #121212 (True Dark Mode Body Base)
            this.BackColor = System.Drawing.Color.FromArgb(18, 18, 18);
            this.ClientSize = new System.Drawing.Size(1200, 480);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.lnlInput);
            this.Controls.Add(this.richTextBox);
            this.Name = "CyberForm";
            this.Text = "CyberGuard - Cyber Awareness Bot";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Label lnlInput;
        private System.Windows.Forms.TextBox txtUserInput;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
    }
}