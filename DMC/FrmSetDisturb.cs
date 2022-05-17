using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMC
{
    public partial class FrmSetDisturb : Form
    {
        string Line;
        public string LineString { get { return Line; } set { Line = value; } }
        int Nd = 0;
        double[] r;
        public double[] SetDisturb { get { return r; } }
        bool Flag = false;
        public bool isFlagTrue { get { return Flag; } }

        public FrmSetDisturb(int nd)
        {
            InitializeComponent();
            Nd = nd;
            Flag = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Line = this.textBox1.Text;
            string[] s = this.textBox1.Text.Trim("[ ]".ToCharArray()).Split(';');
            int lens = s.Count();
            if (lens == Nd)
            {
                r = new double[lens];
                for (int i = 0; i < lens; i++)
                {
                    r[i] = Convert.ToDouble(s[i]);
                }
                Flag = true;
                this.Close();
            }
            else
            {
                Flag = false;
                MessageBox.Show("Dimension error of disturbance variable.");
            }
        }
    }
}
