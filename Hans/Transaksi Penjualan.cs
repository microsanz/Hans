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
        List<ItemModel> listItemModel = new List<ItemModel>();


        OleDbCommand command;
        DataTable oDTHelper = new DataTable();
        DataTable oDTDetail = new DataTable();
        OleDbDataAdapter oDA = new OleDbDataAdapter();
        int focusParam = 0;
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
        private void showProduct()
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select ProductCode as 'Kode Produk', ProductName as 'Nama Produk', ProductQuantity as 'Jumlah Produk', ProductPrice as 'Harga Produk' from Product", Connection.getConnection());
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            dataGridView1.DataSource = oDTHelper;
        }
        private void hideHelper()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
        }
        public void ClearField()
        {
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            textBox3.Text = "";
            textBox6.Text = "";
            numericUpDown1.Value = 0;
            textBox4.Text = "0";
            textBox7.Text = "0";
            oDTDetail = new DataTable();
            oDTDetail.Columns.Add(new DataColumn("Kode Produk"));
            oDTDetail.Columns.Add(new DataColumn("Jumlah Produk"));
            oDTDetail.Columns.Add(new DataColumn("Harga Per Item"));
            oDTDetail.Columns.Add(new DataColumn("Total Harga"));
            oDTHelper = new DataTable();
            dataGridView1.DataSource = oDTHelper;
            dataGridView2.DataSource = oDTDetail;
            focusParam = 0;
        }
        private void Transaksi_Penjualan_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            showPenjualan();
            oDTDetail.Columns.Add(new DataColumn("Kode Produk"));
            oDTDetail.Columns.Add(new DataColumn("Jumlah Produk"));
            oDTDetail.Columns.Add(new DataColumn("Harga Per Item"));
            oDTDetail.Columns.Add(new DataColumn("Total Harga"));
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            hideHelper();
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
            hideHelper();
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
            }
            else if (focusParam == 1)
            {
                textBox3.Text = oDTHelper.Rows[e.RowIndex][0].ToString();
            }
            else if (focusParam == 2)
            {
                textBox6.Text = oDTHelper.Rows[e.RowIndex][0].ToString();
                textBox4.Text = oDTHelper.Rows[e.RowIndex][3].ToString();
                textBox7.Text = (numericUpDown1.Value * Convert.ToInt32(textBox4.Text)).ToString();
            }
            hideHelper();
        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            hideHelper();
            showProduct();
            focusParam = 2;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox7.Text = (numericUpDown1.Value * Convert.ToInt32(textBox4.Text)).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            oDTDetail.Rows.Add(textBox6.Text, numericUpDown1.Value.ToString(), textBox4.Text, textBox7.Text);
            dataGridView2.DataSource = oDTDetail;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select SaleCode from Sale where SaleCode = @SaleCode", Connection.getConnection());
            command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);
            if (oDTHelper.Rows.Count > 0)
            {
                if (oDTHelper.Rows[0][0].ToString() == textBox1.Text)
                {
                    Connection.Open();
                    command = new OleDbCommand("delete from SaleDetail where SaleCode = @SaleCode", Connection.getConnection());
                    command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = "%" + textBox1.Text + "%";
                    command.ExecuteNonQuery();
                    Connection.Close();

                    Connection.Open();
                    command = new OleDbCommand("update Sale set SaleDate = @SaleDate, CustomerCode = @CustomerCode where SaleCode = @SaleCode", Connection.getConnection());
                    command.Parameters.Add("@SaleDate", OleDbType.Date).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();

                    for (int i = 0; i < oDTHelper.Rows.Count; i++)
                    {
                        Connection.Open();
                        command = new OleDbCommand("insert into SaleDetail values (@SaleCode, @ProductCode, @ProductQuantity)", Connection.getConnection());
                        command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
                        command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox6.Text;
                        command.Parameters.Add("@ProductQuantity", OleDbType.Numeric).Value = numericUpDown1.Value;
                        command.ExecuteNonQuery();
                        Connection.Close();
                    }
                }
                else
                {
                    Connection.Open();
                    command = new OleDbCommand("insert into Sale values (@SaleCode, @SaleDate, @CustomerCode)", Connection.getConnection());
                    command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.Parameters.Add("@SaleDate", OleDbType.VarChar).Value = dateTimePicker1.Value;
                    command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox3.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();

                    for (int i = 0; i < oDTHelper.Rows.Count; i++)
                    {
                        Connection.Open();
                        command = new OleDbCommand("insert into SaleDetail values (@SaleCode, @ProductCode, @ProductQuantity)", Connection.getConnection());
                        command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
                        command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox6.Text;
                        command.Parameters.Add("@ProductQuantity", OleDbType.Numeric).Value = numericUpDown1.Value;
                        command.ExecuteNonQuery();
                        Connection.Close();
                    }
                }
            }
            ClearField();
            showPenjualan();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            oDTDetail = new DataTable();
            oDTDetail.Columns.Add(new DataColumn("Kode Produk"));
            oDTDetail.Columns.Add(new DataColumn("Jumlah Produk"));
            oDTDetail.Columns.Add(new DataColumn("Harga Per Item"));
            oDTDetail.Columns.Add(new DataColumn("Total Harga"));
            dataGridView2.DataSource = oDTHelper;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            oDTHelper = new DataTable();
            command = new OleDbCommand("select SaleCode from Sale where SaleCode = @SaleCode", Connection.getConnection());
            command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = textBox1.Text;
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDTHelper);

            if (oDTHelper.Rows[0][0].ToString() == textBox1.Text)
            {
                Connection.Open();
                command = new OleDbCommand("delete from Sale where SaleCode = @SaleCode", Connection.getConnection());
                command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = "%" + textBox1.Text + "%";
                command.ExecuteNonQuery();
                Connection.Close();

                Connection.Open();
                command = new OleDbCommand("delete from SaleDetail where SaleCode = @SaleCode", Connection.getConnection());
                command.Parameters.Add("@SaleCode", OleDbType.VarChar).Value = "%" + textBox1.Text + "%";
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }
    }
}
