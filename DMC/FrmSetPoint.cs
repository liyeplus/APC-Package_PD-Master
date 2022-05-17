using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMatrix;
using OPCDA.NET;
using OPC;
using OPCDA;

namespace DMC
{
    public partial class FrmSetPoint : Form
    {
        string Backup_Line;
        string Line;
        public string LineString { get { return Line; } set { Line = value; } }
        int Ny = 0;
        double[] r;
        public double[] SetPoint { get { return r; } }
        bool Flag = false;
        public bool isFlagTrue { get { return Flag; } }
        Matrix umax;
        Matrix umin;
        FrmOPC OPCpanel;

        public FrmSetPoint(int ny, string Line, Matrix Umax, Matrix Umin)
        {
            umax = Umax;
            umin = Umin;
            InitializeComponent();
            Ny = ny;
            this.setpoint.Text = Line;
            Backup_Line = this.setpoint.Text;
            Flag = false;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Line = this.setpoint.Text;
            string[] s = this.setpoint.Text.Trim("[ ]".ToCharArray()).Split(';');
            int lens = s.Count();
            if (lens == Ny)
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
                MessageBox.Show("Dimension error of controlled variable.");
            }
        }

        public void BackupSetpoint(bool flag)
        {
            if (flag)
            {
                Backup_Line = Line;
                this.setpoint.Text = Backup_Line;
            }
            else
            {
                this.setpoint.Text = Backup_Line;
            }
        }

        private void FrmSetPoint_Load(object sender, EventArgs e)
        {

        }

    }
}
