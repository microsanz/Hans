using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master_Customer form=new Master_Customer();
            form.TopLevel = false;
            form.Parent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void productToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Master_Product form = new Master_Product();
            form.TopLevel = false;
            form.Parent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }

        private void penjualanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Transaksi_Penjualan form = new Transaksi_Penjualan();
            form.TopLevel = false;
            form.Parent = this;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}
