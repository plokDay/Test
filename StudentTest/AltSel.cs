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
    public partial class AltSel : Form
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
        public AltSel()
        {
            InitializeComponent();
            this.Text = "选择题";
        }
        

        //提交按钮
        private void button1_Click(object sender, EventArgs e)
        {
            //判断信息是否输入完整
            if (textR.Text.Trim() == "" || textD.Text.Trim() == "" || textA.Text.Trim() == "" || textB.Text.Trim() == "" || textC.Text.Trim() == "" || textTi.Text.Trim() == "")
            {
              MessageBox.Show("请将信息填写完整！");
            }
            else  
            {
                //如果信息输入完整
                if (ComVal.TeID == 0)
                {
                    //说明是增加题目的操作
                    string sql = "insert into TestInfo(TestType,question,rightKey,A,B,C,D) values(0,'" + textTi.Text.Trim() + "','"
                        + textR.Text.Trim().ToUpper() + "','" + textA.Text.Trim() + "','" + textB.Text.Trim() + "','" + textC.Text.Trim() + "','" + textD.Text.Trim() + "')";
                    DataClass.OperaData(sql);
                }
                else
                { 
                    //说明是修改的操作
                    string sql = "update TestInfo set question='" + textTi.Text + "',rightKey='" + 
                        textR.Text + "',A='" + textA.Text + "',B='" + textB.Text + "',C='" + textC.Text + "',D='" + textD.Text + "' where ID="+ComVal.TeID+"";
                    DataClass.OperaData(sql);
                    ComVal.TeID = 0;//修改之后释放ComVal.TeID，方便再次操作
                }
                //清空
                textA.Clear();
                textB.Clear();
                textC.Clear();
                textD.Clear();
                textR.Clear();
                textTi.Clear();
                Form1.ad.Show();//打开隐藏的管理员窗口
                Form1.ad.Admin_Load(sender,e);//刷新
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

        private void AltSel_Load(object sender, EventArgs e)
        {
            //设置控件背景透明
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            groupBox2.BackColor = Color.Transparent;
            groupBox3.BackColor = Color.Transparent;
           
            if (ComVal.TeID != 0)
            {//说明是修改的操作，加载原来的数据
                textTi.Text = ComVal.seTi;
                textR.Text = ComVal.seR;
                textA.Text = ComVal.seA;
                textB.Text = ComVal.seB;
                textC.Text = ComVal.seC;
                textD.Text = ComVal.seD;
            }
         
        }
        //限制选择题答案文本框只能输入abcdABCD
        private void textR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(e.KeyChar <= 'D' && e.KeyChar >= 'A' || e.KeyChar <= 'd' && e.KeyChar >= 'a') && e.KeyChar != '\r' && e.KeyChar != '\b')
            {
                e.Handled = true;   //处理KeyPress事件
            }


        }

        private void AltSel_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

     
    }
}
