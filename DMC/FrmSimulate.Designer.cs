namespace DMC
{
    partial class FrmSimulate
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
            this.state_A = new System.Windows.Forms.TextBox();
            this.StateComfirm = new System.Windows.Forms.Button();
            this.state_Bb = new System.Windows.Forms.TextBox();
            this.state_Bd = new System.Windows.Forms.TextBox();
            this.state_C = new System.Windows.Forms.TextBox();
            this.state_D = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.delta_U = new System.Windows.Forms.TextBox();
            this.U = new System.Windows.Forms.TextBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.X = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.SeeD = new System.Windows.Forms.Button();
            this.SeeC = new System.Windows.Forms.Button();
            this.SeeBd = new System.Windows.Forms.Button();
            this.SeeBb = new System.Windows.Forms.Button();
            this.SeeA = new System.Windows.Forms.Button();
            this.InsertABCDuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // state_A
            // 
            this.state_A.Location = new System.Drawing.Point(43, 28);
            this.state_A.Name = "state_A";
            this.state_A.Size = new System.Drawing.Size(236, 21);
            this.state_A.TabIndex = 0;
            this.state_A.Text = "[0.9419 0 0 0 0 0;0 0.9535 0 0 0 0;0 0 0.9351 0 0 0;0 0 0 0.9123 0 0;0 0 0 0 0.93" +
    "29 0;0 0 0 0 0 0.9270]";
            // 
            // StateComfirm
            // 
            this.StateComfirm.Location = new System.Drawing.Point(144, 288);
            this.StateComfirm.Name = "StateComfirm";
            this.StateComfirm.Size = new System.Drawing.Size(75, 23);
            this.StateComfirm.TabIndex = 1;
            this.StateComfirm.Text = "OK";
            this.StateComfirm.UseVisualStyleBackColor = true;
            this.StateComfirm.Click += new System.EventHandler(this.StateComfirm_Click);
            // 
            // state_Bb
            // 
            this.state_Bb.Location = new System.Drawing.Point(43, 55);
            this.state_Bb.Name = "state_Bb";
            this.state_Bb.Size = new System.Drawing.Size(236, 21);
            this.state_Bb.TabIndex = 2;
            this.state_Bb.Text = "[0.9706 0;0 0.9766;0 0;0.9555 0;0 0.9661;0 0]";
            // 
            // state_Bd
            // 
            this.state_Bd.Location = new System.Drawing.Point(43, 82);
            this.state_Bd.Name = "state_Bd";
            this.state_Bd.Size = new System.Drawing.Size(236, 21);
            this.state_Bd.TabIndex = 3;
            this.state_Bd.Text = "[0;0;0.9672;0;0;0.9631]";
            // 
            // state_C
            // 
            this.state_C.Location = new System.Drawing.Point(43, 109);
            this.state_C.Name = "state_C";
            this.state_C.Size = new System.Drawing.Size(236, 21);
            this.state_C.TabIndex = 4;
            this.state_C.Text = "[0.7665 -0.900 0.2550 0 0 0;0 0 0 0.6055 -1.3472 0.3712]";
            // 
            // state_D
            // 
            this.state_D.Location = new System.Drawing.Point(43, 136);
            this.state_D.Name = "state_D";
            this.state_D.Size = new System.Drawing.Size(236, 21);
            this.state_D.TabIndex = 5;
            this.state_D.Text = "[0 0 0;0 0 0]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "A:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "Bb:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Bd:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "C:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "D:";
            // 
            // delta_U
            // 
            this.delta_U.Location = new System.Drawing.Point(43, 179);
            this.delta_U.Name = "delta_U";
            this.delta_U.Size = new System.Drawing.Size(236, 21);
            this.delta_U.TabIndex = 11;
            this.delta_U.Text = "[0;0]";
            // 
            // U
            // 
            this.U.Location = new System.Drawing.Point(43, 206);
            this.U.Name = "U";
            this.U.Size = new System.Drawing.Size(236, 21);
            this.U.TabIndex = 12;
            this.U.Text = "[0;0]";
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(43, 233);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(236, 21);
            this.Y.TabIndex = 13;
            this.Y.Text = "[0;0]";
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(43, 260);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(236, 21);
            this.X.TabIndex = 14;
            this.X.Text = "[0;0;0;0;0;0]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "du:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "u:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 236);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "y:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 263);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "x:";
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(225, 288);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 22);
            this.Cancel.TabIndex = 19;
            this.Cancel.Text = "cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SeeD
            // 
            this.SeeD.Location = new System.Drawing.Point(285, 133);
            this.SeeD.Name = "SeeD";
            this.SeeD.Size = new System.Drawing.Size(38, 24);
            this.SeeD.TabIndex = 36;
            this.SeeD.Text = "...";
            this.SeeD.UseVisualStyleBackColor = true;
            this.SeeD.Click += new System.EventHandler(this.SeeD_Click);
            // 
            // SeeC
            // 
            this.SeeC.Location = new System.Drawing.Point(285, 106);
            this.SeeC.Name = "SeeC";
            this.SeeC.Size = new System.Drawing.Size(38, 24);
            this.SeeC.TabIndex = 35;
            this.SeeC.Text = "...";
            this.SeeC.UseVisualStyleBackColor = true;
            this.SeeC.Click += new System.EventHandler(this.SeeC_Click);
            // 
            // SeeBd
            // 
            this.SeeBd.Location = new System.Drawing.Point(285, 79);
            this.SeeBd.Name = "SeeBd";
            this.SeeBd.Size = new System.Drawing.Size(38, 24);
            this.SeeBd.TabIndex = 34;
            this.SeeBd.Text = "...";
            this.SeeBd.UseVisualStyleBackColor = true;
            this.SeeBd.Click += new System.EventHandler(this.SeeBd_Click);
            // 
            // SeeBb
            // 
            this.SeeBb.Location = new System.Drawing.Point(285, 52);
            this.SeeBb.Name = "SeeBb";
            this.SeeBb.Size = new System.Drawing.Size(38, 24);
            this.SeeBb.TabIndex = 33;
            this.SeeBb.Text = "...";
            this.SeeBb.UseVisualStyleBackColor = true;
            this.SeeBb.Click += new System.EventHandler(this.SeeBb_Click);
            // 
            // SeeA
            // 
            this.SeeA.Location = new System.Drawing.Point(285, 25);
            this.SeeA.Name = "SeeA";
            this.SeeA.Size = new System.Drawing.Size(38, 24);
            this.SeeA.TabIndex = 32;
            this.SeeA.Text = "...";
            this.SeeA.UseVisualStyleBackColor = true;
            this.SeeA.Click += new System.EventHandler(this.SeeA_Click);
            // 
            // InsertABCDuy
            // 
            this.InsertABCDuy.Location = new System.Drawing.Point(61, 288);
            this.InsertABCDuy.Name = "InsertABCDuy";
            this.InsertABCDuy.Size = new System.Drawing.Size(77, 23);
            this.InsertABCDuy.TabIndex = 37;
            this.InsertABCDuy.Text = "Auto";
            this.InsertABCDuy.UseVisualStyleBackColor = true;
            this.InsertABCDuy.Click += new System.EventHandler(this.InsertABCDuy_Click);
            // 
            // FrmSimulate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 315);
            this.Controls.Add(this.InsertABCDuy);
            this.Controls.Add(this.SeeD);
            this.Controls.Add(this.SeeC);
            this.Controls.Add(this.SeeBd);
            this.Controls.Add(this.SeeBb);
            this.Controls.Add(this.SeeA);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.X);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.U);
            this.Controls.Add(this.delta_U);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.state_D);
            this.Controls.Add(this.state_C);
            this.Controls.Add(this.state_Bd);
            this.Controls.Add(this.state_Bb);
            this.Controls.Add(this.StateComfirm);
            this.Controls.Add(this.state_A);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSimulate";
            this.Text = "SimulatePanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox state_A;
        private System.Windows.Forms.Button StateComfirm;
        private System.Windows.Forms.TextBox state_Bb;
        private System.Windows.Forms.TextBox state_Bd;
        private System.Windows.Forms.TextBox state_C;
        private System.Windows.Forms.TextBox state_D;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox delta_U;
        private System.Windows.Forms.TextBox U;
        private System.Windows.Forms.TextBox Y;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button SeeD;
        private System.Windows.Forms.Button SeeC;
        private System.Windows.Forms.Button SeeBd;
        private System.Windows.Forms.Button SeeBb;
        private System.Windows.Forms.Button SeeA;
        private System.Windows.Forms.Button InsertABCDuy;
    }
}