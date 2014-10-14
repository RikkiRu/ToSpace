namespace Client
{
    partial class InGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1chat = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.glControl1 = new OpenTK.GLControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(751, 18);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(751, 33);
            this.panel3.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.richTextBox1chat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(162, 299);
            this.panel2.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 135);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(162, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Visible = false;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            // 
            // richTextBox1chat
            // 
            this.richTextBox1chat.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBox1chat.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.richTextBox1chat.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1chat.Name = "richTextBox1chat";
            this.richTextBox1chat.ReadOnly = true;
            this.richTextBox1chat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1chat.ShortcutsEnabled = false;
            this.richTextBox1chat.Size = new System.Drawing.Size(162, 135);
            this.richTextBox1chat.TabIndex = 0;
            this.richTextBox1chat.TabStop = false;
            this.richTextBox1chat.Text = "";
            this.richTextBox1chat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1chat_KeyDown);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(695, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(56, 299);
            this.panel4.TabIndex = 4;
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(162, 18);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(533, 299);
            this.glControl1.TabIndex = 5;
            this.glControl1.VSync = false;
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.glControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseClick);
            this.glControl1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDoubleClick);
            this.glControl1.Resize += new System.EventHandler(this.glControl1_Resize);
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // InGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 350);
            this.Controls.Add(this.glControl1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "InGameForm";
            this.ShowIcon = false;
            this.Text = "InGameForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InGameForm_FormClosing);
            this.Load += new System.EventHandler(this.InGameForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1chat;
        private System.Windows.Forms.Timer timer1;
    }
}