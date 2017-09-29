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
    public partial class AltJud : Form
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
        public AltJud()
        {
            InitializeComponent();
        }
        int j;
        private void AltJud_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            if (ComVal.TeID != 0)
            {//说明是修改的操作
                if (ComVal.seR == "1")
                    comboBox1.Text = "正确";
                else comboBox1.Text = "错误";
                textBox1.Text = ComVal.seTi;
            }
          
        }
        //提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""||comboBox1.Text=="")
                MessageBox.Show("请输入完整信息！");
            else
            { 
                if(comboBox1.Text=="正确")
                     j=1;
                else  j=0;
                if (ComVal.TeID == 0)
                {
                    //说明是增加题目的操作
                    string sql = "insert into TestInfo(TestType,question,rightKey) values(1,'" + textBox1.Text + "','" + j + "')";
                    DataClass.OperaData(sql);
                }
                else
                {
                    //说明是修改的操作
                    string sql = "update TestInfo set question='" + textBox1.Text + "',rightKey='" +
                        j + "' where ID=" + ComVal.TeID + "";
                    DataClass.OperaData(sql);
                    ComVal.TeID = 0;//修改之后释放ComVal.TeID，方便再次操作
                }
                textBox1.Clear();
                Form1.ad.Show();//打开隐藏的管理员窗口
                Form1.ad.Admin_Load(sender, e);//刷新
                this.Close();
            }
        }
        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            ComVal.TeID = 0;//释放
            Form1.ad.Show();//打开隐藏的管理员窗口
            this.Close();
        }

        private void AltJud_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
