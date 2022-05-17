namespace DMC
{
    partial class DMCtoOPC
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chooseD = new System.Windows.Forms.RadioButton();
            this.chooseU = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.chooseY = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectToOPC = new System.Windows.Forms.Button();
            this.OPCItem = new System.Windows.Forms.TextBox();
            this.DMCItem = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chooseD);
            this.groupBox1.Controls.Add(this.chooseU);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.chooseY);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ConnectToOPC);
            this.groupBox1.Controls.Add(this.OPCItem);
            this.groupBox1.Controls.Add(this.DMCItem);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(939, 100);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Points";
            // 
            // chooseD
            // 
            this.chooseD.AutoSize = true;
            this.chooseD.Location = new System.Drawing.Point(172, 46);
            this.chooseD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chooseD.Name = "chooseD";
            this.chooseD.Size = new System.Drawing.Size(47, 21);
            this.chooseD.TabIndex = 2;
            this.chooseD.Text = "DV";
            this.chooseD.UseVisualStyleBackColor = true;
            // 
            // chooseU
            // 
            this.chooseU.AutoSize = true;
            this.chooseU.Location = new System.Drawing.Point(112, 46);
            this.chooseU.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chooseU.Name = "chooseU";
            this.chooseU.Size = new System.Drawing.Size(47, 21);
            this.chooseU.TabIndex = 1;
            this.chooseU.Text = "MV";
            this.chooseU.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(836, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 45;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chooseY
            // 
            this.chooseY.AutoSize = true;
            this.chooseY.Checked = true;
            this.chooseY.Location = new System.Drawing.Point(52, 46);
            this.chooseY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chooseY.Name = "chooseY";
            this.chooseY.Size = new System.Drawing.Size(47, 21);
            this.chooseY.TabIndex = 0;
            this.chooseY.TabStop = true;
            this.chooseY.Text = "CV";
            this.chooseY.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(375, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link";
            // 
            // ConnectToOPC
            // 
            this.ConnectToOPC.Location = new System.Drawing.Point(734, 40);
            this.ConnectToOPC.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConnectToOPC.Name = "ConnectToOPC";
            this.ConnectToOPC.Size = new System.Drawing.Size(80, 31);
            this.ConnectToOPC.TabIndex = 41;
            this.ConnectToOPC.Text = "Update";
            this.ConnectToOPC.UseVisualStyleBackColor = true;
            this.ConnectToOPC.Click += new System.EventHandler(this.ConnectToOPC_Click);
            // 
            // OPCItem
            // 
            this.OPCItem.Location = new System.Drawing.Point(429, 44);
            this.OPCItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.OPCItem.Name = "OPCItem";
            this.OPCItem.Size = new System.Drawing.Size(270, 27);
            this.OPCItem.TabIndex = 44;
            // 
            // DMCItem
            // 
            this.DMCItem.Location = new System.Drawing.Point(264, 44);
            this.DMCItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DMCItem.Name = "DMCItem";
            this.DMCItem.Size = new System.Drawing.Size(65, 27);
            this.DMCItem.TabIndex = 43;
            this.DMCItem.Text = "1";
            // 
            // DMCtoOPC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 126);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "DMCtoOPC";
            this.Text = "DMCtoOPC";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton chooseU;
        private System.Windows.Forms.RadioButton chooseY;
        private System.Windows.Forms.TextBox OPCItem;
        private System.Windows.Forms.TextBox DMCItem;
        private System.Windows.Forms.Button ConnectToOPC;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton chooseD;
        private System.Windows.Forms.Label label1;
    }
}