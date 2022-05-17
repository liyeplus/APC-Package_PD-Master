using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMatrix;      //列表显示新加

namespace DMC
{
    public partial class DMCtoOPC : Form        //DMCtoOPC类
    {
        FrmMain newMain = new FrmMain();

        string[] Y;     //字符、矩阵、Y
        public string[] opcYMatrix { get { return Y; } set { Y = value; } }
        string[] U;
        public string[] opcUMatrix { get { return U; } set { U = value; } }
        string[] D;
        public string[] opcDMatrix { get { return D; } set { D = value; } }

        int ny;     //CV
        int nu;     //MV
        int nd;     //DV

        ListView ListRefresh;       //创建ListView类的对象ListRefresh

        bool Flag = true;
        public bool isConnectOK { get { return Flag; } }

        public DMCtoOPC()
        {
            InitializeComponent();
        }

        public DMCtoOPC(int Nu, int Ny, int Nd,ListView lvRefresh)      //传值
        {
            InitializeComponent();
            nu = Nu;
            ny = Ny;
            nd = Nd;
            Y = new string[ny];
            U = new string[nu];
            D = new string[nd];
            ListRefresh = lvRefresh;
        }

        private void ConnectToOPC_Click(object sender, EventArgs e)     //CVMVDV的按钮Update
        {
            double num2 = Convert.ToDouble(DMCItem.Text);
            int num = (int)num2 - 1;        //在输入框中从1开始计数，代码中从0开始计数
            if (chooseY.Checked)
            {
                Y[num] = OPCItem.Text;
                ListRefresh.Items[num + nu].SubItems[2].Text = Y[num];
            }
            if (chooseU.Checked)
            {
                U[num] = OPCItem.Text;
                ListRefresh.Items[num].SubItems[2].Text = U[num];
                string[] sArray = U[num].Split('.');
                int j = sArray.Count();
             
            }
            if (chooseD.Checked)
            {
                D[num] = OPCItem.Text;
                ListRefresh.Items[num + nu + ny].SubItems[2].Text = D[num];
            }                    
        }

        private void button1_Click(object sender, EventArgs e)      //TEST按钮
        {
            int itemindex = 0;

            U[0] = "1420FIC4054.PIDA.SP";
            U[1] = "1420FIC4067.PIDA.SP";
            Y[0] = "1420FFI4068.DACA.PV";
            Y[1] = "T1_4080,4081,4084,2085四取中";
            Y[2] = "软测量炉温";
            Y[3] = "1420PDI4087.DACA.PV";
            Y[4] = "1420FI4203.DACA.PV";
            Y[5] = "CO+H2";
            D[0] = "1420FC4016.DACA.PV";
            for (int i = 0; i < nu; i++)
            {

                ListRefresh.Items[itemindex].SubItems[2].Text = U[i];        
                itemindex++;
            }
         

            for (int i = 0; i < ny; i++)
            {
                ListRefresh.Items[itemindex].SubItems[2].Text = Y[i];
                itemindex++;
            }

            for (int i = 0; i < nd; i++)
            {
                ListRefresh.Items[itemindex].SubItems[2].Text = D[i];
                itemindex++;
            }

        }

       
    }
}
