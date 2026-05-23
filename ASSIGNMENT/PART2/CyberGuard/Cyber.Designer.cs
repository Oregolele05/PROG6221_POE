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

            // richTextBox
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(
                System.Windows.Forms.AnchorStyles.Top |
                System.Windows.Forms.AnchorStyles.Bottom |
                System.Windows.Forms.AnchorStyles.Left |
                System.Windows.Forms.AnchorStyles.Right));
            this.richTextBox.BackColor = System.Drawing.Color.FromArgb(11, 30, 54);
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.ForeColor = System.Drawing.Color.FromArgb(0, 201, 167);
            this.richTextBox.Location = new System.Drawing.Point(5, 35);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.WordWrap = false;
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Both;
            this.richTextBox.Size = new System.Drawing.Size(1190, 390);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);

            // lnlInput
            this.lnlInput.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            this.lnlInput.AutoSize = true;
            this.lnlInput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnlInput.ForeColor = System.Drawing.Color.FromArgb(0, 201, 167);
            this.lnlInput.Location = new System.Drawing.Point(12, 438);
            this.lnlInput.Name = "lnlInput";
            this.lnlInput.Size = new System.Drawing.Size(45, 20);
            this.lnlInput.TabIndex = 1;
            this.lnlInput.Text = "You:";
            this.lnlInput.Click += new System.EventHandler(this.label1_Click);

            // txtUserInput
            this.txtUserInput.Anchor = System.Windows.Forms.AnchorStyles.Bottom |
                                       System.Windows.Forms.AnchorStyles.Left |
                                       System.Windows.Forms.AnchorStyles.Right;
            this.txtUserInput.BackColor = System.Drawing.Color.FromArgb(11, 30, 54);
            this.txtUserInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserInput.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserInput.ForeColor = System.Drawing.Color.FromArgb(0, 201, 167);
            this.txtUserInput.Location = new System.Drawing.Point(65, 435);
            this.txtUserInput.Name = "txtUserInput";
            this.txtUserInput.Size = new System.Drawing.Size(1010, 27);
            this.txtUserInput.TabIndex = 2;
            this.txtUserInput.TextChanged += new System.EventHandler(this.txtUserInput_TextChanged);
            this.txtUserInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserInput_KeyDown);

            // btnSend
            this.btnSend.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.btnSend.BackColor = System.Drawing.Color.DarkCyan;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(1085, 433);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(111, 29);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            // label1 - title bar
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(0, 201, 167);
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1200, 32);
            this.label1.TabIndex = 4;
            this.label1.Text = "CYBERGUARD — Cyber Awareness Bot";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(11, 30, 54);
            this.ClientSize = new System.Drawing.Size(1200, 475);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtUserInput);
            this.Controls.Add(this.lnlInput);
            this.Controls.Add(this.richTextBox);
            this.ForeColor = System.Drawing.Color.FromArgb(0, 201, 167);
            this.Name = "CyberForm";
            this.Text = "CyberGuard - Cyber Awareness Bot";
            this.Load += new System.EventHandler(this.Form1_Load);
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