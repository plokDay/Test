using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace StudentTest
{
    public partial class QueSc : Form
    {
        #region 使窗体可以移动的代码
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        #endregion
        public QueSc()
        {
            InitializeComponent();
        }

        private void QueSc_Load(object sender, EventArgs e)
        {
            string sql ="select userName as 考生姓名,userID as 准考证号 ,selRes as 选择题,judRes as 判断题,allRes as 总分  from ExamResult ";
            DataClass.DataGridViewBing(dataGridView1, sql);
            //设置控件的底色透明
            groupBox1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
        }
        //查询按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || textBox1.Text == "")
                MessageBox.Show("请输入完整数据！");
            else
            {
                string sql;
                if (comboBox1.Text == "姓名")
                    sql = "select userName as 考生姓名,userID as 准考证号 ,selRes as 选择题,judRes as 判断题,allRes as 总分 from ExamResult where userName like'%" + textBox1.Text + "%'";
                else
                    sql = "select userName as 考生姓名,userID as 准考证号 ,selRes as 选择题,judRes as 判断题,allRes as 总分 from ExamResult where userID='" + textBox1.Text + "'";
                OleDbConnection con = DataClass.DBCon();
                con.Open();
                OleDbCommand com = new OleDbCommand(sql,con);
                OleDbDataReader re = com.ExecuteReader();
                //re.Read();
                if (re.Read() == false)
                {
                    MessageBox.Show("没有查询结果");
                    con.Close();
                }
                else
                {
                    DataClass.DataGridViewBing(dataGridView1, sql);
                }
            }
        }
        //退出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QueSc_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
