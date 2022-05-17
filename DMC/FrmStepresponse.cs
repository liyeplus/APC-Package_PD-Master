using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;
using csharp_matlab;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;

namespace DMC
{
    public partial class FrmStepresponse : Form
    {
        public static Class1 step1 = new Class1();
        public FrmStepresponse()
        {
            InitializeComponent();
            InitChart();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MWArray b1 = double.Parse(textBox1.Text);
            MWArray b0 = double.Parse(textBox2.Text);
            MWArray a2 = double.Parse(textBox3.Text);
            MWArray a1 = double.Parse(textBox4.Text);
            MWArray a0 = double.Parse(textBox5.Text);
            MWArray tao = double.Parse(textBox6.Text);
            double[,] y = new double[1000, 1];//定义C#中接收输出参数的类型
            double[,] t = new double[1000, 1];//是两个二维数组
            int numArgsOut = 2;
            MWArray[] argsIn = new MWArray[] { b1, b0, a2, a1, a0, tao };
            MWArray[] argsOut = new MWArray[2];
            step1.stepresponse1(numArgsOut, ref argsOut, argsIn);
            MWNumericArray net_y = argsOut[0] as MWNumericArray;//matlab函数第一个输出参数
            MWNumericArray net_t = argsOut[1] as MWNumericArray;//第二个输出参数

            //转换成C#中的接收参数
            y = (double[,])net_y.ToArray();//转化为二维数组
            t = (double[,])net_t.ToArray();

            //X的最大值
            double xmax = 0;
            double ymax = 0;
            double ymin = 0;
            for (int i = 0; i < t.GetLength(0); i = i + 1)
            {
                if (t[i, 0] > xmax)
                {
                    xmax = t[i, 0];
                }
            }
            xmax = Math.Floor(xmax + 5);
            this.chart1.ChartAreas[0].AxisX.Maximum = xmax;
            this.chart1.ChartAreas[0].AxisX.Interval = Math.Floor(xmax / 10);

            //Y的最大值
            for (int i = 0; i < y.GetLength(0); i = i + 1)
            {
                if (y[i, 0] > ymax)
                {
                    ymax = y[i, 0];
                }
            }
            //Y的最小值
            for (int i = 0; i < y.GetLength(0); i = i + 1)
            {
                if (y[i, 0] < ymin)
                {
                    ymin = y[i, 0];
                }
            }

            //Y轴范围
            ymax = Math.Floor(ymax + 1);
            ymin = Math.Floor(ymin - 1);
            this.chart1.ChartAreas[0].AxisY.Maximum = ymax;
            this.chart1.ChartAreas[0].AxisY.Minimum = ymin;
            this.chart1.ChartAreas[0].AxisY.Interval = Math.Floor(ymax / 10);

            //画图
            this.chart1.Series[0].Points.Clear();
            for (int i = 0; i < y.GetLength(0); i = i + 1)
            {
                this.chart1.Series[0].Points.AddXY(t[i, 0], y[i, 0]);
            }
        }
        private void InitChart()
        {
            //定义图表区域
            this.chart1.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("C1");
            this.chart1.ChartAreas.Add(chartArea1);
            //定义存储和显示点的容器
            this.chart1.Series.Clear();
            Series series1 = new Series("响应曲线");
            series1.ChartArea = "C1";
            this.chart1.Series.Add(series1);
            //设置图表显示样式
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            this.chart1.ChartAreas[0].AxisX.Maximum = 20;
            this.chart1.ChartAreas[0].AxisX.Interval = 1;
            this.chart1.ChartAreas[0].AxisX.Title = "t/s";
            this.chart1.ChartAreas[0].AxisY.Minimum = -0.1; ;
            this.chart1.ChartAreas[0].AxisY.Maximum = 1;
            this.chart1.ChartAreas[0].AxisY.Interval = 0.1;
            this.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            this.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //设置标题
            this.chart1.Titles.Clear();
            this.chart1.Series[0].Color = Color.Blue;
            this.chart1.Series[0].BorderWidth = 2;
            this.chart1.Series[0].ChartType = SeriesChartType.Spline;
            this.chart1.Series[0].Points.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
        }
    }
}
