using Contract;
using NAudio.Wave;
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

namespace Client
{
    public partial class FormBegin : Form
    {
        ClientConnectionSys connection;
        Player me;
        InGameForm game;
        WaveOut waveOut = new WaveOut();
        LoopStream loop = new LoopStream(new Mp3FileReader(@"Sound/background/Chris_Zabriskie_-_09_-_Cylinder_Nine.mp3"));
      
        //Chris_Zabriskie_-_09_-_Cylinder_Nine.mp3

        public static bool SuccesConnected = false;
        public static bool Tested = false;
        public static string welcomeStr = String.Empty;

        public FormBegin()
        {
            InitializeComponent();
        }

        private void button1join_Click(object sender, EventArgs e)
        {
            if(connection!=null)
            {
                connection.disconnect();
            }

            me.name = textBox3name.Text;
            me.online = true;
            me.civSettings = new CivSettings();
            me.civSettings.name = "Человеки";
            

            int port = 0;
            try
            {
                port = Convert.ToInt32(textBox2port.Text);


                Tested = false;
                connection = new ClientConnectionSys(me, textBox1ip.Text, port, textBox4pass.Text);


                while(!Tested)
                {
                    Thread.Sleep(100);
                }

                if (SuccesConnected)
                {
                    game = new InGameForm(me, connection);
                    game.FormClosed += game_FormClosed;
                    game.addToChat("Сервер", welcomeStr);
                    this.Hide();
                }
            }

            catch(Exception ex)
            { 
                MessageBox.Show(ex.Message); 
            }
        }

        void game_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
            game.Close();
        }

        private void FormBegin_Load(object sender, EventArgs e)
        {
            me = new Player();

            waveOut.Init(loop);
            waveOut.Play();
        }

        private void FormBegin_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }

    
}
