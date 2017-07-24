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
    public partial class Master_Product : Form
    {
        OleDbCommand command;
        DataTable oDT = new DataTable();
        OleDbDataAdapter oDA = new OleDbDataAdapter();
        public Master_Product()
        {
            InitializeComponent();
        }
        private void showAll()
        {
            oDT = new DataTable();
            command = new OleDbCommand("select ProductCode as 'Kode Produk', ProductName as 'Nama Produk', ProductQuantity as 'Jumlah Produk', ProductPrice as 'Harga Produk' from Product", Connection.getConnection());
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDT);
            dataGridView1.DataSource = oDT;
        }
        private void ClearField()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            oDT = new DataTable();
            command = new OleDbCommand("select ProductCode as 'Kode Produk', ProductName as 'Nama Produk', ProductQuantity as 'Jumlah Produk', ProductPrice as 'Harga Produk' from Product where ProductCode like @ProductCode", Connection.getConnection());
            command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = "%" + textBox1.Text + "%";
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDT);
            dataGridView1.DataSource = oDT;
        }

        private void Master_Product_Load(object sender, EventArgs e)
        {
            showAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (oDT.Rows.Count == 0)
            {
                Connection.Open();
                command = new OleDbCommand("insert into Product values( @ProductCode, @ProductName, @ProductQty, @ProductPrice)", Connection.getConnection());
                command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox1.Text;
                command.Parameters.Add("@ProductName", OleDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@ProductQty", OleDbType.VarChar).Value = textBox3.Text;
                command.Parameters.Add("@ProductPrice", OleDbType.VarChar).Value = textBox4.Text;
                command.ExecuteNonQuery();
                Connection.Close();
            }
            else
            {
                if (oDT.Rows[0][0].ToString() == textBox1.Text)
                {
                    Connection.Open();
                    command = new OleDbCommand("update Product set ProductName = @ProductName , ProductQuantity = @ProductQty, ProductPrice = @ProductPrice where ProductCode = @ProductCode", Connection.getConnection());
                    command.Parameters.Add("@ProductName", OleDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@ProductQty", OleDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@ProductPrice", OleDbType.VarChar).Value = textBox4.Text;
                    command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
                else
                {
                    Connection.Open();
                    command = new OleDbCommand("insert into Product values( @ProductCode, @ProductName, @ProductQty, @ProductPrice)", Connection.getConnection());
                    command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.Parameters.Add("@ProductName", OleDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@ProductQty", OleDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@ProductPrice", OleDbType.VarChar).Value = textBox4.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            ClearField();
            showAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (oDT.Rows.Count == 1 && oDT.Rows[0][0].ToString() == textBox1.Text)
            {
                Connection.Open();
                command = new OleDbCommand("delete from Product where ProductCode = @ProductCode", Connection.getConnection());
                command.Parameters.Add("@ProductCode", OleDbType.VarChar).Value = textBox1.Text;
                command.ExecuteNonQuery();
                Connection.Close();
            }
            ClearField();
            showAll();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = oDT.Rows[e.RowIndex][0].ToString();
            textBox2.Text = oDT.Rows[0][1].ToString();
            textBox3.Value = Convert.ToInt32(oDT.Rows[0][2].ToString());
            textBox4.Value = Convert.ToInt32(oDT.Rows[0][3].ToString());
        }
    }
}
