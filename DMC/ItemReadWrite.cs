using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OPC;
using OPCDA;
using OPCDA.NET;
using System.Net;

namespace DMC
{
    public partial class ItemReadWrite : Form
    {
        public OpcServer Srv = null;
        BrowseTree ItemTree;
        public string[] RefreshItemName;
        RefreshGroup AsyncRefrGroup;
        SyncIOGroup ReadWriteGroup;
        OPCItemState Rslt;
        OPCDATASOURCE dsrc = OPCDATASOURCE.OPC_DS_CACHE;  //数据源的读取有两种方式 1种是通过读取缓存区 1种是通过读取硬件
        //OPCDATASOURCE dsrc1=OPCDATASOURCE.OPC_DS_DEVICE //从设备读取数据

        public ItemReadWrite(OpcServer srv, BrowseTree itemtree, RefreshGroup asyncrefrgroup, SyncIOGroup readwritegroup) //从主函数中将参数的值传到这个函数
        {
            InitializeComponent();
            Srv = srv;
            ItemTree = itemtree;
            AsyncRefrGroup = asyncrefrgroup;
            ReadWriteGroup = readwritegroup;

            //预置点位
            //lbselectedItems.Items.Add("1420MQHAPC1.EQA.OUT");通讯中断显示点位
            //lbselectedItems.Items.Add("CV2.CV2");
            //lbselectedItems.Items.Add("CV3.CV3");
            //lbselectedItems.Items.Add("CV4.CV4");
            //lbselectedItems.Items.Add("MV1.MV1");
            ////lbselectedItems.Items.Add("MV2.MV2");
            //lbselectedItems.Items.Add("DV1.DV1");
            //lbselectedItems.Items.Add("CV1.SP");
            //lbselectedItems.Items.Add("CV2.SP");
            //lbselectedItems.Items.Add("CV3.SP");
            //lbselectedItems.Items.Add("CV4.SP");
            //lbselectedItems.Items.Add("CV1.startandpause");
            //lbselectedItems.Items.Add("CV2.startandpause");
            //lbselectedItems.Items.Add("CV3.startandpause");
            //lbselectedItems.Items.Add("CV4.startandpause");
            //lbselectedItems.Items.Add("WATCHDOG.PV");
            //lbselectedItems.Items.Add("WATCHDOG.startandpause");
        }

