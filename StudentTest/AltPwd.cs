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
    public partial class AltPwd : Form
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
        public AltPwd()
        {
            InitializeComponent();
            this.Text= "修改密码";
        }
        //提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text == "")
                MessageBox.Show("请输入旧密码！");
            if (textBox1.Text.Trim() != ComVal.userPwd)
                MessageBox.Show("密码错误！");
            if (textBox1.Text.Trim() == ComVal.userPwd)
            {
                string strAlt = "update UserInfo set userPwd='" + textBox2.Text + "' where userAcc='"+ComVal.userAcc+"'";
                DataClass.OperaData(strAlt);
                ComVal.userPwd = textBox2.Text;
                this.Close();
            }
        }
        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AltPwd_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
        }

        private void AltPwd_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

    }
}
