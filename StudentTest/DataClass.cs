using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data.OleDb;


namespace StudentTest
{
   public  class DataClass
    {
        //数据库连接函数
        public static OleDbConnection DBCon()
        {
            string con = "provider=microsoft.Jet.OLEDB.4.0;";
            con += "Data Source=";
            con += Application.StartupPath;
            con += "\\student1.mdb";
            OleDbConnection mycon = new OleDbConnection(con);
            return mycon;
        }
        //绑定DataGridView
        public static void  DataGridViewBing(DataGridView dgv, string strSQL)
        {
            OleDbConnection conn = DBCon();
            OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
            DataSet se=new DataSet ();
            da.Fill (se);
            
            dgv.DataSource =se.Tables[0];
        }
        //数据库操作函数
        public static void OperaData(string  strSQL)
        {
            OleDbConnection conn = DBCon();
            conn.Open();
            OleDbCommand inscom = new OleDbCommand(strSQL,conn);
           
            if (inscom.ExecuteNonQuery()> 0)
                MessageBox.Show("操作成功");
            //else MessageBox.Show("操作失败");
            conn.Close();
        }
        
    }
}
