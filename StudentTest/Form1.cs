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
    public partial class Form1 : Form
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
        public Form1()
        {
            InitializeComponent();
        }

        public static Admin ad=new Admin ();//声明静态全局变量 管理员窗口实例
        public static Exam ex = new Exam();//声明静态全局变量 考试窗口实例
        private void LogButt_Click(object sender, EventArgs e)
        {
            //判断是否选择登陆身份
            if (useIdeTextBox.Text == "")
                MessageBox.Show("请选择登陆身份！");
            else
            {
                //判断是否输入用户名和密码
                if (accTextBox.Text == "" || pwdTxtBox.Text == "")
                {
                    MessageBox.Show("请输入用户名和密码！");
                }
                else
                {
                    //如果是考生
                    if (useIdeTextBox.Text == "考生")
                    {
                        //判断用户名密码是否符合
                        OleDbConnection con = DataClass.DBCon();
                        con.Open();
                        OleDbCommand com = new OleDbCommand(
                            "select * from UserInfo where userAcc='" + accTextBox.Text + "' and userPwd='" + pwdTxtBox.Text + "' and userIde=0", con);
                        OleDbDataReader red = com.ExecuteReader();
                        red.Read();
                        if (red.HasRows)
                        {
                           
                            ComVal.userPwd = pwdTxtBox.Text;
                            ComVal.userAcc = accTextBox.Text;
                            Exam._Useracc = ComVal.userAcc;//传递用户名
                            ex.Show();//显示考试窗口
                            this.Hide();
                          
                        }
                        else
                            MessageBox.Show("用户名或密码错误！");
                        con.Close();
                       
                    }
                    else
                    {
                        //如果不是学生，则判断管理员的身份是否符合
                        OleDbConnection con = DataClass.DBCon();
                        con.Open();
                        OleDbCommand com = new OleDbCommand(
                            "select * from UserInfo where userIde=1 and userAcc='" + accTextBox.Text + "' and userPwd='" + pwdTxtBox.Text + "'", con);
                        OleDbDataReader red = com.ExecuteReader();
                        red.Read();
                        if (red.HasRows)
                        {
                           
                            //上传用户名密码
                            ComVal.userPwd = pwdTxtBox.Text;
                            ComVal.userAcc = accTextBox.Text;
                            ComVal.userName = red[2].ToString();
                            ad.Show();//显示管理员窗口
                            this.Hide();
                        }
                        else
                            MessageBox.Show("用户名或密码错误！");
                        con.Close();
                       
                    }

                }

            }
        }
        //清除按钮
        private void button1_Click(object sender, EventArgs e)
        {
            accTextBox.Clear();
            pwdTxtBox.Clear();
        }
        //退出按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

     

    }
}
