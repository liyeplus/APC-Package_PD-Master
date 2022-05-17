using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMatrix;
using OPC;
using OPCDA;
using OPC.Common;
using OPCDA.NET;
using System.Net;
using System.IO;
using System.Threading;
using System.Configuration;



namespace DMC       //DMC命名空间
{
    public partial class FrmMain : Form
    {
    
        /// <summary>
        /// The instantiation of simulation object and DMC-controller 初始化仿真对象和DMC控制器
        /// </summary>
        DMCAlgorithm DMCcontrol;        //创建DMCAlgorithm类的对象DMCcontrol
        FrmOPC OPCpanel;
        DMCtoOPC dmctoopc;      //创建DMCtoOPC类的对象dmctoopc
        FrmSetPoint SetPoint;
        FrmSetDisturb SetDisturb;
        FrmLimit Limit;
        VariableDMC DMCVar;         //创建VariableDMC类的对象DMCVar
        FrmInit newForm;        //创建FrmInit类的newForm对象

        int nu;     
        int ny;     
        int nd;
        int filteri = 0;//滤波计数

        double[] CV1nums = new double[5];//滤波存储数组
        double[] CV2nums = new double[5];
        double[] CV3nums = new double[5];
        double[] CV4nums = new double[5];
        double[] CV5nums = new double[5];
        double[] CV6nums = new double[5];

        bool CV1_flag;
        bool CV2_flag;
        bool CV3_flag;
        bool CV4_flag;
        bool CV5_flag;
        bool CV6_flag;
        struct VariableDMC      //struct是结构体，定义与class类似，struct是值类型，定义VariableDMC方法，作用未知
        {
            Matrix p1;      //系统Matrix矩阵类，声明p1,p2矩阵
            Matrix p2;
            double[] p3;
            public Matrix uMatrix { get { return p1; } set { p1 = value; } }        //u y输入输出是列向量，d是行向量
            public Matrix yMatrix { get { return p2; } set { p2 = value; } }
            public double[] dMatrix { get { return p3; } set { p3 = value; } }

            public VariableDMC(int nu, int ny, int nd)
            {
                p1 = Matrix.Zeros(nu, 1);       //将P1/P2转换成nu行1列的矩阵
                p2 = Matrix.Zeros(ny, 1);
                p3 = new double[nd];        //将P3转换成nd列1行的矩阵
            }

            public VariableDMC(Matrix U, Matrix Y, double[] D)
            {
                p1 = U;
                p2 = Y;
                p3 = D;
            }
        }

        public FrmMain()        //开始新的FrmMain窗口
        {
            InitializeComponent();
            //this.MaximizeBox = false;
            OPCpanel = new FrmOPC(lvRefreshItems);
        }

