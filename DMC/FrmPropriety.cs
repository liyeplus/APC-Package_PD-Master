using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMC
{
    public partial class FrmPropriety : Form
    {
        string item;
        string value;
        string opcitem;
        string sv;
        string max;
        string min;
        string dmax;
        string dmin;
        public FrmPropriety(string a,string b,string c,string d,string e,string f,string g,string h)
        {
            InitializeComponent();
            item = a; this.ItemBox.Text = item;
            value = b; this.ValueBox.Text = value;
            opcitem = c; this.OPCItemBox.Text = opcitem;
            sv = d; this.SvBox.Text = sv;
            max = e; this.MaxBox.Text = max;
            min = f; this.MinBox.Text = min;
            dmax = g; this.dMaxBox.Text = dmax;
            dmin = h; this.dMinBox.Text = dmin;
        }
    }
}
