using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contract;

namespace Client
{
    public partial class uppanelPlanet : UserControl
    {
        public uppanelPlanet(RecieveDel del)
        {
            InitializeComponent();
            newName = del;
        }

        public event RecieveDel newName;

        static Color good = Color.Green;
        static Color warning = Color.Yellow;
        static Color bad = Color.Red;

        public void setName(string name, bool canChange)
        {
            button1.Text = name;
            button1.Enabled = canChange;
        }

        public void setResourse(int o2, int water, int trash, int temperature)
        {
            label2o2.Text = o2.ToString();
            label8water.Text = water.ToString();
            label5trash.Text = trash.ToString();
            label7t.Text = temperature.ToString();

            if (o2 > 30) label2o2.ForeColor = good;
            else
            {
                if (o2 > 5) label2o2.ForeColor = warning;
                else label2o2.ForeColor = bad;
            }

            if (water > 30) label8water.ForeColor = good;
            else
            {
                if (water > 5) label8water.ForeColor = warning;
                else label8water.ForeColor = bad;
            }

            if (trash < 70) label5trash.ForeColor = good;
            else
            {
                if (water < 95) label5trash.ForeColor = warning;
                else label5trash.ForeColor = bad;
            }

            if (temperature < 30 && temperature > -30) label7t.ForeColor = good;
            else
            {
                if (temperature < 60 && temperature > -60) label7t.ForeColor = warning;
                else label7t.ForeColor = bad;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = InGameForm.enterText("Введите новое имя для планеты");
            if(a!=string.Empty)
            {
                string[] res = new string[2];
                res[0] = a;
                newName(res);
            }
        }
    }
}
