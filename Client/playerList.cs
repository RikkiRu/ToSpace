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
    public partial class playerList : Form
    {

        public bool showing = false;

        public playerList()
        {
            InitializeComponent();

            dataGridView1.ClearSelection();
        }

        private void playerList_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Tab:
                    this.showing = false;
                    this.Hide();
                    break;
            }
        }

        public void update(Dictionary<string, bool> listP)
        {

            dataGridView1.RowCount = listP.Count;

            for (int i = 0; i < listP.Count; i++)
            {
                dataGridView1[0, i].Value = listP.ElementAt(i).Key;
                if(listP.ElementAt(i).Value)
                {
                    dataGridView1[1, i].Style.BackColor = Color.Green;
                }

                else
                {
                    dataGridView1[1, i].Style.BackColor = Color.Gray;
                }
            }

            dataGridView1.ClearSelection();

            this.showing = true;
        }
    }
}
