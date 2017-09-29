using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace StudentTest
{
    public partial class Exam : Form
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
        public  static  string _Useracc = "";//获得考生的准考证号
        public  OleDbConnection _conn = DataClass.DBCon();//连接数据库
        
        public Exam()
        {
            InitializeComponent();
          
        }

        public void Exam_Load(object sender, EventArgs e)
        {
            checkBox1.BackColor = Color.Transparent;
            menuStrip1.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            _conn.Open();
            //根据用户名加载相应考生的照片和真实姓名
            OleDbCommand com = new OleDbCommand("select * from UserInfo where userAcc='" + _Useracc + "'", _conn);
            OleDbDataReader re = com.ExecuteReader();
            if (re.Read())
            {
                if (re[5].ToString() == "0")
                    查询分数ToolStripMenuItem1.Enabled = false;
                label1.Text = re[2].ToString();
                if (label1.Text == "段隆婵")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231841;
                if (label1.Text == "武嘉敏")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231946;
                if (label1.Text == "邵美琪")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_c47071d89f960255ae10e2015ccdfc52;
                if (label1.Text == "潘昱荣")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_3215a9af47ca2c38584ba19b2ca72599;
                pictureBox1.ImageLocation = @"" + re[6].ToString() + "";
               
                ComVal.userName = re[2].ToString();
            }
            _conn.Close();
            checkIsTest();
           
        }

        //根据登录用户名查看是否参加考试
        public  void checkIsTest()                                                 
        {
            _conn.Open();                
            OleDbCommand cmd = new OleDbCommand("select isTest from UserInfo where userAcc='" + _Useracc + "'", _conn);
            int  flag =Convert.ToInt32( cmd.ExecuteScalar());
           
            if (flag == 0)                                                       
            {
                //如果flag值为0
                //则说明没有参加考试，显示“开始考试”，禁用“查询分数”命令
                button2.Visible = true;
                查询分数ToolStripMenuItem1.Enabled = false;
            }
            else                                                                      
            {
                //如果flag值不为0
                //则说明已经参加过考试，显示“查询分数”禁用“开始考试”命令,并显示离开考场
                查询分数ToolStripMenuItem1.Enabled = true;
                button2.Visible = false;
                listBox1.Visible = false;
                checkBox1.Visible = false;
            }
            _conn.Close();
        
        }
    
        //修改密码
        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltPwd alp = new AltPwd();
            alp.Show();
        }
        //查询分数
        private void 查询分数ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //根据用户名检索,查询分数
            string sql = "select * from ExamResult where userID='" + _Useracc + "'";
            _conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql, _conn);
            OleDbDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string xz = sdr[2].ToString(); //获得选择题分数
                string pd = sdr[3].ToString();//获得判断题分数
                string all = sdr[4].ToString();//获得总分 
                //弹出提示的字符串
                string mess = sdr[5].ToString() + "你好，您的考分如下：\n" + "选择题得分：" + xz + "\n" + "判断题得分：" + pd + "\n" + "最后总分为：" + all;
                MessageBox.Show(mess);
            }
            _conn.Close();
        }

        //退出按钮
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出应用程序
        }
        //开始考试按钮
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();//隐藏本窗口
            Examing exing = new Examing();
            exing = new Examing();
            exing.Show();//打开正在考试窗口
        }
        //判断是否阅读考生须知
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button2.Visible = true;
            button2.Enabled = true;
            //checkIsTest();//检查该考生是否考过
        }
        //帮助
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("所有的窗口，点选任意地区可以移动\r考生界面：\r点击“我已阅读考生说明”后，\r才能考试每人只能考一次，\r考过能查分考试一旦开始，不能退出");
        }

        private void Exam_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

    }
}
