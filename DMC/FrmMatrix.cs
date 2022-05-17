using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMatrix;

namespace DMC
{
    public partial class FrmMatrix : Form

    {
        Matrix ABCD;
        public Matrix abcdMatrix { get { return ABCD; } set { ABCD = value; } }
        int x;
        public int xValue { get { return x; } set { x = value; } }
        int y; 
        public int yValue { get { return y; } set { y = value; } }

        int nu;
        public int nuValue { get { return nu; } set { nu = value; } }
        int ny;
        public int nyValue { get { return ny; } set { ny = value; } }

        Matrix TA;
        public Matrix TaMatrix { get { return TA; }}

        int F;
        public int FValue { get { return F; } set { F = value; } }

        public FrmMatrix()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            CVs.SelectedIndex = 0;
            this.dataGridView1.ColumnCount = y;
            this.dataGridView1.ColumnHeadersVisible = true;

            // Set the column header style.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Beige;
            columnHeaderStyle.Font = new Font("Verdana", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Set the column header names.
            for (int i = 0; i < y; i++)
            {
                dataGridView1.Columns[i].Name = "No."+ Convert.ToString(i+1);

            }
            for (int i = 0; i < x; i++)
            {
                int index = this.dataGridView1.Rows.Add();
                for (int j = 0; j < y; j++)
                {
                    this.dataGridView1.Rows[index].Cells[j].Value = Convert.ToString(ABCD.Data[i, j]);
                }
            }
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                this.dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            TA = ABCD;
            this.Close();
        }

        private void Change_Click(object sender, EventArgs e)
        {
            TA = Matrix.Zeros(x, y);
            for (int i = 0; i < x; i++)
            {
                //int index = this.dataGridView1.Rows.Add();
                for (int j = 0; j < y; j++)
                {
                    TA[i, j] = Convert.ToDouble(this.dataGridView1.Rows[i].Cells[j].Value);
                }
            }
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double a = double.Parse(textBox1.Text);//系数
            int rowCount = this.dataGridView1.Rows.Count;         
            for (int i =CVs.SelectedIndex; i < rowCount - 1; i = i +CVs.Items.Count)
            {
            this.dataGridView1.Rows[i].Cells[i].Value = a * Convert.ToDouble(this.dataGridView1.Rows[i].Cells[i].Value);
            }              
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void FrmMatrix_FormClosing_1(object sender, FormClosingEventArgs e)
        {

            TA = Matrix.Zeros(x, y);
            for (int i = 0; i < x; i++)
            {
                //int index = this.dataGridView1.Rows.Add();
                for (int j = 0; j < y; j++)
                {
                    TA[i, j] = Convert.ToDouble(this.dataGridView1.Rows[i].Cells[j].Value);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowCount = this.dataGridView1.Rows.Count;
            for (int i =0; i < rowCount - 1; i = i + CVs.Items.Count)
            {
                this.dataGridView1.Rows[i].Cells[i].Value = 50;             
            }
            for (int i = 1; i < rowCount - 1; i = i + CVs.Items.Count)
            {
                this.dataGridView1.Rows[i].Cells[i].Value = 5;
            }
            for (int i = 2; i < rowCount - 1; i = i + CVs.Items.Count)
            {
                this.dataGridView1.Rows[i].Cells[i].Value = 5;
            }
        }
    }
}
