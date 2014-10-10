using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class simpleTextEnter : Form
    {
        public simpleTextEnter(string caption)
        {
            InitializeComponent();
            label1.Text = caption;
        }

        public bool result = false;
        public string text = string.Empty;


        private void button2_Click(object sender, EventArgs e)
        {
            result = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            result = true;
            this.Close();
        }
    }
}
