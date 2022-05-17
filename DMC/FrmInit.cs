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
    public partial class FrmInit : Form     //窗体FrmInit类，用于初始化控制器参数，将参数传递到FrmMain类中，FrmInit关闭时，缓存参数将丢失
    {
        //状态方程 x(k+1) = A x(k) + Bb u + Bd d
        //输出方程 y(k) = C x(k) + D {u}
        //                           {d}
        Matrix A;       //定义Matrix类的矩阵字段A（类中变量称为字段），同下
        public Matrix aMatrix { get { return A; } set { A = value; } }
        Matrix Bb;
        public Matrix bbMatrix { get { return Bb; } set { Bb = value; } }
        Matrix Bd;
        public Matrix bdMatrix { get { return Bd; } set { Bd = value; } }
        Matrix C;
        public Matrix cMatrix { get { return C; } set { C = value; } }
        Matrix D;
        public Matrix dMatrix { get { return D; } set { D = value; } }
        Matrix B;       //横向拼接Bb Bd得到B，B一般是列向量
        public Matrix bMatrix { get { return B; } set { B = value; } }
        
        bool OK;        //定义字段布尔变量OK
        public bool isOK { get { return OK; } }     //定义方法isOK，返回OK的布尔值
        
        double Delt;        //采样周期
        public double delt { get { return Delt; } set { Delt = value; } }
        int Nu;     //输入MV个数
        public int nuValue { get { return Nu; } set { Nu = value; } }
        int Ny;     //输出稳定性向量 输出CV个数  
        public int nyValue { get { return Ny; } set { Ny = value; } }
        int Nd;     //干扰DV个数
        public int ndValue { get { return Nd; } set { Nd = value; } }
        int P;      // prediction horizon 预测范围
        public int pValue { get { return P; } set { P = value; } }
        int M;      // control horizon 控制水平
        public int mValue { get { return M; } set { M = value; } }
        int N;      // N = tfinal/delt;
        public int nValue { get { return N; } set { N = value; } }

        double[] r;
        public double[] r_Value { get { return r; } }

        Matrix dumax;       //定义Matrix类的矩阵字段dumax，存放最大最小值。同下
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

        string S_Line;
        public string SetpointLine { get { return S_Line; } }
        string L_Line;
        public string LimitLine { get { return L_Line; } }
        
        public FrmInit()
        {    
            InitializeComponent();    
            
        }

        private void Setting_Click(object sender, EventArgs e)      //OK按钮
        {
            A = new Matrix(GetState(state_A.Text));
            Bb = new Matrix(GetState(state_Bb.Text));
            Bd = new Matrix(GetState(state_Bd.Text));
            C = new Matrix(GetState(state_C.Text));
            D = new Matrix(GetState(state_D.Text));
            B = GetState_B();

            Delt = Convert.ToDouble(delt1.Text);
            Nu = Convert.ToInt16(nu.Text);
            Ny = Convert.ToInt32(ny.Text);
            Nd = Convert.ToInt16(nd.Text);
            P = Convert.ToInt16(p.Text);
            M = Convert.ToInt16(m.Text);
            N = 50;        // Tfinal / Delt;
            this.SetPoint();
            this.Limit();
            OK = true;      //将OK设置为ture
            this.Close();
        }

        private double[,] GetState(string a)        //猜测是将输入的TXT文件，转换成ss矩阵，并将ss返回幅值
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
            return ss;
        }

        private Matrix GetState_B()     //横向拼接矩阵Bb和Bd得到矩阵B
        {
            Matrix B = Matrix.Combine(Bb, Bd, Matrix.Direction.horizontal);     
            return B;
        }

        private void Cancel_Click(object sender, EventArgs e)       ///取消按钮
        {
            OK = false;
            this.Close();//关闭窗口
        }
        
        private void SeeA_Click(object sender, EventArgs e)     //查看A矩阵
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = new Matrix(GetState(state_A.Text));
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeBb_Click(object sender, EventArgs e)        //查看Bb列向量
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = new Matrix(GetState(state_Bb.Text));
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeBd_Click(object sender, EventArgs e)        //查看Bd列向量
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = new Matrix(GetState(state_Bd.Text));
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeC_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = new Matrix(GetState(state_C.Text));
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }

        private void SeeD_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = new Matrix(GetState(state_D.Text));
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.ShowDialog();
        }
        #region
        private void InsertABCD_Click(object sender, EventArgs e)       //Auto按钮
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
            StreamReader sr = new StreamReader(fs);     //从文件中读取数据的类SteramReader
            while (!sr.EndOfStream)     //当EndOfStream=ture时停止循环
            {
                string s = sr.ReadLine();       //按行读取，每次将一行内容保存到s字符串
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
                trainData.Add(s);       //将s字符串，添加到List类对象trainDate的结尾
                //trainSampleNumber++;
            }   //将输入的训练数据加入数组
            sr.Close();
            fs.Close();

            state_A.Text = trainData[0];        //将List类对象trainDate的每一行，赋值给各个行向量
            state_Bb.Text = trainData[1];
            state_Bd.Text = trainData[2];
            state_C.Text = trainData[3];
            state_D.Text = trainData[4];
            setpoint.Text = trainData[5];
            limit.Text = trainData[6];
        }
        #endregion
        private void SetPoint()     //设定值
        {
            string Line = this.setpoint.Text;
            S_Line = Line;

            string[] s = this.setpoint.Text.Trim("[ ]".ToCharArray()).Split(';');
            int lens = s.Count();
            if (lens == Ny)
            {
                r = new double[lens];
                for (int i = 0; i < lens; i++)
                {
                    r[i] = Convert.ToDouble(s[i]);
                }
            }
            else
            {
                MessageBox.Show("Dimension error of controlled variable.");
            }
        }

        private void Limit()        //限值
        {
            string Line = this.limit.Text;
            L_Line = Line; 

            string[] s = Line.Trim("[ ]".ToCharArray()).Split(';');

            dumax = new Matrix(Nu, 1);
            dumin = new Matrix(Nu, 1);
            umax = new Matrix(Nu, 1);
            umin = new Matrix(Nu, 1);
            ymax = new Matrix(Ny, 1);
            ymin = new Matrix(Ny, 1);
            //dumax
            string[] SSS = s[0].Split(' ');
            int lens = SSS.Count();
            if (lens != Nu)
            {
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
            if (lens != Nu)
            {
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
            if (lens != Nu)
            {
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
            if (lens != Nu)
            {
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
            if (lens != Ny)
            {
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
            if (lens != Ny)
            {
                MessageBox.Show("Constraint error in Ymin .");
                return;
            }
            for (int i = 0; i < lens; i++)
            {
                ymin[i, 0] = Convert.ToDouble(SSS[i]);
            }
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmStepresponse sr = new FrmStepresponse();
            sr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGstods a = new FrmGstods();
            a.Show(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //约束条件格式为[△U1max △U2max;△U1min △U2min;U1max U2max;U1min U2min;Y1max  Y2max;Y1min Y2min]
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = true;
                checkBox5.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = true;
            }
        }

        private void FrmInit_Load(object sender, EventArgs e)
        {
            cobModels.SelectedIndex = 0;
        }

        private void cobModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            int model_ID = cobModels.SelectedIndex+2;
            if (model_ID == 1)
            {         
                state_A.Text = "[-0.03153 -0.03826 0 0 0 0 0 0;0.04171 0.04845 0 0 0 0 0 0;0 0 0.34513 0 0 0 0 0;0 0 0 0.6267 0 0 0 0;0 0 0 0 1E-05 0 0 0;0 0 0 0 0 0 0 0;0 0 0 0 0 0 0 0;0 0 0 0 0 0 0 0]";
                state_Bb.Text = "[0.00521 0;0.12965 0;0.19237 0;0.12482 0;0.211 0;0 0;0.03552 0;0 12.79995]";
                state_Bd.Text = "[0;0;0;0;0;0;0;0]";
                state_C.Text = "[0.09541 0.03963 0 0 0 0 0 0;0 0 0.04357 0 0 0 0 0;0 0 0 0.04935 0 0 0 0;0 0 0 0 0 0 0 -55.39062;0 0 0 0 0.47867 0 0 0;0 0 0 0 0 0 0.77421 0]";
                state_D.Text = "[0 0 0;0 0 0;0 0 0;0 0 0;0 0 0;0 0 0]";
                delt1.Text = "5";
                this.nu.Text = "2";
                this.ny.Text = "6";
                this.nd.Text = "1";
                this.setpoint.Text = "[470;1270;1270;70;600000;79.5]";
                this.limit.Text = "[800 200;-800 -200;125000 15625;27600 3450;520 1350 1350 100 800000 100;450 1100 1100 0 0 0]";
                //约束条件格式为[△U1max △U2max;△U1min △U2min;U1max U2max;U1min U2min;Y1max  Y2max;Y1min Y2min]
            }
            if (model_ID == 2)
            {
                state_A.Text = "[-0.03153 -0.03826 0 0 0 0 0 0;0.04171 0.04845 0 0 0 0 0 0;0 0 0.34513 0 0 0 0 0;0 0 0 0.6267 0 0 0 0;0 0 0 0 1E-05 0 0 0;0 0 0 0 0 0 0 0;0 0 0 0 0 0 0 0;0 0 0 0 0 0 0 0]";
                state_Bb.Text = "[0.00521 0;0.12965 0;0.19237 0;0.12482 0;0.211 0;0 0;0.00888 0;0 12.79995]";
                state_Bd.Text = "[0;0;0;0;0;0;0;0]";
                state_C.Text = "[0.09541 0.03963 0 0 0 0 0 0;0 0 0.04357 0 0 0 0 0;0 0 0 0.04935 0 0 0 0;0 0 0 0 0 0 0 -55.39062;0 0 0 0 0.47867 0 0 0;0 0 0 0 0 0 0.30968 0]";
                state_D.Text = "[0 0 0;0 0 0;0 0 0;0 0 0;0 0 0;0 0 0]";
                delt1.Text = "5";
                this.nu.Text = "2";
                this.ny.Text = "6";
                this.nd.Text = "1";
                this.setpoint.Text = "[470;1240;1270;70;600000;79.5]";
                this.limit.Text = "[800 200;-800 -200;125000 15625;27600 3450;520 1350 1350 200 800000 120;420 1100 1100 0 0 0]";
                //约束条件格式为[△U1max △U2max;△U1min △U2min;U1max U2max;U1min U2min;Y1max  Y2max;Y1min Y2min]
            }
        }
    }
}
