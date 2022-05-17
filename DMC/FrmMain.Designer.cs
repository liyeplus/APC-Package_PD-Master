namespace DMC
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.controltime = new System.Windows.Forms.Timer(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dMCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Running = new System.Windows.Forms.ToolStripMenuItem();
            this.startDMCtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseDCMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gammauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setDisturbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.oPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPCClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.opclisttime = new System.Windows.Forms.Timer(this.components);
            this.lvRefreshItems = new System.Windows.Forms.ListView();
            this.colItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colQuality = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startandpause = new System.Windows.Forms.Timer(this.components);
            this.filtertime = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.tabControl2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // controltime
            // 
            this.controltime.Interval = 60000;
            this.controltime.Tick += new System.EventHandler(this.controltime_Tick);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader6,
            this.columnHeader11,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(13, 61);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1404, 252);
            this.listView1.TabIndex = 25;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "变量名";
            this.columnHeader1.Width = 76;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "当前值";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "位号/描述";
            this.columnHeader6.Width = 186;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "CV设定值/MV计算值";
            this.columnHeader11.Width = 159;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "切停Max";
            this.columnHeader7.Width = 84;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "切停Min";
            this.columnHeader8.Width = 86;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "△Max";
            this.columnHeader9.Width = 86;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "△Min";
            this.columnHeader10.Width = 86;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "控制上限";
            this.columnHeader3.Width = 94;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "控制下限";
            this.columnHeader4.Width = 95;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(13, 28);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(540, 33);
            this.tabControl2.TabIndex = 30;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(532, 4);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "DMC";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(532, 4);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "OPC";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dMCToolStripMenuItem,
            this.oPCToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1454, 28);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dMCToolStripMenuItem
            // 
            this.dMCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iNToolStripMenuItem,
            this.Running,
            this.startDMCtoolStripMenuItem,
            this.pauseDCMToolStripMenuItem,
            this.resetToolStripMenuItem,
            this.toolStripSeparator1});
            this.dMCToolStripMenuItem.Name = "dMCToolStripMenuItem";
            this.dMCToolStripMenuItem.Size = new System.Drawing.Size(97, 24);
            this.dMCToolStripMenuItem.Text = "Controller";
            // 
            // iNToolStripMenuItem
            // 
            this.iNToolStripMenuItem.Name = "iNToolStripMenuItem";
            this.iNToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.iNToolStripMenuItem.Text = "Initialize(DMC)";
            this.iNToolStripMenuItem.Click += new System.EventHandler(this.iNToolStripMenuItem_Click);
            // 
            // Running
            // 
            this.Running.Enabled = false;
            this.Running.Name = "Running";
            this.Running.Size = new System.Drawing.Size(198, 26);
            this.Running.Text = "Running(DMC)";
            this.Running.Click += new System.EventHandler(this.Running_Click);
            // 
            // startDMCtoolStripMenuItem
            // 
            this.startDMCtoolStripMenuItem.Enabled = false;
            this.startDMCtoolStripMenuItem.Name = "startDMCtoolStripMenuItem";
            this.startDMCtoolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.startDMCtoolStripMenuItem.Text = "Start(DMC)";
            this.startDMCtoolStripMenuItem.Click += new System.EventHandler(this.startDMCtoolStripMenuItem_Click);
            // 
            // pauseDCMToolStripMenuItem
            // 
            this.pauseDCMToolStripMenuItem.Enabled = false;
            this.pauseDCMToolStripMenuItem.Name = "pauseDCMToolStripMenuItem";
            this.pauseDCMToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.pauseDCMToolStripMenuItem.Text = "Pause(DMC)";
            this.pauseDCMToolStripMenuItem.Click += new System.EventHandler(this.pauseDCMToolStripMenuItem_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gammayToolStripMenuItem,
            this.gammauToolStripMenuItem,
            this.kFToolStripMenuItem,
            this.limitToolStripMenuItem,
            this.setPointToolStripMenuItem,
            this.setDisturbToolStripMenuItem});
            this.resetToolStripMenuItem.Enabled = false;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.resetToolStripMenuItem.Text = "Reset(DMC)";
            // 
            // gammayToolStripMenuItem
            // 
            this.gammayToolStripMenuItem.Name = "gammayToolStripMenuItem";
            this.gammayToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.gammayToolStripMenuItem.Text = "Gamma_y";
            this.gammayToolStripMenuItem.Click += new System.EventHandler(this.gammayToolStripMenuItem_Click);
            // 
            // gammauToolStripMenuItem
            // 
            this.gammauToolStripMenuItem.Name = "gammauToolStripMenuItem";
            this.gammauToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.gammauToolStripMenuItem.Text = "Gamma_u";
            this.gammauToolStripMenuItem.Click += new System.EventHandler(this.gammauToolStripMenuItem_Click);
            // 
            // kFToolStripMenuItem
            // 
            this.kFToolStripMenuItem.Name = "kFToolStripMenuItem";
            this.kFToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.kFToolStripMenuItem.Text = "KF";
            this.kFToolStripMenuItem.Click += new System.EventHandler(this.kFToolStripMenuItem_Click);
            // 
            // limitToolStripMenuItem
            // 
            this.limitToolStripMenuItem.Name = "limitToolStripMenuItem";
            this.limitToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.limitToolStripMenuItem.Text = "Limit";
            this.limitToolStripMenuItem.Click += new System.EventHandler(this.limitToolStripMenuItem_Click);
            // 
            // setPointToolStripMenuItem
            // 
            this.setPointToolStripMenuItem.Name = "setPointToolStripMenuItem";
            this.setPointToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.setPointToolStripMenuItem.Text = "SetPoint";
            this.setPointToolStripMenuItem.Click += new System.EventHandler(this.setPointToolStripMenuItem_Click);
            // 
            // setDisturbToolStripMenuItem
            // 
            this.setDisturbToolStripMenuItem.Enabled = false;
            this.setDisturbToolStripMenuItem.Name = "setDisturbToolStripMenuItem";
            this.setDisturbToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.setDisturbToolStripMenuItem.Text = "SetDisturb";
            this.setDisturbToolStripMenuItem.Click += new System.EventHandler(this.setDisturbToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(195, 6);
            // 
            // oPCToolStripMenuItem
            // 
            this.oPCToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oPCClientToolStripMenuItem,
            this.cToolStripMenuItem});
            this.oPCToolStripMenuItem.Name = "oPCToolStripMenuItem";
            this.oPCToolStripMenuItem.Size = new System.Drawing.Size(54, 24);
            this.oPCToolStripMenuItem.Text = "OPC";
            // 
            // oPCClientToolStripMenuItem
            // 
            this.oPCClientToolStripMenuItem.Name = "oPCClientToolStripMenuItem";
            this.oPCClientToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.oPCClientToolStripMenuItem.Text = "OPC Client";
            this.oPCClientToolStripMenuItem.Click += new System.EventHandler(this.oPCClientToolStripMenuItem_Click);
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Enabled = false;
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.cToolStripMenuItem.Text = "Connet and Config";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
            // 
            // opclisttime
            // 
            this.opclisttime.Interval = 10000;
            this.opclisttime.Tick += new System.EventHandler(this.opclisttime_Tick);
            // 
            // lvRefreshItems
            // 
            this.lvRefreshItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colItem,
            this.colName,
            this.colValue,
            this.colQuality,
            this.colTimestamp});
            this.lvRefreshItems.FullRowSelect = true;
            this.lvRefreshItems.GridLines = true;
            this.lvRefreshItems.HideSelection = false;
            this.lvRefreshItems.Location = new System.Drawing.Point(13, 61);
            this.lvRefreshItems.Margin = new System.Windows.Forms.Padding(4);
            this.lvRefreshItems.Name = "lvRefreshItems";
            this.lvRefreshItems.Size = new System.Drawing.Size(1309, 252);
            this.lvRefreshItems.TabIndex = 74;
            this.lvRefreshItems.UseCompatibleStateImageBehavior = false;
            this.lvRefreshItems.View = System.Windows.Forms.View.Details;
            this.lvRefreshItems.Visible = false;
            // 
            // colItem
            // 
            this.colItem.Text = "序号";
            // 
            // colName
            // 
            this.colName.Text = "变量名";
            this.colName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colName.Width = 120;
            // 
            // colValue
            // 
            this.colValue.Text = "值";
            this.colValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colValue.Width = 100;
            // 
            // colQuality
            // 
            this.colQuality.Text = "质量";
            this.colQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colQuality.Width = 100;
            // 
            // colTimestamp
            // 
            this.colTimestamp.Text = "时间戳";
            this.colTimestamp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colTimestamp.Width = 140;
            // 
            // startandpause
            // 
            this.startandpause.Interval = 3000;
            this.startandpause.Tick += new System.EventHandler(this.startandpause_Tick);
            // 
            // filtertime
            // 
            this.filtertime.Interval = 6000;
            this.filtertime.Tick += new System.EventHandler(this.filtertime_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(513, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 15);
            this.label2.TabIndex = 75;
            this.label2.Text = "APC投用状态：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(625, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 76;
            this.label3.Text = "停止";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(139, 340);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(53, 19);
            this.checkBox1.TabIndex = 77;
            this.checkBox1.Text = "CV1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(281, 340);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(53, 19);
            this.checkBox2.TabIndex = 78;
            this.checkBox2.Text = "CV2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(423, 340);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(53, 19);
            this.checkBox3.TabIndex = 79;
            this.checkBox3.Text = "CV3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(566, 340);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(53, 19);
            this.checkBox4.TabIndex = 80;
            this.checkBox4.Text = "CV4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(699, 340);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(53, 19);
            this.checkBox5.TabIndex = 81;
            this.checkBox5.Text = "CV5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 15);
            this.label4.TabIndex = 82;
            this.label4.Text = "CV投用开关";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(830, 339);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(53, 19);
            this.checkBox6.TabIndex = 83;
            this.checkBox6.Text = "CV6";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1454, 376);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.lvRefreshItems);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "1#PD-Master";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tabControl2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer controltime;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dMCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPCClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.Timer opclisttime;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gammayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gammauToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kFToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ToolStripMenuItem startDMCtoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setDisturbToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Running;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pauseDCMToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader colItem;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colValue;
        private System.Windows.Forms.ColumnHeader colQuality;
        public System.Windows.Forms.ColumnHeader colTimestamp;
        private System.Windows.Forms.Timer startandpause;
        public System.Windows.Forms.ListView lvRefreshItems;
        private System.Windows.Forms.Timer filtertime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox6;
    }
}

