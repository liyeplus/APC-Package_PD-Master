using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Net;
using System.Windows.Forms;
using OPC;
using OPCDA;
using OPC.Common;
using OPCDA.NET;


namespace DMC
{
    public partial class ServerConnect : Form
    {
        public  string HostName;  //主机的名称       
        public  bool isConnect=false;
        public string ServerName;
        public OpcServer Srv = null;


        public ServerConnect(OpcServer srv)
        {
            InitializeComponent();
            Srv = srv;
            this.txtIP.Text = "10.101.1.33";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry IPHostEntry1 = Dns.GetHostEntry(txtIP.Text);                
                HostName = IPHostEntry1.HostName.ToString(); //获得本地计算机的名称
                MessageBox.Show(HostName);
                OpcServerBrowser SrvList = new OpcServerBrowser(HostName);     // " PC201307081939   lpx-pc"对于远程服务器 在这个地方直接输入计算机名称就可以了
                string[] Servers;
                SrvList.GetServerList(out Servers);
                if (Servers != null)
                {
                    lbServersName.Items.Clear();
                    lbServersName.Items.AddRange(Servers);    //检索到相应的服务器  则将相应的服务器名称显示出来
                    // cbServerName.SelectedIndex = 0;          //句柄  默认选中第一个                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void lbServersName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbServersName.SelectedItem == null)
            {
                //还没有选择项
                return;
            }           
            txtOPCServerName.Text = lbServersName.SelectedItem.ToString();
            ServerName = txtOPCServerName.Text;
            //MessageBox.Show(lbServersName.SelectedItem.ToString());
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            int rtc;
            try
            {
                if (Srv != null)
                {                   
                    rtc = Srv.Connect(HostName, txtOPCServerName.Text);  //这个地方还可以通过获取clsid /proid来连接服务器 
                    if (HRESULTS.Failed(rtc))
                    {
                        Srv = null;
                        return;
                    }
                }                
            }
            catch
            {
                Srv = null;
                return;
            }
            SERVERSTATUS stat = new SERVERSTATUS();
            rtc = Srv.GetStatus(out stat);
            if (HRESULTS.Succeeded(rtc))
            if(true)
            {
                //如果连接服务器成功
                isConnect = true;
                this.Close();
            }
            else
            {
               //如果连接服务器失败
                //isConnect = false;
                Srv = null;
                return;
            }
            
            //如果连接服务器成功  将读取到的节点添加到我们的客户端的服务器
        
        }

        /// <summary>
        /// 判断服务器是不是连接成功
        /// </summary>
        public bool isConnectFunction()
        {
            return isConnect;
        }

        public void disconnectServer()
        {
            if (Srv != null)
            {
                Srv.Disconnect();               
                Srv = null;
                
            }
           
        }
       
    }
}
