using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToSpace_
{
    public partial class FormMain : Form
    {
        StructureObjects gameStruct;
        Connection connect;
        eventHandler manager;

        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_start_Click(object sender, EventArgs e)
        {


            this.Hide();
        }

        public void startServer()
        {
            gameStruct = new StructureObjects();
            connect = new Connection();
            connect.Start(gameStruct.PlayerList, gameStruct.passwords);

            manager = new eventHandler(connect, gameStruct);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(connect!=null) connect.close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            startServer();

        }
    }
}
