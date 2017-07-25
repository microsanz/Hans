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
    public partial class Master_Customer : Form
    {
        OleDbCommand command;
        DataTable oDT= new DataTable();
        OleDbDataAdapter oDA=new OleDbDataAdapter();


        public Master_Customer()
        {
            InitializeComponent();
        }
        private void showAll()
        {
            oDT = new DataTable();
            command = new OleDbCommand("select CustomerCode as 'Kode Customer', CustomerName as 'Nama Customer', CustomerAddress as 'Alamat Customer' from Customer",Connection.getConnection());
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDT);
            dataGridView1.DataSource = oDT;
        }
        private void ClearField()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void Master_Customer_Load(object sender, EventArgs e)
        {
            showAll();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            oDT = new DataTable();
            command = new OleDbCommand("select CustomerCode as 'Kode Customer', CustomerName as 'Nama Customer', CustomerAddress as 'Alamat Customer' from Customer where CustomerCode like @CustomerCode", Connection.getConnection());
            //OleDbParameter param = new OleDbParameter("@CustomerCode", OleDbType.VarChar);
            //param.Value =textBox1.Text;
            command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value="%"+textBox1.Text+"%";
            oDA = new OleDbDataAdapter(command);
            oDA.Fill(oDT);
            dataGridView1.DataSource = oDT;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(oDT.Rows.Count==0)
            {
                Connection.Open();
                command = new OleDbCommand("insert into Customer values( @CustomerName, @CustomerAddress, @CustomerCode)",Connection.getConnection());
                command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox1.Text;
                command.Parameters.Add("@CustomerName", OleDbType.VarChar).Value = textBox2.Text;
                command.Parameters.Add("@CustomerAddress", OleDbType.VarChar).Value = textBox3.Text;
                command.ExecuteNonQuery();
                Connection.Close();
            }
            else
            {
                if (oDT.Rows[0][0].ToString() == textBox1.Text)
                {
                    Connection.Open();
                    command = new OleDbCommand("update Customer set CustomerName = @CustomerName , CustomerAddress = @CustomerAddress where CustomerCode = @CustomerCode", Connection.getConnection());
                    command.Parameters.Add("@CustomerName", OleDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@CustomerAddress", OleDbType.VarChar).Value = textBox3.Text;
                    command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
                else
                {
                    Connection.Open();
                    command = new OleDbCommand("insert into Customer values( @CustomerName, @CustomerAddress, @CustomerCode)", Connection.getConnection());
                    command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox1.Text;
                    command.Parameters.Add("@CustomerName", OleDbType.VarChar).Value = textBox2.Text;
                    command.Parameters.Add("@CustomerAddress", OleDbType.VarChar).Value = textBox3.Text;
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
            }
            ClearField();
            showAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(oDT.Rows.Count == 1 && oDT.Rows[0][0].ToString() == textBox1.Text)
            {
                Connection.Open();
                command = new OleDbCommand("delete from Customer where CustomerCode = @CustomerCode", Connection.getConnection());
                command.Parameters.Add("@CustomerCode", OleDbType.VarChar).Value = textBox1.Text;
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
            textBox3.Text = oDT.Rows[0][2].ToString();
        }
    }
}
