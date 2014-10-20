using Contract;
using GreenRender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Threading;
using NAudio.Wave;

namespace Client
{
    public partial class InGameForm : Form
    {
        Player me;
        ClientConnectionSys connect;
        Renderer render;
        playerList tabList = new playerList();
        object upPanel = null;
        ObjectChoose objChoose = null;

        public InGameForm(Player me, ClientConnectionSys connection)
        {
            InitializeComponent();

            this.me = me;

            this.connect = connection;

            this.Show();
        }

        public void ParseToChart(object o)
        {
            string[] t = (string[])o;
            Invoke(new Action(() => addToChat(t[0], t[1])));
        }

        public void addToChat(string name, string text)
        {
            if (richTextBox1chat.Text.Length > 500) richTextBox1chat.Text = richTextBox1chat.Text.Substring(richTextBox1chat.Text.Length - 300);

            richTextBox1chat.Text += name + ": " + text+Environment.NewLine;

            richTextBox1chat.SelectionStart = richTextBox1chat.Text.Length;
            richTextBox1chat.ScrollToCaret();
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:

                    

                    break;

                case Keys.A:
                    if (textBox1.Visible) break;
                    render.position.a = true;
                    break;

                case Keys.D:
                    if (textBox1.Visible) break;
                    render.position.d = true;
                    break;

                case Keys.W:
                    if (textBox1.Visible) break;
                    render.position.w = true;
                    break;

                case Keys.S:
                    if (textBox1.Visible) break;
                    render.position.s = true;
                    break;

                case Keys.Tab:
                    if (!tabList.showing)
                    {
                        connect.send(new Sending { operation = "getPlayerList", data = me });
                        render.position.w = false;
                        render.position.s = false;
                        render.position.a = false;
                        render.position.d = false;
                    }

                    break;
            }
        }

        private void InGameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            connect.disconnect();
            if(objChoose!=null) objChoose.Close();
            if(tabList!=null) tabList.Close();
            if (render != null)
            {
                render.display = null;
                render = null;
            }
        }

        private void InGameForm_Load(object sender, EventArgs e)
        {

            connect.addToEvent("chat", ParseToChart);
            connect.addToEvent("takeYourMap!", loadMap);
            connect.addToEvent("playerList", showPlayerList);

            render = new Renderer(glControl1, me, connect);
            timer1.Enabled = true;
            connect.send(new Sending { operation = "IwantMyMap!", data = me });
        }




      

        public void loadMap(object o)
        {
            render.currentMap = (GameMap)o;

            render.position.xMax = render.quadSize * render.currentMap.sizeX / 2;
            render.position.yMax = render.quadSize * render.currentMap.sizeY / 2;
            render.position.xMin = -render.position.xMax;
            render.position.yMin = -render.position.yMax;

            render.position.x = render.position.xMax / 2 + render.position.xMin / 2;
            render.position.y = render.position.yMax / 2 + render.position.yMin / 2;

            render.backgroundQuad = null;

            if(o is MapPlanet)
            {
                uppanelPlanet upPanelT = new uppanelPlanet(setNewNamePlanet);
                upPanelT.Dock = DockStyle.Fill;
                

                upPanelT.setName((o as MapPlanet).name, !(o as MapPlanet).wasRenamed);

                Invoke(new Action(() => panel1.Controls.Clear()));
                Invoke(new Action(() => panel1.Height = upPanelT.Height));
                Invoke(new Action(()=> panel1.Controls.Add(upPanelT)));

                upPanel = upPanelT;
            }
        }

        public void showPlayerList(object o)
        {
            Dictionary<string, bool> playerList = (Dictionary<string, bool>)o;

            tabList.update(playerList);

            tabList.ShowDialog();
        }

        const int WM_KEYDOWN = 0x100;
        const int WM_SYSKEYDOWN = 0x104;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Tab:
                        OnKeyDown(new KeyEventArgs(Keys.Tab));
                        break;

                    case Keys.Enter:
                        if (textBox1.Visible == false)
                        {
                            textBox1.Visible = true;
                            textBox1.Focus();
                        }

                        else
                        {
                            if (textBox1.Text != String.Empty)
                            {
                                SignedData sd = new SignedData { from = me, data = textBox1.Text };
                                connect.send(new Sending { operation = "chat", data = sd });

                                textBox1.Text = String.Empty;
                            }

                            textBox1.Visible = false;
                        }
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
	

        private void timer1_Tick(object sender, EventArgs e)
        {
            render.position.move();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            if (render != null) render.draw();
            glControl1.SwapBuffers();
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            try
            {
                render.backgroundQuad = null;
                render.display.resize();
            }
            catch { }
        }

        private void glControl1_MouseClick(object sender, MouseEventArgs e)
        {
            makeClick(e, false);
        }

        private void glControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            makeClick(e, true);
        }

        void makeClick(MouseEventArgs e, bool doubleC)
        {
            float x = e.X;
            float y = e.Y;

            x = x / glControl1.Width * render.display.ortoX - render.position.x;
            y = y / glControl1.Height * render.display.ortoY - render.position.y;

            render.makeSelection(x, y, e, true);
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.A:
                    if (textBox1.Visible) break;
                    render.position.a = false;
                    break;

                case Keys.D:
                    if (textBox1.Visible) break;
                    render.position.d = false;
                    break;

                case Keys.W:
                    if (textBox1.Visible) break;
                    render.position.w = false;
                    break;

                case Keys.S:
                    if (textBox1.Visible) break;
                    render.position.s = false;
                    break;
            }
        }

        private void richTextBox1chat_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public static string enterText(string caption)
        {
            simpleTextEnter t = new simpleTextEnter(caption);
            t.ShowDialog();
            string res = t.text;
            t.Close();
            return res;
        }

        public void setNewNamePlanet(object o)
        {
            (o as string[])[1] = me.name;
            connect.send(new Sending { operation = "newPlanetName", data = o });
        }
    }
}
