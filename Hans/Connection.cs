using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Hans
{
    class Connection
    {
        private static OleDbConnection oleConn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + AppDomain.CurrentDomain.BaseDirectory+ "\\POS.accdb");
        public static OleDbConnection getConnection()
        {
            return oleConn;
        }
        public static void Open()
        {
            oleConn.Open();
        }
        public static void Close()
        {
            oleConn.Close();
        }

    }
}