        public double filtering(double[] nums)
        {
           
                Array.Sort(nums);
                double result = nums[2];
                return result;
                      
        }
        private void filtertime_Tick(object sender, EventArgs e)//滤波
        {
            
            //try
            //{
            //    CV1nums[filteri] = readsetpoint("APC.CV1");
            //}
            //catch
            //{
              
            //}
            //try
            //{
            //    CV2nums[filteri] = readsetpoint("APC.CV2");
            //}
            //catch
            //{
                
            //}
            //try
            //{
            //    CV3nums[filteri] = readsetpoint("APC.CV3");
            //}
            //catch
            //{
                
            //}
            //try
            //{
            //    CV4nums[filteri] = readsetpoint("APC.CV4");
            //}
            //catch
            //{
                
            //}
            //try
            //{
            //    CV5nums[filteri] = readsetpoint("APC.CV5");
            //}
            //catch
            //{
                
            //}
            //try
            //{
            //    CV6nums[filteri] = readsetpoint("APC.CV6");
            //}
            //catch
            //{
                
            //}
            //filteri++;
            //if(filteri == 5)
            //{
            //    Array.Clear(CV1nums, 0, CV1nums.Length);
            //    Array.Clear(CV2nums, 0, CV2nums.Length);
            //    Array.Clear(CV3nums, 0, CV3nums.Length);
            //    Array.Clear(CV4nums, 0, CV4nums.Length);
            //    Array.Clear(CV5nums, 0, CV5nums.Length);
            //    Array.Clear(CV6nums, 0, CV6nums.Length);
            //    filteri = 0;
            //}
        }
        private void controltime_Tick(object sender, EventArgs e)        //timer1主要执行DMC运算、OPC读取、写入，其属性Interval是执行时间
        {
            DateTime dt = DateTime.Now;
            //x1:1420AI1206_CH4.PV
            //x2:1420AI1206_CO2.PV
            //x3:1420FFI1054_1.DACA.PV
            double x1, x2, x3;
            double a, b, c, d;
            double CV1, CV2, CV3, CV4, CV5, CV6;
            double MV1, MV2;
            double DV1;
            double CV1_MAX, CV1_MIN,CV2_MAX, CV2_MIN, CV3_MAX, CV3_MIN, CV5_MAX, CV5_MIN, CV6_MAX, CV6_MIN;
            bool CV1_startandpause, CV2_startandpause, CV3_startandpause, CV4_startandpause, CV5_startandpause, CV6_startandpause;
            CV1_startandpause = checkBox1.Checked;
            CV2_startandpause = checkBox2.Checked;
            CV3_startandpause = checkBox3.Checked;
            CV4_startandpause = checkBox4.Checked;
            CV5_startandpause = checkBox5.Checked;
            CV6_startandpause = checkBox6.Checked;
            

            x1 = readsetpoint("1420AI1206_CH4.PV");
            x2 = readsetpoint("1420AI1206_CO2.PV");
            x3 = readsetpoint("1420FFI1068.DACA.PV");

            a= readsetpoint("1420TI_1080.DACA.PV");
            b= readsetpoint("1420TI_1081.DACA.PV");
            c= readsetpoint("1420TI_1084.DACA.PV");
            d= readsetpoint("1420TI_1085.DACA.PV");

            CV1 = readsetpoint("1420FFI1068.DACA.PV");
            CV2 = softcal.Calmed(a, b, c,d);
            CV3 = softcal.CalTem(x1, x2, x3);
            //CV4 = readsetpoint("1420PDI4087.DACA.PV");
            CV4 = 70;
            CV5 = readsetpoint("1420FI1203.DACA.PV");
            CV6=  readsetpoint("1420AI1206_CO.PV") + readsetpoint("1420AI1206_H2.PV");

            MV1 = readsetpoint("1420FIC1054.PIDA.SP");
            MV2 = readsetpoint("1420FIC1067.PIDA.SP");
            DV1 = readsetpoint("1420FC1016.DACA.PV");

            if ((double.IsNaN(x1)) || (double.IsNaN(x2)) || (double.IsNaN(x3)) || (double.IsNaN(a)) || (double.IsNaN(b)) || (double.IsNaN(c)) || (double.IsNaN(d)) || (double.IsNaN(CV1)) || (double.IsNaN(CV4)) || (double.IsNaN(CV5)) || (double.IsNaN(CV6)) || (double.IsNaN(MV1)) || (double.IsNaN(MV2)))
            {
                if ((double.IsNaN(MV1)) || (double.IsNaN(MV2)))
                {                 
                    string str5 = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                    string loc5 = System.Configuration.ConfigurationManager.AppSettings["Loc"];
                    string mix5 = "读取到非数字包括氧回路";
                    WriteTxt.WriteToTxt(mix5, loc5, "MV实际+MV计算+CV实际+DV实际+CV预测_" + str5 + ".txt");
                }
                else
                {
                    string str6 = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                    string loc6 = System.Configuration.ConfigurationManager.AppSettings["Loc"];
                    string mix6 = MV1.ToString("f1") + " " + MV2.ToString("f1") + " " + MV1.ToString("f1") + " " + MV2.ToString("f1") + " " + "读取到非数字";
                    WriteTxt.WriteToTxt(mix6, loc6, "MV实际+MV计算+CV实际+DV实际+CV预测_" + str6 + ".txt");
                }
            }
            else
            {
                DMCVar.yMatrix[0, 0] = CV1;
                DMCVar.yMatrix[1, 0] = CV2;
                DMCVar.yMatrix[2, 0] = CV3;
                DMCVar.yMatrix[3, 0] = CV4;
                DMCVar.yMatrix[4, 0] = CV5;
                DMCVar.yMatrix[5, 0] = CV6;

                DMCVar.uMatrix[0, 0] = MV1;
                DMCVar.uMatrix[1, 0] = MV2;

                DMCVar.dMatrix[0] = DV1;

                for (int j = 0; j < ny; j++)
                {
                    listView1.Items[(2 + j)].SubItems[1].Text = DMCVar.yMatrix[j, 0].ToString("f1");
                }

                for (int j = 0; j < nd; j++)
                {
                    listView1.Items[(8 + j)].SubItems[1].Text = DMCVar.dMatrix[j].ToString("f1");
                }
                CV1_MAX = Convert.ToDouble(listView1.Items[2].SubItems[8].Text);
                CV1_MIN = Convert.ToDouble(listView1.Items[2].SubItems[9].Text);
                CV2_MAX = Convert.ToDouble(listView1.Items[3].SubItems[8].Text);
                CV2_MIN = Convert.ToDouble(listView1.Items[3].SubItems[9].Text);
                CV3_MAX = Convert.ToDouble(listView1.Items[4].SubItems[8].Text);
                CV3_MIN = Convert.ToDouble(listView1.Items[4].SubItems[9].Text);
                CV5_MAX = Convert.ToDouble(listView1.Items[6].SubItems[8].Text);
                CV5_MIN = Convert.ToDouble(listView1.Items[6].SubItems[9].Text);
                CV6_MAX = Convert.ToDouble(listView1.Items[7].SubItems[8].Text);
                CV6_MIN = Convert.ToDouble(listView1.Items[7].SubItems[9].Text);

                if ((CV1 < CV1_MAX) && (CV1 > CV1_MIN) || CV1_startandpause == false)//控制的上下限
                {
                    CV1_flag = true;
                }
                else
                {
                    CV1_flag = false;
                }
                if ((CV2 < CV2_MAX) && (CV2 > CV2_MIN) || CV2_startandpause == false)
                {
                    CV2_flag = true;
                }
                else
                {
                    CV2_flag = false;
                }
                if ((CV3 < CV3_MAX) && (CV3 > CV3_MIN) || CV3_startandpause == false)
                {
                    CV3_flag = true;
                }
                else
                {
                    CV3_flag = false;
                }
                if (CV4 < 90)
                {
                    CV4_flag = true;
                }
                else
                {
                    CV4_flag = false;
                }
                if ((CV5 < CV5_MAX) && (CV5 > CV5_MIN) || CV5_startandpause == false)
                {
                    CV5_flag = true;
                }
                else
                {
                    CV5_flag = false;
                }
                if ((CV6 < CV6_MAX) && (CV6 > CV6_MIN) || CV6_startandpause == false)
                {
                    CV6_flag = true;
                }
                else
                {
                    CV6_flag = false;
                }

                DMCcontrol.Control_All(DMCVar.uMatrix, DMCVar.yMatrix, DMCVar.dMatrix, CV1_flag, CV2_flag, CV3_flag, CV4_flag, CV5_flag, CV6_flag);     //调用DMCAlgorithm类的方法，将参数传递给DMCAlgorithm类

                if (CV4 < 90)
                {

                    string str1 = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                    string loc1 = System.Configuration.ConfigurationManager.AppSettings["Loc"];
                    string mix1 = MV1.ToString("f1") + " " + MV2.ToString("f1") + " " + DMCcontrol.u_CMatrix[0, 0].ToString("f1") + " " + (DMCcontrol.u_CMatrix[0, 0] * MV2 / MV1).ToString("f1") +
                        " " + CV1.ToString("f1") + " " + CV2.ToString("f1") + " " + CV3.ToString("f1") + " " + CV4.ToString("f1") + " " + CV5.ToString("f1") + " " + CV6.ToString("f1") +
                        " " + DV1.ToString("f1") + " " +
                    DMCcontrol.Y_pre[294, 0].ToString("f1") + " " + DMCcontrol.Y_pre[295, 0].ToString("f1") + " " + DMCcontrol.Y_pre[296, 0].ToString("f1") + " " + DMCcontrol.Y_pre[297, 0].ToString("f1") + " " + DMCcontrol.Y_pre[298, 0].ToString("f1") + " " + DMCcontrol.Y_pre[299, 0].ToString("f1");
                    WriteTxt.WriteToTxt(mix1, loc1, "MV实际+MV计算+CV实际+DV实际+CV预测_" + str1 + ".txt");
                    DMCVar.uMatrix[0, 0] = DMCcontrol.u_CMatrix[0, 0];
                    DMCVar.uMatrix[1, 0] = DMCcontrol.u_CMatrix[0, 0] * MV2 / MV1;

                    listView1.Items[0].SubItems[1].Text = MV1.ToString("f1");
                    listView1.Items[1].SubItems[1].Text = MV2.ToString("f1");
                    listView1.Items[0].SubItems[3].Text = DMCVar.uMatrix[0, 0].ToString("f1");
                    listView1.Items[1].SubItems[3].Text = (DMCVar.uMatrix[0, 0] * MV2 / MV1).ToString("f1");


                }
                else
                {

                    string str2 = DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString();
                    string loc2 = System.Configuration.ConfigurationManager.AppSettings["Loc"];
                    string mix2 = MV1.ToString("f1") + " " + MV2.ToString("f1") + " " + DMCcontrol.u_CMatrix[0, 0].ToString("f1") + " " + DMCcontrol.u_CMatrix[1, 0].ToString("f1") +
                        " " + CV1.ToString("f1") + " " + CV2.ToString("f1") + " " + CV3.ToString("f1") + " " + CV4.ToString("f1") + " " + CV5.ToString("f1") + " " + CV6.ToString("f1") +
                        " " + DV1.ToString("f1") + " " +
                        DMCcontrol.Y_pre[294, 0].ToString("f1") + " " + DMCcontrol.Y_pre[295, 0].ToString("f1") + " " + DMCcontrol.Y_pre[296, 0].ToString("f1") + " " + DMCcontrol.Y_pre[297, 0].ToString("f1") + " " + DMCcontrol.Y_pre[298, 0].ToString("f1") + " " + DMCcontrol.Y_pre[299, 0].ToString("f1");

                    WriteTxt.WriteToTxt(mix2, loc2, "MV实际+MV计算+CV实际+DV实际+CV预测_" + str2 + ".txt");
                    DMCVar.uMatrix[0, 0] = DMCcontrol.u_CMatrix[0, 0];
                    DMCVar.uMatrix[1, 0] = DMCcontrol.u_CMatrix[1, 0];
                    listView1.Items[0].SubItems[1].Text = MV1.ToString("f1");
                    listView1.Items[1].SubItems[1].Text = MV2.ToString("f1");
                    for (int j = 0; j < nu; j++)
                    {
                        listView1.Items[j].SubItems[3].Text = DMCVar.uMatrix[j, 0].ToString("f1");
                    }

                }


                if (DMCcontrol.PauseFlag)       //调用PauseFlag方法得到AutoPause的bool值，值为T则进入if语句，使得timer1停止，即控制器停止
                {
                    //controltime.Enabled = false;
                    //filtertime.Enabled = false;
                    //label3.BackColor = Color.Red;
                    //label3.Text = "停止";
                    //startDMCtoolStripMenuItem.Enabled = true;         //Start按钮变为可用
                    //pauseDCMToolStripMenuItem.Enabled = false;       //Pause按钮变为不可用  
                    /*MessageBox.Show("Optimal value calculation is out of time.");*/ //输出最优值计算超出时间 
                }

                else
                {

                }

            }
        }

