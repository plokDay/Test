using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices; 

namespace StudentTest
{
    public partial class SthInf : Form
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
        public SthInf()
        {
            InitializeComponent();
            this.Text = "考生信息";
           
        }

        private void SthInf_Load(object sender, EventArgs e)
        {
            //更改控件背景为透明
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            label7.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
            if (ComVal.StuNA != "")
            {//这是修改的操作
                textBox1.Enabled = false;
                comboBox2.Enabled = false;

                textBox1.Text = ComVal.StuNA;
                if (textBox1.Text == "段隆婵")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231841;
                if (textBox1.Text == "武嘉敏")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.IMG_20170621_231946;
                if (textBox1.Text == "邵美琪")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_c47071d89f960255ae10e2015ccdfc52;
                if (textBox1.Text == "潘昱荣")
                    pictureBox1.Image = global::StudentTest.Properties.Resources.img_3215a9af47ca2c38584ba19b2ca72599;
                if (ComVal.StuSex == "男")
                    comboBox2.Text = comboBox2.Items[0].ToString();
                else comboBox2.Text = comboBox2.Items[1].ToString();
                textBox2.Text = ComVal.StuID;
                if (ComVal.Iste == 0)
                    comboBox1.Text = comboBox1.Items[1].ToString();
                else comboBox1.Text = comboBox1.Items[0].ToString();
                textBox3.Text = ComVal.StuPw; pictureBox1.ImageLocation = ComVal.PicPath;
            }
        }
        //”提交“按钮
        private void button1_Click(object sender, EventArgs e)
        {
            int isTe;
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox2.Text == "" || comboBox1.Text == "")
                MessageBox.Show("请输入完整信息！");
            else
            {
                if (comboBox2.Text == "已参加过考试")
                    isTe = 1;
                else isTe = 0;

                if (ComVal.StuNA == "")
                {  //s说明是添加的操作
                  
                    string sql = "insert into UserInfo(userIde,userName,userAcc,userPwd,isTest,userSex,picPath) values(0,'"
                           + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'," + isTe + ",'" + comboBox1.Text + "','"+ComVal.PicPath+"')";
                    DataClass.OperaData(sql);
                }
                else
                { 
                    //说明这是修改的操作
                    //修改成功后，释放ComVal中存放的相关数据，方便再次操作
                    ComVal.StuNA = "";
                  
                    string sql = "update UserInfo set userAcc='" + textBox2.Text + "',userPwd='"
                        + textBox3.Text + "',isTest=" + isTe + ",userSex='" + comboBox1.Text + "',picPath='"+ComVal.PicPath+"' where userName='"+textBox1.Text+"'";
                    DataClass.OperaData(sql);
                    
                }
                StuManage stum = new StuManage();
                Admin.stum.Show();//打开之前隐藏的”学生管理“窗体
                Admin.stum.StuManage_Load(sender, e);//刷新
                this.Close();

            }
        }
        //”取消“按钮
        private void button2_Click(object sender, EventArgs e)
        {
            ComVal.StuNA = "";//释放之前选中得到的值
            Admin.stum.Show();//打开之前隐藏的”学生管理“窗体
            this.Close();
        }
        //上传或更改图片
        private void label6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            openFileDialog1.Title = "选择上传的图片";
            openFileDialog1.Filter = "图片格式|*.jpg";
            openFileDialog1.Multiselect = false;//设置不能多选
            //如果点击”确定“
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ComVal.PicPath = openFileDialog1.FileName;//获得图片路径
                pictureBox1.ImageLocation = ComVal.PicPath;
            }
        }

        private void SthInf_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        
    }
}
