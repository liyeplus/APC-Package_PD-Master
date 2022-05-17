using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using OPCDA.NET;
using OPC;
using OPCDA;
using System.IO;
using DMC;
using System.Configuration;

namespace DMC
{   
     public partial class FrmOPC : Form
     {
        #region
        public static OpcServer Srv =null;
        public static string[] OPCReadItems ;
        public static double[] datarange;
        BrowseTree ItemTree;
        SyncIOGroup ReadWriteGroup;
        RefreshGroup AsyncRefrGroup;
        ItemReadWrite irw = null;
        OPCItemState Rslt;
        OPCDATASOURCE dsrc = OPCDATASOURCE.OPC_DS_CACHE;  //数据源的读取有两种方式 1种是通过读取缓存区 1种是通过读取硬件
        public static int num = 0;
        public static string[,,] DataSaveArray;  //用来保存采集到的数 创建3维数组  第一维表示列表中有多少不同item需要保存
        ListView MainListView;
        

        //第二维表示每个item中有多少数据（名字，时间 质量）
        //第三维表示需要保存每一个item的个数
        //为了在主程序界面上实现刷新等效果  将服务器等重要变量放在这个主程序界面上
        public FrmOPC()
        {
            InitializeComponent();
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
            addItemToolStripMenuItem.Enabled = false;     
        }
        public FrmOPC(ListView temp)
        {
            InitializeComponent();
            MainListView = temp;
        }



        /// <summary>
        /// 菜单栏中的推出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 菜单栏中的连接服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Srv == null)
            {
                Srv = new OpcServer();
            }
            ServerConnect sc = new ServerConnect(Srv);
            sc.Location = this.Location + new Size(50, 100);
            sc.ShowDialog();
            bool isConnected = sc.isConnectFunction();
            if (isConnected == true)
            {
                //如果判断显示服务器已经正常连接
                connectToolStripMenuItem.Enabled = false;
                disconnectToolStripMenuItem.Enabled = true;
                addItemToolStripMenuItem.Enabled = true;             
                Srv = sc.Srv;               
                //连接上以后的处理函数    
                try
                {
                    ReadWriteGroup = new SyncIOGroup(Srv);  
                    DataChangeEventHandler dch = new DataChangeEventHandler(this.DataChangeHandler);
                    AsyncRefrGroup = new RefreshGroup(Srv, dch);                                      //如果不是同一个用户名  则在这个阶段 其检测出现错误
                    ItemTree = new BrowseTree(Srv);  //因为后面更新状态的时候有用  所以放在主程序里面
                    irw = new ItemReadWrite(Srv, ItemTree, AsyncRefrGroup, ReadWriteGroup);
                }
                catch (OPCException ex)
                {
                    Srv = null;
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
        }
        /// <summary>
        /// 菜单栏中的断开连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //断开连接后的处理函数
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem.Enabled = false;
            addItemToolStripMenuItem.Enabled = false;       
            ServerConnect sc = new ServerConnect(Srv);
            sc.disconnectServer();
            lvRefreshItems.Items.Clear();  //清空listview中的现存的所有数据
        }
        /// <summary>
        ///增加读取和写入项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {          
                //对项的写入和读出
            irw.Location = this.Location + new Size(100,100);
            irw.ShowDialog();    //调用的窗口关闭后才执行下面的语句
            try
            {
                if (lvRefreshItems.Items.Count != 0)
                {
                    //如果刚开始存在数据
                    lvRefreshItems.Items.Clear();
                }
                if (ItemReadWrite.a == true)
                {
                    for (int i = 0; i < irw.RefreshItemName.Length; i++)
                    {
                        //为了实现调理性  现在这个地方就现将名字加到列表中 后面的函数在实现刷新操作
                        int rtc = ReadWriteGroup.Read(dsrc, irw.RefreshItemName[i].ToString(), out Rslt);
                        if (HRESULTS.Succeeded(rtc))   //成功读取opc服务器
                        {
                            if (Rslt != null)
                            {
                                if (HRESULTS.Succeeded(Rslt.Error)) //成功读取了item选项
                                {

                                    ListViewItem lvI = new ListViewItem((i + 1).ToString());   //第一列 index
                                    string strName = irw.RefreshItemName[i].ToString();
                                    lvI.SubItems.Add(strName);                    //第二列 名字
                                    string strValue = Rslt.DataValue.ToString();
                                    lvI.SubItems.Add(strValue);                  //第三列值        
                                    string strQuality = ReadWriteGroup.GetQualityString(Rslt.Quality);
                                    lvI.SubItems.Add(strQuality);              //第四列质量
                                    DateTime dt = DateTime.Now;
                                    lvI.SubItems.Add(dt.ToString());                //第五列时间戳
                                    this.lvRefreshItems.Items.Add(lvI);
                                }
                            }
                        }
                    }

                }
            }
            catch
            {
                this.Close();  //避免异常关闭后程序会死
            }      
            //btnSaveData.Enabled = true;
            OPCReadItems = new string[lvRefreshItems.Items.Count];
            datarange = new double[lvRefreshItems.Items.Count];
            for (int i = 0; i < lvRefreshItems.Items.Count; i++)
            {
                OPCReadItems[i] = lvRefreshItems.Items[i].SubItems[1].Text;
                datarange[i] = 0;
            }
        }

     

