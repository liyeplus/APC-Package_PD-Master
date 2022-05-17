namespace DMC
{
    partial class ItemReadWrite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemReadWrite));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbselectedItems = new System.Windows.Forms.ListBox();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddToReFresh = new System.Windows.Forms.Button();
            this.serverBrowser = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtWriteItem = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtItemID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbselectedItems);
            this.groupBox1.Controls.Add(this.tvItems);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(605, 395);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据组";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(323, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "已选择的项";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "添加项";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(20, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "服务器";
            // 
            // txtItemID
            // 
            this.txtItemID.Location = new System.Drawing.Point(68, 346);
            this.txtItemID.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemID.Name = "txtItemID";
            this.txtItemID.Size = new System.Drawing.Size(132, 25);
            this.txtItemID.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 351);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "项名";
            // 
            // lbselectedItems
            // 
            this.lbselectedItems.FormattingEnabled = true;
            this.lbselectedItems.ItemHeight = 15;
            this.lbselectedItems.Location = new System.Drawing.Point(324, 54);
            this.lbselectedItems.Margin = new System.Windows.Forms.Padding(4);
            this.lbselectedItems.Name = "lbselectedItems";
            this.lbselectedItems.Size = new System.Drawing.Size(236, 334);
            this.lbselectedItems.TabIndex = 2;
            this.lbselectedItems.SelectedIndexChanged += new System.EventHandler(this.lbselectedItems_SelectedIndexChanged);
            // 
            // tvItems
            // 
            this.tvItems.Location = new System.Drawing.Point(16, 54);
            this.tvItems.Margin = new System.Windows.Forms.Padding(4);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(229, 282);
            this.tvItems.TabIndex = 1;
            this.tvItems.DoubleClick += new System.EventHandler(this.tvItems_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAddToReFresh);
            this.groupBox2.Controls.Add(this.serverBrowser);
            this.groupBox2.Location = new System.Drawing.Point(11, 418);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(611, 74);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选项";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(487, 30);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 29);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(339, 30);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 29);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddToReFresh
            // 
            this.btnAddToReFresh.Location = new System.Drawing.Point(163, 30);
            this.btnAddToReFresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddToReFresh.Name = "btnAddToReFresh";
            this.btnAddToReFresh.Size = new System.Drawing.Size(125, 29);
            this.btnAddToReFresh.TabIndex = 0;
            this.btnAddToReFresh.Text = "开始读取";
            this.btnAddToReFresh.UseVisualStyleBackColor = true;
            this.btnAddToReFresh.Click += new System.EventHandler(this.btnAddToRefresh_Click);
            // 
            // serverBrowser
            // 
            this.serverBrowser.Location = new System.Drawing.Point(8, 30);
            this.serverBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.serverBrowser.Name = "serverBrowser";
            this.serverBrowser.Size = new System.Drawing.Size(100, 29);
            this.serverBrowser.TabIndex = 0;
            this.serverBrowser.Text = "浏览";
            this.serverBrowser.UseVisualStyleBackColor = true;
            this.serverBrowser.Click += new System.EventHandler(this.serverBrowser_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(683, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.MaximumSize = new System.Drawing.Size(400, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "请从已选择的项中选择需要写入的项：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(685, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "项:";
            // 
            // txtWriteItem
            // 
            this.txtWriteItem.Location = new System.Drawing.Point(776, 158);
            this.txtWriteItem.Margin = new System.Windows.Forms.Padding(4);
            this.txtWriteItem.Name = "txtWriteItem";
            this.txtWriteItem.ReadOnly = true;
            this.txtWriteItem.Size = new System.Drawing.Size(201, 25);
            this.txtWriteItem.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(687, 230);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "值:";
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.Location = new System.Drawing.Point(776, 225);
            this.txtWriteValue.Margin = new System.Windows.Forms.Padding(4);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(201, 25);
            this.txtWriteValue.TabIndex = 4;
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(811, 322);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(4);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(100, 29);
            this.btnWrite.TabIndex = 5;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // ItemReadWrite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 506);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.txtWriteValue);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtWriteItem);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ItemReadWrite";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ItemReadWrite";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button serverBrowser;
        private System.Windows.Forms.TreeView tvItems;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListBox lbselectedItems;
        private System.Windows.Forms.TextBox txtItemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtWriteItem;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWriteValue;
        private System.Windows.Forms.Button btnWrite;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnAddToReFresh;
    }
}