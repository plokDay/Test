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
    public partial class StuManage : Form
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
        public StuManage()
        {
            InitializeComponent();
            this.Text = "考生管理";
        }

        public  void StuManage_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;

            string sql = "select userName as 考生姓名,userSex as 考生性别,userAcc as 准考证号 ,isTest as 是否考试  from UserInfo where userIde=0";
            DataClass.DataGridViewBing(dataGridView1, sql);
            dataGridView1.AllowUserToAddRows = false;//禁止显示最后一行
           
        }
        public string StuNA;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 )
            {//如果没有选中考生，禁用右键
                contextMenuStrip1.Enabled = false;
            }
            else
            {
                contextMenuStrip1.Enabled = true;
               
                StuNA = dataGridView1.SelectedCells[0].Value.ToString(); //获取考生姓名
                string sql = "select * from UserInfo where userName='" + StuNA + "'";
                OleDbConnection con = DataClass.DBCon();
                con.Open();
                OleDbCommand com = new OleDbCommand(sql, con);
                OleDbDataReader re = com.ExecuteReader();
                if (re.Read())
                {
                    //传递选中考生数据
                    label1.Text = re[2].ToString();
                    if (re[6].ToString() != "")
                    {

                        pictureBox1.ImageLocation = re[6].ToString();

                    }
                    else
                    {
                        switch (label1.Text)
                        {
                            case "段隆婵":

                                pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231841;break;

                            case "武嘉敏":
                                    pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231946;break;

                            case"邵美琪":
                                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_c47071d89f960255ae10e2015ccdfc52;break;

                            case"潘昱荣":
                                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_3215a9af47ca2c38584ba19b2ca72599;break;
                            default: pictureBox1.Image=null;break;
                        }
                       
                    }
                   
                    re.Close();
                    con.Close();
                }

            }
            
        }
        //实现右键删除考生信息的功能
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string  MMid = dataGridView1.SelectedCells[0].Value.ToString(); //获取考生姓名
            OleDbConnection con = DataClass.DBCon();
            con.Open();
            OleDbCommand com = new OleDbCommand("delete * from ExamResult where userName='" + MMid + "'", con) ;
            com.ExecuteNonQuery();//如果该考生有成绩记录，则一起删除
            con.Close();
            string sql = "delete * from UserInfo where userName='" + MMid + "' ";//声明SQL语句用于删除
            DataClass.OperaData(sql);//调用DeleteData方法
            StuManage_Load(sender, e);//刷新当前窗体
        }
        //修改
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComVal.StuNA = dataGridView1.SelectedCells[0].Value.ToString(); //获取考生姓名
            string sql = "select * from UserInfo where userName='" + ComVal.StuNA + "'";
            OleDbConnection con = DataClass.DBCon();
            con.Open();
            OleDbCommand com = new OleDbCommand(sql, con);
            OleDbDataReader re = com.ExecuteReader();
            if (re.Read())
            {
                //传递选中考生数据
                ComVal.StuSex = re[7].ToString();
                ComVal.StuID = re[3].ToString();
                ComVal.Iste = Convert.ToInt32(re[5].ToString());
                ComVal.StuPw = re[4].ToString();
                ComVal.PicPath = re[6].ToString();
                re.Close();
                con.Close();
                //修改窗体出现
               
                SthInf st = new SthInf();
                st.Show();
                this.Hide();//隐藏本窗体
            }
           
        }
        //增加
        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SthInf st = new SthInf();
            st.Show();
            this.Hide();//隐藏本窗体
        }
        //退出按钮
        private void 退出_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StuManage_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
        
    }
}
