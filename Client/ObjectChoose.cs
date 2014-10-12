using Contract;
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

    public partial class ObjectChoose : Form
    {
        object x;
        System.Timers.Timer time;
        Dictionary<string, EventHandler> buts;

        public ObjectChoose(object x, Dictionary<string, EventHandler> buttons)
        {
            InitializeComponent();

            this.x = x;
            this.buts = buttons;
        }

        private void ObjectChoose_Load(object sender, EventArgs e)
        {
            makeButtons();

            pictureBox1.Image = Image.FromFile(@"Textures/planet/Capital/1.png");

            update();

            time = new System.Timers.Timer(GameObject.timeTick);
            time.Elapsed += resUpdate;
            time.Start();
        }

        void makeButtons()
        {
            if (buts != null)
            {
                foreach (var a in buts)
                {
                    Button nB = new Button();
                    nB.Text = a.Key;
                    nB.Click += a.Value;
                    nB.Click += closeThis;
                    nB.Dock = DockStyle.Top;
                    nB.Height = 25;

                    panel1.Controls.Add(nB);
                }
            }
        }

        private void closeThis(object sender, EventArgs e)
        {
            this.Close();
        }

        private void resUpdate(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (x is capitialResource)
            {
                capitialResource a = (capitialResource)x;
                a.delta();
            }

            update();
        }

        void update()
        {
            if (x is capitialResource)
            {
                capitialResource a = (capitialResource)x;

                label1.Text = "Столица";

                dataGridView1.RowCount = 7;

                dataGridView1[0, 0].Value = "Еда";
                dataGridView1[1, 0].Value = a.eat;
                dataGridView1[2, 0].Value = a.d_eat;

                dataGridView1[0, 1].Value = "Вода";
                dataGridView1[1, 1].Value = a.myWater;
                dataGridView1[2, 1].Value = a.d_myWater;

                dataGridView1[0, 2].Value = "Кислород";
                dataGridView1[1, 2].Value = a.myO2;
                dataGridView1[2, 2].Value = a.d_myO2;

                dataGridView1[0, 3].Value = "Дерево";
                dataGridView1[1, 3].Value = a.wood;
                dataGridView1[2, 3].Value = a.d_wood;

                dataGridView1[0, 4].Value = "Безработные";
                dataGridView1[1, 4].Value = a.populationFree;
                dataGridView1[2, 4].Value = a.d_populdation;

                dataGridView1[0, 5].Value = "Занятые рабочие";
                dataGridView1[1, 5].Value = a.populationWorkers;
                dataGridView1[2, 5].Value = "-";

                dataGridView1[0, 6].Value = "Лимит населения";
                dataGridView1[1, 6].Value = a.populationLimit;
                dataGridView1[2, 6].Value = "-";
            }
        }
    }
}
