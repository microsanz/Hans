using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Hans
{
    public partial class Transaksi_Penjualan : Form
    {
        OleDbCommand command;
        DataTable oDTHelper = new DataTable();
        DataTable oDTDetail = new DataTable();
        OleDbDataAdapter oDA = new OleDbDataAdapter();
        int focusParam=0;
        public Transaksi_Penjualan()
        {
            InitializeComponent();
        }
        private void showPenjualan()
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select SaleCode as 'Kode Penjualan', SaleDate as 'Tanggal Penjualan', CustomerCode as 'Kode Customer' from Sale", Connection.getConnection());
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            dataGridView1.DataSource = oDTHelper;
        }
        private void showCustomer()
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select CustomerCode as 'Kode Customer', CustomerName as 'Nama Customer', CustomerAddress as 'Alamat Customer' from Customer", Connection.getConnection());
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            dataGridView1.DataSource = oDTHelper;
        }
        private void hideHelper()
        {
            dataGridView1.DataSource = new DataTable();
        }
        private void Transaksi_Penjualan_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            showPenjualan();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            showPenjualan();
            focusParam = 0;
        }



        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select CustomerCode as 'Kode Customer', CustomerName as 'Nama Customer', CustomerAddress as 'Alamat Customer' from Customer where CustomerCode like @CustomerCode", Connection.getConnection());
            //OleDbParameter param = new OleDbParameter("@CustomerCode", OleDbType.VarChar);
            //param.Value =textBox1.Text;
            command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = "%" + textBox3.Text + "%";
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            dataGridView1.DataSource = oDTHelper;
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            showCustomer();
            focusParam = 1;
        }
        private void dateTimePicker1_Enter(object sender, EventArgs e)
        {
            hideHelper();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select SaleCode as 'Kode Penjualan', SaleDate as 'Tanggal Penjualan' , CustomerCode as 'Kode Customer' from Sale where SaleCode like @SaleCode", Connection.getConnection());
            //OleDbParameter param = new OleDbParameter("@CustomerCode", OleDbType.VarChar);
            //param.Value =textBox1.Text;
            command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = "%" + textBox1.Text + "%";
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            dataGridView1.DataSource = oDTHelper;
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (focusParam == 0)
            {
                textBox1.Text = oDTHelper.Rows[e.RowIndex][0].ToString();
            }else if (focusParam == 1)
            {
                textBox3.Text = oDTHelper.Rows[e.RowIndex][0].ToString();
            }
            hideHelper();
        }
    }
}
