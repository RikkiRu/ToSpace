namespace Client
{
    partial class FormBegin
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
            this.button1join = new System.Windows.Forms.Button();
            this.textBox1ip = new System.Windows.Forms.TextBox();
            this.textBox2port = new System.Windows.Forms.TextBox();
            this.button2setup = new System.Windows.Forms.Button();
            this.textBox3name = new System.Windows.Forms.TextBox();
            this.textBox4pass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1join
            // 
            this.button1join.Location = new System.Drawing.Point(12, 38);
            this.button1join.Name = "button1join";
            this.button1join.Size = new System.Drawing.Size(141, 23);
            this.button1join.TabIndex = 0;
            this.button1join.Text = "Вступить в игру";
            this.button1join.UseVisualStyleBackColor = true;
            this.button1join.Click += new System.EventHandler(this.button1join_Click);
            // 
            // textBox1ip
            // 
            this.textBox1ip.Location = new System.Drawing.Point(12, 67);
            this.textBox1ip.Name = "textBox1ip";
            this.textBox1ip.Size = new System.Drawing.Size(81, 20);
            this.textBox1ip.TabIndex = 1;
            this.textBox1ip.Text = "127.0.0.1";
            // 
            // textBox2port
            // 
            this.textBox2port.Location = new System.Drawing.Point(99, 67);
            this.textBox2port.Name = "textBox2port";
            this.textBox2port.Size = new System.Drawing.Size(54, 20);
            this.textBox2port.TabIndex = 2;
            this.textBox2port.Text = "8083";
            // 
            // button2setup
            // 
            this.button2setup.Location = new System.Drawing.Point(12, 93);
            this.button2setup.Name = "button2setup";
            this.button2setup.Size = new System.Drawing.Size(141, 23);
            this.button2setup.TabIndex = 3;
            this.button2setup.Text = "Настроить рассу";
            this.button2setup.UseVisualStyleBackColor = true;
            // 
            // textBox3name
            // 
            this.textBox3name.Location = new System.Drawing.Point(12, 12);
            this.textBox3name.Name = "textBox3name";
            this.textBox3name.Size = new System.Drawing.Size(68, 20);
            this.textBox3name.TabIndex = 4;
            this.textBox3name.Text = "Player001";
            // 
            // textBox4pass
            // 
            this.textBox4pass.Location = new System.Drawing.Point(86, 12);
            this.textBox4pass.Name = "textBox4pass";
            this.textBox4pass.Size = new System.Drawing.Size(100, 20);
            this.textBox4pass.TabIndex = 5;
            this.textBox4pass.Text = "1234";
            this.textBox4pass.UseSystemPasswordChar = true;
            // 
            // FormBegin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(195, 132);
            this.Controls.Add(this.textBox4pass);
            this.Controls.Add(this.textBox3name);
            this.Controls.Add(this.button2setup);
            this.Controls.Add(this.textBox2port);
            this.Controls.Add(this.textBox1ip);
            this.Controls.Add(this.button1join);
            this.Name = "FormBegin";
            this.Text = "ToSpace!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBegin_FormClosing);
            this.Load += new System.EventHandler(this.FormBegin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1join;
        private System.Windows.Forms.TextBox textBox1ip;
        private System.Windows.Forms.TextBox textBox2port;
        private System.Windows.Forms.Button button2setup;
        private System.Windows.Forms.TextBox textBox3name;
        private System.Windows.Forms.TextBox textBox4pass;
    }
}

