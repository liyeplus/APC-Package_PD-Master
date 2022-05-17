using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMatrix;

namespace DMC
{
    public partial class FrmLimit : Form
    {
        string Line;
        public string LineString { get { return Line; } set { Line = value; } }
        string Backup_Line;
        int nu;
        int ny;
        int nd;
        Matrix dumax;
        public Matrix dumax_Value { get { return dumax; } }
        Matrix dumin;
        public Matrix dumin_Value { get { return dumin; } }
        Matrix umax;
        public Matrix umax_Value { get { return umax; } }
        Matrix umin;
        public Matrix umin_Value { get { return umin; } }
        Matrix ymax;
        public Matrix ymax_Value { get { return ymax; } }
        Matrix ymin;
        public Matrix ymin_Value { get { return ymin; } }
        bool Flag = false;
        public bool isFlagTrue { get { return Flag; } }

        public FrmLimit(int Nu, int Ny, int Nd, string Line)
        {
            InitializeComponent();
            Flag = false;
            nu = Nu;
            ny = Ny;
            nd = Nd;
            this.textBox1.Text = Line;
            Backup_Line = this.textBox1.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Line = this.textBox1.Text;

            string[] s = Line.Trim("[ ]".ToCharArray()).Split(';');

            dumax = new Matrix(nu, 1);
            dumin = new Matrix(nu, 1);
            umax = new Matrix(nu, 1);
            umin = new Matrix(nu, 1);
            ymax = new Matrix(ny, 1);
            ymin = new Matrix(ny, 1);
            //dumax
            string[] SSS = s[0].Split(' ');
            int lens = SSS.Count();
            if (lens != nu)
            {
                Flag = false;
                MessageBox.Show("Constraint error in △Umax .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                dumax[i, 0] = Convert.ToDouble(SSS[i]);
            }
            //dumin
            SSS = s[1].Split(' ');
            lens = SSS.Count();
            if (lens != nu)
            {
                Flag = false;
                MessageBox.Show("Constraint error in △Umin .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                dumin[i, 0] = Convert.ToDouble(SSS[i]);
            }
            //umax
            SSS = s[2].Split(' ');
            lens = SSS.Count();
            if (lens != nu)
            {
                Flag = false;
                MessageBox.Show("Constraint error in Umax .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                umax[i, 0] = Convert.ToDouble(SSS[i]);
            }
            //umin
            SSS = s[3].Split(' ');
            lens = SSS.Count();
            if (lens != nu)
            {
                Flag = false;
                MessageBox.Show("Constraint error in Umin .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                umin[i, 0] = Convert.ToDouble(SSS[i]);
            }
            //ymax
            SSS = s[4].Split(' ');
            lens = SSS.Count();
            if (lens != ny)
            {
                Flag = false;
                MessageBox.Show("Constraint error in Ymax .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                ymax[i, 0] = Convert.ToDouble(SSS[i]);
            }
            //ymin
            SSS = s[5].Split(' ');
            lens = SSS.Count();
            if (lens != ny)
            {
                Flag = false;
                MessageBox.Show("Constraint error in Ymin .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                ymin[i, 0] = Convert.ToDouble(SSS[i]);
            }
            Flag = true;
            this.Close();
        }

        public void BackupLimit(bool flag)
        {
            if (flag)
            {
                Backup_Line = Line;
                this.textBox1.Text = Backup_Line;
            }
            else
            {
                this.textBox1.Text = Backup_Line;
            }
        }
    }
}
