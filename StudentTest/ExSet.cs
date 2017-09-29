using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace StudentTest
{
    public partial class ExSet : Form
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
        public ExSet()
        {
            InitializeComponent();
            this.Text = "考试设置";
        }
        //读取原本的数据
        private void ExSet_Load(object sender, EventArgs e)
        {
            label9.BackColor = Color.Transparent;
            OleDbConnection con = DataClass.DBCon();
            con.Open();
            string sql = "select * from ExamSet where ID=1";
            OleDbCommand com = new OleDbCommand(sql, con);
            OleDbDataReader re = com.ExecuteReader();
            re.Read();
            coXR.Text = re[1].ToString();
            coX.Text = re[2].ToString();
            coPR.Text = re[3].ToString();
            coP.Text = re[4].ToString();
            texT.Text = re[5].ToString();
            con.Close();
        }
        //提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            if (coP.Text == "" || coPR.Text == "" || coX.Text == "" || coXR.Text == "")
                MessageBox.Show("请输入完整信息！");
            else 
            {
                if (int.Parse(coXR.Text) + int.Parse(coPR.Text) != 100)
                    MessageBox.Show("请输入正确信息！");
                else
                {
                    string sql = "update ExamSet set selRate='" + coXR.Text + "',selPre='"
                        + coX.Text + "',judRate='" + coPR.Text + "',judPre='" + coP.Text + "',ExamTime='"+texT.Text.Trim()+"'";
                    DataClass.OperaData(sql);
                    this.Close();//关闭本窗体
                    Form1.ad.Admin_Load(sender, e);//刷新
                }
            }
            
        }
        //考试时间只能输入0~9
        private void texT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar <= '9' && e.KeyChar >= '0') && e.KeyChar != '\r' && e.KeyChar != '\b')
            {
                e.Handled = true;     
            }
        }
        //取消按钮
        private void button2_Click(object sender, EventArgs e)
        {
            Form1.ad.Show();//打开隐藏的管理员窗口
            this.Close();
        }

        private void ExSet_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
    }
}