        public void DataChangeHandler(object sender, DataChangeEventArgs e)
        {

           
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new DataChangeEventHandler(DataChangeHandler), new object[] { sender, e });
                    return;
                }
                if (irw == null)
                {
                    //判断如果还没有实例化irw 也即是还没有数据需要刷新
                    return;
                }
                else
                {
                    //有数据需要刷新
                    //for (int j = 0; j < e.sts.Length; j++)   //e.sts 相当于一个数组列表
                    //{

                    //}               
                    for (int i = 0; i < irw.RefreshItemName.Length; i++)
                    {
                        OPCItemState Rslt;
                        OPCDATASOURCE dsrc = OPCDATASOURCE.OPC_DS_CACHE;
                        int rtc = ReadWriteGroup.Read(dsrc, irw.RefreshItemName[i].ToString(), out Rslt);
                        if (HRESULTS.Succeeded(rtc))   //成功读取opc服务器
                        {
                            if (Rslt != null)
                            {
                                if (HRESULTS.Succeeded(Rslt.Error)) //成功读取了item选项
                                {

                                    if (lvRefreshItems.Items[i].SubItems[1].Text.Equals(irw.RefreshItemName[i].ToString()))
                                    {
                                        string strValue = Rslt.DataValue.ToString();
                                        lvRefreshItems.Items[i].SubItems[2].Text = strValue;               //第三列值
                                        //label1.Text = lvRefreshItems.Items[0].SubItems[2].Text;
                                        string strQuality = ReadWriteGroup.GetQualityString(Rslt.Quality);
                                        lvRefreshItems.Items[i].SubItems[3].Text = strQuality;              //第四列质量
                                        DateTime dt = DateTime.Now;
                                        lvRefreshItems.Items[i].SubItems[4].Text = dt.ToString();                //第五列时间戳
                                    }


                                }
                            }
                        }
                    }
                }
            
           
            
        }
       
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
         
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
       
        }
        #endregion



        public void write(string item, string value) //frmMain调用此函数，因为readwritegroup.write有一定的保护级别
        {
            ReadWriteGroup.Write(item, value);
        }

        public string Read(string str)
        {
            OPCItemState Rslt;
            OPCDATASOURCE dsrc = OPCDATASOURCE.OPC_DS_CACHE;
            ReadWriteGroup = new SyncIOGroup(Srv);
            string val = "";
            int rtc = ReadWriteGroup.Read(dsrc, str, out Rslt);
            if (HRESULTS.Succeeded(rtc))        // read from OPC server successful
            {
                if (Rslt != null)
                {
                    if (HRESULTS.Succeeded(Rslt.Error)) // item read successful
                    {
                        val = Rslt.DataValue.ToString();
                    }
                    else            // the item could not be read
                    {
                        //MessageBox.Show("error-1");
                        val = ReadWriteGroup.GetErrorString(Rslt.Error);
                    }
                }
                else
                {
                    //MessageBox.Show("error-2");
                    val = "No data could be read";
                }
            }
            else        // OPC server read error
            {
                //MessageBox.Show("error-3");
                val = ReadWriteGroup.GetErrorString(rtc);
            }
            return val;
        }
    

        public string AddAndReadItem(string str)        //从OPC Server中读数返回读取值，参数（读取位点名称）
        {
            
            OPCItemState Rslt;
            OPCDATASOURCE dsrc = OPCDATASOURCE.OPC_DS_CACHE;         
            string val = "";
            int rtc = ReadWriteGroup.Read(dsrc, str, out Rslt);
                if (HRESULTS.Succeeded(rtc))		// read from OPC server successful
                {
                    if (Rslt != null)
                    {
                        if (HRESULTS.Succeeded(Rslt.Error))	// item read successful
                        {
                            val = Rslt.DataValue.ToString();
                        }
                        else			// the item could not be read
                        {
                            //MessageBox.Show("error-1");
                            val = ReadWriteGroup.GetErrorString(Rslt.Error);
                        }
                    }
                    else
                    {
                        //MessageBox.Show("error-2");
                        val = "No data could be read";
                    }
                }
                else		// OPC server read error
                {
                    //MessageBox.Show("error-3");
                    val = ReadWriteGroup.GetErrorString(rtc);
                }
                return val;
            }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }

    }

