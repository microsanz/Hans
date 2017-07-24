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
        string command = "";
        DataTable oDT= new DataTable();
        OleDbDataAdapter oDA=new OleDbDataAdapter();


        public Master_Customer()
        {
            InitializeComponent();
        }
        private void showAll()
        {
            command = "select CustomerCode as 'Kode Customer', CustomerName as 'Nama Customer', CustomerAddress as 'Alamat Customer' from Customer";
            oDA = new OleDbDataAdapter(command, Connection.oleConn);
            oDA.Fill(oDT);
            dataGridView1.DataSource = oDT;
        }
        private void Master_Customer_Load(object sender, EventArgs e)
        {
            showAll();
        }
    }
}
