using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CMatrix;

namespace DMC
{
    public partial class FrmSimulate : Form
    {
        Matrix A;
        public Matrix aMatrix { get { return A; } set { A = value; } }
        Matrix Bb;
        public Matrix bbMatrix { get { return Bb; } set { Bb = value; } }
        Matrix Bd;
        public Matrix bdMatrix { get { return Bd; } set { Bd = value; } }
        Matrix C;
        public Matrix cMatrix { get { return C; } set { C = value; } }
        Matrix D;
        public Matrix dMatrix { get { return D; } set { D = value; } }

        bool OK;
        public bool isOK { get { return OK; } }

        Matrix delta_u;
        public Matrix duMatrix { get { return delta_u; } set { delta_u = value; } }
        Matrix u;
        public Matrix uMatrix { get { return u; } set { u = value; } }
        Matrix y;
        public Matrix yMatrix { get { return y; } set { y = value; } }
        Matrix x;
        public Matrix xMatrix { get { return x; } set { x = value; } }

        public FrmSimulate()
        {
            InitializeComponent();
        }

        private void StateComfirm_Click(object sender, EventArgs e)
        {
            A = GetState(state_A.Text);
            Bb = GetState(state_Bb.Text);
            Bd = GetState(state_Bd.Text);
            C = GetState(state_C.Text);
            D = GetState(state_D.Text);

            delta_u = GetState(delta_U.Text);
            u = GetState(U.Text);
            y = GetState(Y.Text);
            x = GetState(X.Text);

            OK = true;
            this.Close();
        }

        private Matrix GetState(string a)
        {
            string[] s = a.Trim("[ ]".ToCharArray()).Split(';');
            string[] s1 = s[0].Split(' ');

            int rows = s.Count();
            int cols = s1.Count();
            double[,] ss = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string[] ss_temp = s[i].Split(' ');
                for (int j = 0; j < cols; j++)
                {
                    ss[i, j] = Convert.ToDouble(ss_temp[j]);
                }
            }
            Matrix st = new Matrix(ss);
            return st;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            OK = false;
            this.Close();
        }

        private void SeeA_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = GetState(state_A.Text);
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeBb_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = GetState(state_Bb.Text);
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeBd_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = GetState(state_Bd.Text);
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeC_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = GetState(state_C.Text);
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeD_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = GetState(state_D.Text);
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void InsertABCDuy_Click(object sender, EventArgs e)
        {
            string train_FileName = string.Empty;
            string train_SafeFileName = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "文本文件|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                train_FileName = ofd.FileName;
                train_SafeFileName = ofd.SafeFileName;
                //txtb_TrainPath.Text = train_FileName;
                //btn_StartTrain.IsEnabled = true;
            }

            //int trainSampleNumber = 0;
            //int trainMaxIndex = 0;
            //List<double> trainLabel = new List<double>();
            List<string> trainData = new List<string>();
            FileStream fs = new FileStream(train_FileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                //int n = s.Count();
                //if (n - 1 > trainMaxIndex) trainMaxIndex = n - 1;
                //trainLabel.Add(Convert.ToDouble(s[0]));

                //double[] aNode = new double[n - 1];
                //for (int i = 0; i < n - 1; i++)
                //{
                //    aNode[i] = new double();
                //}
                //for (int i = 0; i < n - 1; i++)
                //{
                //    string[] ss = s[i + 1].Split(':');//字符串用冒号分隔
                //    //aNode[i].Index = Convert.ToInt32(ss[0]);
                //    aNode[i] = Convert.ToDouble(ss[1]);
                //}
                trainData.Add(s);
                //trainSampleNumber++;
            }//将输入的训练数据加入数组
            sr.Close();
            fs.Close();

            state_A.Text = trainData[0];
            state_Bb.Text = trainData[1];
            state_Bd.Text = trainData[2];
            state_C.Text = trainData[3];
            state_D.Text = trainData[4];

            delta_U.Text = trainData[5];
            U.Text = trainData[6];
            Y.Text = trainData[7];
            X.Text = trainData[8];
        }


    }
}