        private void opclisttime_Tick(object sender, EventArgs e)        //timer2刷新图，表
        {
           
        }

        private void iNToolStripMenuItem_Click(object sender, EventArgs e)      //初始化
        {

            opclisttime.Enabled = false;
            this.listView1.Items.Clear();
            newForm = new FrmInit();        //初始化newForm对象
            newForm.ShowDialog();        
            //timer1.Interval = Convert.ToInt32(newForm.delt * 1000);
            //timer2.Interval = Convert.ToInt32(newForm.delt * 1000);
            if (newForm.isOK)       //调用isOK方法，返回OK值是ture
            {
                try
                {       
                    DMCcontrol = new DMCAlgorithm(newForm.delt, newForm.nuValue, newForm.ndValue, newForm.nyValue, newForm.pValue, newForm.mValue, newForm.nValue,
                                                    newForm.aMatrix, newForm.bMatrix, newForm.cMatrix, newForm.dMatrix, newForm.bbMatrix, newForm.bdMatrix);//初始化变量
                    DMCcontrol.constraint();
                    DMCcontrol.SetPoint(newForm.r_Value);
                    DMCcontrol.SetLimit(newForm.dumax_Value.Data, newForm.dumin_Value.Data, newForm.umax_Value.Data, newForm.umin_Value.Data, newForm.ymax_Value.Data, newForm.ymin_Value.Data);
                    nu = newForm.nuValue;       //赋值
                    ny = newForm.nyValue;
                    nd = newForm.ndValue;
                    Matrix tt = Matrix.Zeros(nu + ny, 4);       //nu+ny是MV+CV个数，tt是 N*4维矩阵，矩阵内元素全为0
                    tt = ProStringToMatrix(newForm.dumax_Value.Data, newForm.dumin_Value.Data, newForm.umax_Value.Data,newForm.umin_Value.Data, newForm.ymax_Value.Data, newForm.ymin_Value.Data);
                            //给tt矩阵幅值
                    DMCVar = new VariableDMC(nu, ny, nd);       //初始化DMCVar变量          
                    //----------------------------------------------------------------------------------------------------------------列表初始化
                    for (int j = 0; j < newForm.nuValue; j++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = 0;
                        lvi.Text = "MV" + Convert.ToString(j + 1);
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        this.listView1.Items.Add(lvi);
                    }
                    //--------------------------------------------------------------------------------------------------------------------------
                    for (int j = 0; j < newForm.nyValue; j++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = 1;
                        lvi.Text = "CV" + Convert.ToString(j + 1);
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        this.listView1.Items.Add(lvi);
                    }
                    //--------------------------------------------------------------------------------------------------------------------------
                    for (int j = 0; j < newForm.ndValue; j++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.ImageIndex = 2;
                        lvi.Text = "DV" + Convert.ToString(j + 1);
                        lvi.SubItems.Add("0");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");
                        lvi.SubItems.Add("###");                       
                        this.listView1.Items.Add(lvi);
                    }
                    //--------------------------------------------------------------------------------------------------------------------------
                    for (int i = 0; i < (nu + ny); i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            listView1.Items[i].SubItems[j + 4].Text = tt[i, j].ToString("f3");
                            if ((i >= nu) && (j >= 2))
                            {
                                listView1.Items[i].SubItems[j + 4].Text = "###";
                            }
                        }
                    }
                    //--------------------------------------------------------------------------------------------------------------------------
                    for (int i = 0; i < ny; i++)
                    {
                        listView1.Items[i + nu].SubItems[3].Text = newForm.r_Value[i].ToString("f3");
                    }
                    listView1.Items[2].SubItems[8].Text = (newForm.r_Value[0] + 5).ToString("f3");
                    listView1.Items[2].SubItems[9].Text = (newForm.r_Value[0] - 5).ToString("f3");
                    listView1.Items[3].SubItems[8].Text = (newForm.r_Value[1] + 20).ToString("f3");
                    listView1.Items[3].SubItems[9].Text = (newForm.r_Value[1] - 20).ToString("f3");
                    listView1.Items[4].SubItems[8].Text = (newForm.r_Value[2] + 20).ToString("f3");
                    listView1.Items[4].SubItems[9].Text = (newForm.r_Value[2] - 20).ToString("f3");
                    listView1.Items[5].SubItems[8].Text = "90";
                    listView1.Items[6].SubItems[8].Text = (newForm.r_Value[4] + 30000).ToString("f3");
                    listView1.Items[6].SubItems[9].Text = (newForm.r_Value[4] - 30000).ToString("f3");
                    listView1.Items[7].SubItems[8].Text = (newForm.r_Value[5] + 0.8).ToString("f3");
                    listView1.Items[7].SubItems[9].Text = (newForm.r_Value[5] - 0.8).ToString("f3");
                    //--------------------------------------------------------------------------------------------------------------------------
                    //iNToolStripMenuItem.Enabled = false;        //置初始化按钮不可用
                    cToolStripMenuItem.Enabled = true;      //置connet＆config按钮可用
                    resetToolStripMenuItem.Enabled = true;
                    SetPoint = new FrmSetPoint(this.ny, newForm.SetpointLine, DMCcontrol.Umax, DMCcontrol.Umin);      //初始化
                    SetDisturb = new FrmSetDisturb(this.nd);
                    Limit = new FrmLimit(this.nu,this.ny,this.nd, newForm.LimitLine);
                    dmctoopc = new DMCtoOPC(nu, ny, nd, listView1);     //首先运行的是主窗体FrmMain类，因此将初始化后得到的CVMVDV个数及列表内容，传到DMCtoOPC类中
                }
                catch (Exception ex)        //捕获异常对象
                {
                    MessageBox.Show(ex.Message);        //Display exception information
                }
            }
            else
            { }

        }

