using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using csharp_matlab;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
namespace DMC
{
    public partial class FrmGstods : Form
    {
        public static Class2 lisan = new Class2();
        public FrmGstods()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //nu
            double[] nu = new double[1];
            string[] nu_text = this.Nu.Text.Trim().Split(';', ' ');//提取数
            for (int i = 0; i < nu_text.Length; i++)
            {
                nu[i] = Convert.ToDouble(nu_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_nu = new MWNumericArray(1, 1, nu);

            //nd
            double[] nd = new double[1];
            string[] nd_text = this.Nd.Text.Trim().Split(';', ' ');//提取数
            for (int i = 0; i < nd_text.Length; i++)
            {
                nd[i] = Convert.ToDouble(nd_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_nd = new MWNumericArray(1, 1, nd);

            //ny
            double[] ny = new double[1];
            string[] ny_text = this.Ny.Text.Trim().Split(';', ' ');//提取数
            for (int i = 0; i < ny_text.Length; i++)
            {
                ny[i] = Convert.ToDouble(ny_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_ny = new MWNumericArray(1, 1, ny);

            int nu_num = int.Parse(this.Nu.Text);
            int nd_num = int.Parse(this.Nd.Text);
            int ny_num = int.Parse(this.Ny.Text);

            //b1
            double[] b1 = new double[ny_num * (nu_num + nd_num)];
            string[] b1_text = this.b1.Text.Trim().Split(';', ' ');//提取数                   
            for (int i = 0; i < b1_text.Length; i++)
            {
                b1[i] = Convert.ToDouble(b1_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_b1 = new MWNumericArray(ny_num, nu_num + nd_num, b1);

            //b0
            double[] b0 = new double[ny_num * (nu_num + nd_num)];
            string[] b0_text = this.b0.Text.Trim().Split(';', ' ');//提取数                   
            for (int i = 0; i < b0_text.Length; i++)
            {
                b0[i] = Convert.ToDouble(b0_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_b0 = new MWNumericArray(ny_num, nu_num + nd_num, b0);

            //a2
            double[] a2 = new double[ny_num * (nu_num + nd_num)];
            string[] a2_text = this.a2.Text.Trim().Split(';', ' ');//提取数                   
            for (int i = 0; i < a2_text.Length; i++)
            {
                a2[i] = Convert.ToDouble(a2_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_a2 = new MWNumericArray(ny_num, nu_num + nd_num, a2);

            //a1
            double[] a1 = new double[ny_num * (nu_num + nd_num)];
            string[] a1_text = this.a1.Text.Trim().Split(';', ' ');//提取数                   
            for (int i = 0; i < a1_text.Length; i++)
            {
                a1[i] = Convert.ToDouble(a1_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_a1 = new MWNumericArray(ny_num, nu_num + nd_num, a1);

            //a0
            double[] a0 = new double[ny_num * (nu_num + nd_num)];
            string[] a0_text = this.a0.Text.Trim().Split(';', ' ');//提取数
            for (int i = 0; i < a0_text.Length; i++)
            {
                a0[i] = Convert.ToDouble(a0_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_a0 = new MWNumericArray(ny_num, nu_num + nd_num, a0);

            //tao
            double[] tao = new double[ny_num * (nu_num + nd_num)];
            string[] tao_text = this.tao.Text.Trim().Split(';', ' ');//提取数               
            for (int i = 0; i < b1_text.Length; i++)
            {
                tao[i] = Convert.ToDouble(tao_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_tao = new MWNumericArray(ny_num, nu_num + nd_num, tao);

            //T
            double[] T = new double[1];
            string[] T_text = this.T.Text.Trim().Split(';', ' ');//提取数
            for (int i = 0; i < T_text.Length; i++)
            {
                T[i] = Convert.ToDouble(T_text[i]);
            }
            //把输入参数都转换成中间类型，中间类型也是矩阵所以要指明维数
            MWNumericArray matlab_T = new MWNumericArray(1, 1, T);

            //定义C#中接收输出参数的类型，是五个二维数组
            double[,] G = new double[1000, 1000]; //for state_A
            double[,] Bb = new double[1000, 1000];//for state_Bb
            double[,] Bd = new double[1000, 1000];//for state_Bd
            double[,] C = new double[1000, 1000]; //for state_C
            double[,] D = new double[1000, 1000]; //for state_D

            int numArgsOut = 5;
            //输入参数转化为MWArray元素类型
            MWArray[] argsIn = new MWArray[] { matlab_nu, matlab_nd, matlab_ny, matlab_b1, matlab_b0, matlab_a2, matlab_a1, matlab_a0, matlab_tao, matlab_T };
            //声明输出参数是5个MWArray元素类型
            MWArray[] argsOut = new MWArray[5];
            lisan.gs2ds(numArgsOut, ref argsOut, argsIn);//调用混编函数

            //把两个输出参数转换成中间类型
            MWNumericArray net_G = argsOut[0] as MWNumericArray;//matlab函数第一个输出参数
            MWNumericArray net_Bb = argsOut[1] as MWNumericArray;//第二个输出参数
            MWNumericArray net_Bd = argsOut[2] as MWNumericArray;//第三
            MWNumericArray net_C = argsOut[3] as MWNumericArray;//第四
            MWNumericArray net_D = argsOut[4] as MWNumericArray;//第五

            //转换成C#中的接收参数
            //转换成C#中的接收参数
            G = (double[,])net_G.ToArray();//转化为二维数组
                                           //二维数组用ToArray()函数转换
                                           //一维数组用ToVector(MWArrayComponent.Real)函数转换
                                           //单个double值用ToScalarDouble()函数转换
                                           //单个int值用ToScalarInteger()函数转换
            Bb = (double[,])net_Bb.ToArray();
            Bd = (double[,])net_Bd.ToArray();
            C = (double[,])net_C.ToArray();
            D = (double[,])net_D.ToArray();

            //打印stare_A,即G
            this.A.Text = "";
            for (int i = 0; i < G.GetLength(0); i++)
            {
                this.A.Text = this.A.Text + Math.Round(G[i, 0], 5).ToString();
                for (int j = 1; j < G.GetLength(1); j++)
                {
                    this.A.Text = this.A.Text + " " + Math.Round(G[i, j], 5).ToString();
                }
                if (i < (G.GetLength(0) - 1))
                {
                    this.A.Text = this.A.Text + ";";
                }
            }
            this.A.Text = "[" + this.A.Text + "]";

            //打印state_Bb
            this.Bb.Text = "";
            for (int i = 0; i < Bb.GetLength(0); i++)
            {
                this.Bb.Text = this.Bb.Text + Math.Round(Bb[i, 0], 5).ToString();
                for (int j = 1; j < Bb.GetLength(1); j++)
                {
                    this.Bb.Text = this.Bb.Text + " " + Math.Round(Bb[i, j], 5).ToString();
                }
                if (i < (Bb.GetLength(0) - 1))
                {
                    this.Bb.Text = this.Bb.Text + ";";
                }
            }
            this.Bb.Text = "[" + this.Bb.Text + "]";

            //打印state_Bd
            this.Bd.Text = "";
            for (int i = 0; i < Bd.GetLength(0); i++)
            {
                this.Bd.Text = this.Bd.Text + Math.Round(Bd[i, 0], 5).ToString();
                for (int j = 1; j < Bd.GetLength(1); j++)
                {
                    this.Bd.Text = this.Bd.Text + " " + Math.Round(Bd[i, j], 5).ToString();
                }
                if (i < (Bd.GetLength(0) - 1))
                {
                    this.Bd.Text = this.Bd.Text + ";";
                }
            }
            this.Bd.Text = "[" + this.Bd.Text + "]";

            //打印state_C
            this.C.Text = "";
            for (int i = 0; i < C.GetLength(0); i++)
            {
                this.C.Text = this.C.Text + Math.Round(C[i, 0], 5).ToString();
                for (int j = 1; j < C.GetLength(1); j++)
                {
                    this.C.Text = this.C.Text + " " + Math.Round(C[i, j], 5).ToString();
                }
                if (i < (C.GetLength(0) - 1))
                {
                    this.C.Text = this.C.Text + ";";
                }
            }
            this.C.Text = "[" + this.C.Text + "]";

            //打印state_D
            this.D.Text = "";
            for (int i = 0; i < D.GetLength(0); i++)
            {
                this.D.Text = this.D.Text + Math.Round(D[i, 0], 5).ToString();
                for (int j = 1; j < D.GetLength(1); j++)
                {
                    this.D.Text = this.D.Text + " " + Math.Round(D[i, j], 5).ToString();
                }
                if (i < (D.GetLength(0) - 1))
                {
                    this.D.Text = this.D.Text + ";";
                }
            }
            this.D.Text = "[" + this.D.Text + "]";

            //显示到FrmInit
            FrmInit a = (FrmInit)this.Owner;
            a.state_A.Text = this.A.Text;
            a.state_Bb.Text = this.Bb.Text;
            a.state_Bd.Text = this.Bd.Text;
            a.state_C.Text = this.C.Text;
            a.state_D.Text = this.D.Text;

            a.delt1.Text = this.T.Text;
            a.nu.Text = this.Nu.Text;
            a.ny.Text = this.Ny.Text;
            a.nd.Text = this.Nd.Text;
        }

        private void FrmGstods_Load(object sender, EventArgs e)
        {
            Nu.Text = "2";
            Nd.Text = "1";
            Ny.Text = "2";
            b1.Text = "0 0 0;0 0 0";
            b0.Text = "1 1 0;1 1 0";
            a2.Text = "0 0 1;0 0 1";
            a1.Text = "1790.6 17602.3 0;76.9 383.8 0";
            a0.Text = "68.5 208.6 0;3.2 8.6 0";
            tao.Text = "1.4 68.1 0;1 24.2 0";
            T.Text = "20";

        }
    }
}