        /// <summary>
        /// 连接和预览服务器中存在的项组合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serverBrowser_Click(object sender, EventArgs e)
        {
            int rtc;
            tvItems.Nodes.Clear();  //将之前存在于treeview中的数值全部清除                             
            rtc = ItemTree.CreateTree();              //从根节点开始读取服务器

            if (HRESULTS.Succeeded(rtc))
            {
                tvItems.ImageList = ItemTree.ImageList;
                tvItems.Nodes.AddRange(ItemTree.Root());
            }
            else
            {
                Srv = null;
                return;
            }
        }
        /// <summary>
        /// 取消按键按下后的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        ///原服务器中双击后增加项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvItems_DoubleClick(object sender, EventArgs e)
        {
            bool isRepeat = false;  //判断在现有的listbox中是不是有和新添项有重复的
            try
            {
                if (tvItems.SelectedNode.Nodes.Count == 0)//判断选中的节点是叶子节点
                {
                    if (lbselectedItems.Items.Count == 0) //判断是不是第一个加入listbox中的项 因为第一下不存在重复的问题
                    {
                        //  lbselectedItems.Items.Add(tvItems.SelectedNode);                    
                        string selItem = ItemTree.ItemName(tvItems.SelectedNode);
                        lbselectedItems.Items.Add(selItem);
                    }
                    else
                    {
                        for (int i = 0; i < lbselectedItems.Items.Count; i++)
                        {
                            string selItem = ItemTree.ItemName(tvItems.SelectedNode);
                            if (lbselectedItems.Items[i].Equals(selItem))//判断现有的所有项中是否和正在添加项有重复
                            {
                                isRepeat = true;
                                MessageBox.Show("the Item has existed");
                            }
                        }
                        if (isRepeat == false)
                        {
                            string selItem = ItemTree.ItemName(tvItems.SelectedNode);
                            lbselectedItems.Items.Add(selItem);
                            //lbselectedItems.Items.Add(tvItems.SelectedNode.Text);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 通过直接输入添加项的ID来添加该项（实际使用的时候可能会直接输入位号来添加项）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            bool isRepeat = false;
            TreeNode tn = FindNodeByCode(tvItems.Nodes, txtItemID.Text);
            if (tn == null) //判断在该服务器中是否有该item的存在
            {
                //服务器中不存在该Item
                MessageBox.Show("these do not exit the item in  servers");
            }
            else
            {
                //服务器中存在该项 
                //则判断该项是否已经在listbox列表中存在
                if (lbselectedItems.Items.Count == 0) //判断是不是第一个加入listbox中的项 因为第一下不存在重复的问题
                {
                    lbselectedItems.Items.Add(txtItemID.Text);
                }
                else
                {
                    for (int i = 0; i < lbselectedItems.Items.Count; i++)
                    {
                        if (lbselectedItems.Items[i].Equals(txtItemID.Text))//判断现有的所有项中是否和正在添加项有重复
                        {
                            isRepeat = true;
                            MessageBox.Show("the Item has existed");
                        }
                    }
                    if (isRepeat == false)
                    {
                        lbselectedItems.Items.Add(txtItemID.Text);
                    }
                }

            }
        }
        /// <summary>
        /// 判断在服务器中是否存在手动输入的item 没有则判断输入无效 有则是正确输入
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        private TreeNode FindNodeByCode(TreeNodeCollection nodes, string Code)
        {

            //遍历所有节点  看是否存在目标节点
            if (nodes.Count == 0)
            {
                //如果节点为空 则返回值为空
                return null;
            }
            foreach (TreeNode node in nodes)
            {
                if (ItemTree.ItemName(node).Equals(Code.Trim())) //通过字符串判断他们相同
                {
                    return node;
                }
                TreeNode node_2;
                if (node.Nodes.Count != 0) //判断该节点是否是子节点 如果不是则继续往下查找
                {
                    //如果该节点不是子节点 则使用递归的方法 继续查找
                    node_2 = FindNodeByCode(node.Nodes, Code);
                }
                else
                {
                    //无子节点
                    node_2 = null;
                }
                if (node_2 != null)
                {
                    //如果递归找到了目标 则返回查找到的值
                    return node_2;
                }
            }

            //遍历所有节点 没找到目标 返回空
            return null;
        }
        /// <summary>
        /// 删除选中项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            lbselectedItems.Items.Remove(lbselectedItems.SelectedItem);
        }
        public static bool a = false;//判断是否添加了点位
        public void btnAddToRefresh_Click(object sender, EventArgs e)
        {
            //从服务器中读取数据的方式是：根节点+节点的形式如（random.int1）  
            //为了适应上面我们手动输入节点末节点的形式，我们在这采用反推的方式 来匹配读出数据（从listbox列表中反找到treeview的server）
            if (lbselectedItems.Items.Count == 0)
            {
                //如果listbox列表中没有数据
                MessageBox.Show("Please chose the item which you want to read!!");
                return;
            }
            RefreshItemName = new string[lbselectedItems.Items.Count];
            for (int i = 0; i < lbselectedItems.Items.Count; i++)
            {
                int rtc = AsyncRefrGroup.Add(lbselectedItems.Items[i].ToString());//将数据放到异步读取组  为了促发datachange事件  已经被加入过的事件则不能再被加入
                                                                                  //lbselectedItems.Items[i].ToString()
                if (HRESULTS.Succeeded(1))
                {
                    RefreshItemName[i] = lbselectedItems.Items[i].ToString();
                    //将列表中的数据读到主页面中进行刷新操作
                }
            }


            for (int i = 0; i < lbselectedItems.Items.Count; i++)
            {
                //为了实现调理性  现在这个地方就现将名字加到列表中 后面的函数在实现刷新操作
                int rtc = ReadWriteGroup.Read(dsrc, lbselectedItems.Items[i].ToString(), out Rslt);
                if (HRESULTS.Succeeded(rtc))   //成功读取opc服务器
                {
                    if (Rslt != null)
                    {
                        if (HRESULTS.Succeeded(Rslt.Error)) //成功读取了item选项
                        {
                            //成功读取添加的Item
                        }
                        else
                        {
                            //如果读取item不成功      在这个地方加是为了直接在lbselectedItems中进行修改
                            string strName = lbselectedItems.Items[i].ToString();
                            string str = "\"" + strName + "\"";
                            try
                            {
                                //当程序执行过快的时候 弹出多个窗口导致程序异常 所以使用try语句
                                MessageBox.Show(str + " read fail");
                                lbselectedItems.Items.Remove(lbselectedItems.Items[i]);
                                if (i >= 0)
                                {
                                    i = i - 1; //这个地方做减法是因为  加入多项不减会导致有些项没被消除
                                }
                            }
                            catch
                            {
                                lbselectedItems.Items.Remove(lbselectedItems.Items[i]);
                                if (i >= 0)
                                {
                                    i = i - 1; //这个地方做减法是因为  加入多项不减会导致有些项没被消除
                                }
                            }


                        }
                    }
                }
            }
            a = true;
            this.Close();
        }

        private void lbselectedItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbselectedItems.SelectedItem == null)
            {
                //还没有选择项
                return;
            }
            txtWriteItem.Text = lbselectedItems.SelectedItem.ToString();
            //MessageBox.Show(lbServersName.SelectedItem.ToString());
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            object val = txtWriteValue.Text;
            int rtc = ReadWriteGroup.Write(txtWriteItem.Text, val);
            //  int rtc2 = ReadWriteGroup.Write("Random.Int1", val);
            //if (HRESULTS.Succeeded(rtc))
            //{
            //    MessageBox.Show("succeed write!!");
            //}            
            try
            {
                MessageBox.Show(ReadWriteGroup.GetErrorString(rtc));
            }
            catch
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int rtc = ReadWriteGroup.Read(dsrc, txtItemID.Text, out Rslt);
            if (rtc < -1000000000)
            {
                MessageBox.Show("找不到该点位");
            }
            else
            {
                MessageBox.Show("添加成功");
                lbselectedItems.Items.Add(txtItemID.Text);
            }

        }

    }
}  
   