        private void Running_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            opclisttime.Enabled = true;
            startDMCtoolStripMenuItem.Enabled = true;
            Running.Enabled = false;
            startandpause.Enabled = true;
        }

        private void startDMCtoolStripMenuItem_Click(object sender, EventArgs e)       //start
        {
            controltime.Enabled = true;                            //点击Start按钮，timer1开始运行，控制器开始输出
            filtertime.Enabled = true;
            output("DMC控制--开始");
            startDMCtoolStripMenuItem.Enabled = false;         //Start按钮变为不可用
            pauseDCMToolStripMenuItem.Enabled = true;         //Pause按钮变为可用  
            label3.BackColor = Color.Green;
            label3.Text = "投用";

        }

        private void pauseDCMToolStripMenuItem_Click(object sender, EventArgs e)        //Pause
        {
            controltime.Enabled = false;                          //点击Pause按钮，timer1停止运行，控制器开始输出
            filtertime.Enabled = false;
            output("DMC控制--暂停");
            DMCcontrol.InitFlag = true;     //原来暂停逻辑里有此语句
            startDMCtoolStripMenuItem.Enabled = true;         //Start按钮变为可用
            pauseDCMToolStripMenuItem.Enabled = false;       //Pause按钮变为不可用  
            label3.BackColor = Color.Red;
            label3.Text = "停止";

        }
        
        private void oPCClientToolStripMenuItem_Click(object sender, EventArgs e)       //OPC Client
        {
            OPCpanel.ShowDialog();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)       //Connet and Config
        {
             
            dmctoopc.ShowDialog();
            if (dmctoopc.isConnectOK)
            {
                Running.Enabled = true;
            }
        }
        

        private void gammayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = DMCcontrol.yGammaValue;
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.nuValue = this.nu;
            newForm.nyValue = this.ny;
            newForm.FValue = 1;
            newForm.ShowDialog();
            DMCcontrol.yGammaValue = newForm.TaMatrix;
            DMCcontrol.ResetGamma();
        }

        private void gammauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = DMCcontrol.uGammaValue;
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.nuValue = this.nu;
            newForm.nyValue = this.ny;
            newForm.FValue = 2;
            newForm.ShowDialog();
            DMCcontrol.uGammaValue = newForm.TaMatrix;
            DMCcontrol.ResetGamma();
        }
        
        private void kFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMatrix newForm = new FrmMatrix();
            newForm.abcdMatrix = DMCcontrol.KFValue;
            newForm.xValue = Convert.ToInt16(newForm.abcdMatrix.rows);
            newForm.yValue = Convert.ToInt16(newForm.abcdMatrix.cols);
            newForm.nuValue = this.nu;
            newForm.nyValue = this.ny;
            newForm.FValue = 3;
            newForm.ShowDialog();
            DMCcontrol.KFValue = newForm.TaMatrix;
        }

        private void limitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Limit.ShowDialog();

            if (Limit.isFlagTrue)
            {
                bool flag = DMCcontrol.CheckLimit(Limit.dumax_Value, Limit.dumin_Value, Limit.umax_Value, Limit.umin_Value, Limit.ymax_Value, Limit.ymin_Value, DMCVar.uMatrix, DMCVar.yMatrix);
                if (flag)
                {
                    DMCcontrol.SetLimit(Limit.dumax_Value.Data, Limit.dumin_Value.Data, Limit.umax_Value.Data, Limit.umin_Value.Data, Limit.ymax_Value.Data, Limit.ymin_Value.Data);

                    Matrix tt = Matrix.Zeros(nu + ny, 4);
                    tt = ProStringToMatrix(Limit.dumax_Value.Data, Limit.dumin_Value.Data, Limit.umax_Value.Data, Limit.umin_Value.Data, Limit.ymax_Value.Data, Limit.ymin_Value.Data);
                    for (int i = 0; i < (nu + ny); i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            listView1.Items[i].SubItems[j + 4].Text = tt[i, j].ToString("f3");
                            if ((i >= nu) && (j >= 2))
                            {
                                listView1.Items[i].SubItems[j + 4].Text = "###";
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Inappropriate constraints.");
                }
                Limit.BackupLimit(flag);
            }
        }

        public void setPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPoint.ShowDialog();
            if (SetPoint.isFlagTrue)
            {
                bool flag = DMCcontrol.CheckSetPointAndLimit(SetPoint.SetPoint);
                if (flag)
                {
                    DMCcontrol.SetPoint(SetPoint.SetPoint);
                    for (int i = 0; i < ny; i++)
                    {
                        listView1.Items[i + nu].SubItems[3].Text = SetPoint.SetPoint[i].ToString("f3");
                    }
                    //OPCpanel.write("APC.CV1",SetPoint.SetPoint[0].ToString());
                    listView1.Items[2].SubItems[8].Text = (SetPoint.SetPoint[0] + 3).ToString("f3");
                    listView1.Items[2].SubItems[9].Text = (SetPoint.SetPoint[0] - 3).ToString("f3");
                    listView1.Items[3].SubItems[8].Text = (SetPoint.SetPoint[1] + 20).ToString("f3");
                    listView1.Items[3].SubItems[9].Text = (SetPoint.SetPoint[1] - 20).ToString("f3");
                    listView1.Items[4].SubItems[8].Text = (SetPoint.SetPoint[2] + 20).ToString("f3");
                    listView1.Items[4].SubItems[9].Text = (SetPoint.SetPoint[2] - 20).ToString("f3");
                    listView1.Items[6].SubItems[8].Text = (SetPoint.SetPoint[4] + 30000).ToString("f3");
                    listView1.Items[6].SubItems[9].Text = (SetPoint.SetPoint[4] - 30000).ToString("f3");
                    listView1.Items[7].SubItems[8].Text = (SetPoint.SetPoint[5] + 0.8).ToString("f3");
                    listView1.Items[7].SubItems[9].Text = (SetPoint.SetPoint[5] - 0.8).ToString("f3");
                }
                else
                {
                   // MessageBox.Show("Set value beyond the scope of the constraint.");
                }
                SetPoint.BackupSetpoint(flag);
            }
        }

        public double readsetpoint(string item)
        {
         try
        {

            
                double value;
                string val = "";
                val = OPCpanel.Read(item);
                value = Convert.ToDouble(val);
                return value;
        }
            catch
            {
                double value1;
                value1 = double.NaN;
                return value1;

            }
    }

        private void setDisturbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDisturb.ShowDialog();
            if (SetDisturb.isFlagTrue)
            {
                DMCcontrol.SetDisturb(SetDisturb.SetDisturb);
            }
        }
        
        public void output(string log)
        {
            //if (tbxlog.GetLineFromCharIndex(tbxlog.Text.Length) > 100)
            //    tbxlog.Text = "";
            //tbxlog.AppendText(DateTime.Now.ToString("HH:mm:ss ") + log + "\r\n");
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int a = GetSelectedItemFromListView(listView1);

            FrmPropriety propriety = new FrmPropriety(listView1.Items[(a)].SubItems[0].Text, listView1.Items[(a)].SubItems[1].Text,
                                                        listView1.Items[(a)].SubItems[2].Text, listView1.Items[(a)].SubItems[3].Text,
                                                        listView1.Items[(a)].SubItems[4].Text, listView1.Items[(a)].SubItems[5].Text,
                                                        listView1.Items[(a)].SubItems[6].Text, listView1.Items[(a)].SubItems[7].Text);
            propriety.Show();
        }

        private int GetSelectedItemFromListView(ListView listView)
        {
            return listView.SelectedItems[0].Index;
        }

        private Matrix ProStringToMatrix(double[,] dumax, double[,] dumin, double[,] umax, double[,] umin, double[,] ymax, double[,] ymin)
        {
            Matrix All = Matrix.Zeros(nu+ny,4);
            for (int i = 0; i < nu; i++)
            {
                All[i, 2] = dumax[i, 0];
            }
            for (int i = 0; i < nu; i++)
            {
                All[i, 3] = dumin[i, 0];
            }
            for (int i = 0; i < nu; i++)
            {
                All[i, 0] = umax[i, 0];
            }
            for (int i = 0; i < nu; i++)
            {
                All[i, 1] = umin[i, 0];
            }
            for (int i = 0; i < ny; i++)
            {
                All[nu + i, 0] = ymax[i, 0];
            }
            for (int i = 0; i < ny; i++)
            {
                All[nu + i, 1] = ymin[i, 0];
            }
            return All;
        }
        

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabControl2.SelectedIndex)
            {
                case 0:
                    listView1.Visible = true;
                    lvRefreshItems.Visible = false;
                    break;
                case 1:
                    listView1.Visible = false;
                    lvRefreshItems.Visible = true;
                    break;
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            for(int i=0;i<9;i++)
            {
                ListViewItem lvi1 = new ListViewItem();
                lvi1.SubItems.Add(" ");
                lvi1.SubItems.Add(" ");
                lvi1.SubItems.Add(" ");
                lvi1.SubItems.Add(" ");
                lvi1.SubItems.Add(" ");
                this.lvRefreshItems.Items.Add(lvi1);
            }

            //label5.Text = invone(1000, 0, 0.5).ToString();

        }


        private void startandpause_Tick(object sender, EventArgs e)//APC切停
        {
            ////double oxy;//氧流量
            ////double centeroxy;//中心氧
            //double oxycoalratio;//氧煤比
            //double realfurnacetem;//气化炉温度 
            ////double[] furnacetem=new double[4];//气化炉温度数组                               
            ////double softmeasuretem;//软测量温度   
            //double zkyc;
            //double syngas;//合成气流量
            //double coandh2;//有效气流量
            ////oxy = readsetpoint("1420FIC1054.PIDA.PV");
            ////centeroxy = readsetpoint("1420FIC1067.PIDA.PV");
            ////oxycoalratio = readsetpoint("1420FFI1068DACA.PV");
            ////syngas = readsetpoint("1420FI1203DACA.PV");
            ////coandh2 = readsetpoint("1420AI1206_CO.PV") + readsetpoint("1420AI1206_H2.PV");
            //oxycoalratio = readsetpoint("APC.CV1");
            //realfurnacetem = readsetpoint("APC.CV2");
            //zkyc = readsetpoint("APC.CV4");
            //syngas = readsetpoint("APC.CV5");
            //coandh2 = readsetpoint("APC.CV6");
            ////if ((startandpauseItem == 0) || (oxy < 27600) || (oxy > 125000) || (centeroxy < 2500) || (centeroxy > 16900) ||(oxycoalratio<450)||(oxycoalratio >640)
            ////    ||(realfurnacetem<1180)||(realfurnacetem >1350)||(syngas>800000)||(coandh2<76)||(coandh2>81))
            //if ( (oxycoalratio < 450) || (oxycoalratio > 520) || (realfurnacetem < 1150) || (realfurnacetem > 1350) || (zkyc>100))
            //{
            //    controltime.Enabled = false;                          //点击Pause按钮，timer1停止运行，控制器开始输出
            //    filtertime.Enabled = false;
            //    DMCcontrol.InitFlag = true;                       //原来暂停逻辑里有此语句
            //    startDMCtoolStripMenuItem.Enabled = true;         //Start按钮变为可用
            //    pauseDCMToolStripMenuItem.Enabled = false;       //Pause按钮变为不可用  
            //    label3.BackColor = Color.Red;
            //    label3.Text = "停止";

            //}
            //else
            //{
            //    controltime.Enabled = true;                            //点击Start按钮，timer1开始运行，控制器开始输出
            //    filtertime.Enabled = true;
            //    output("DMC控制--开始");
            //    startDMCtoolStripMenuItem.Enabled = false;         //Start按钮变为不可用
            //    pauseDCMToolStripMenuItem.Enabled = true;         //Pause按钮变为可用  
            //    label3.BackColor = Color.Green;
            //    label3.Text = "投用";
            //}
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否关闭程序", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        public static double one(double one_max, double one_min, double one_now)
        {
            double value;
            value = (one_now - one_min) / (one_max - one_min);
            return value;
        }
        public static double invone(double one_max, double one_min, double one_now)
        {
            double value;
            value = one_min+one_now*(one_max - one_min);
            return value;
        }
    }
}
