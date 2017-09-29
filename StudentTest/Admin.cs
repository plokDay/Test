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
using System.Data.OleDb;
namespace StudentTest
{
    public partial class Admin : Form
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
        public Admin()
        {
            InitializeComponent();
            this.ControlBox = false;//禁用原来的控制按钮窗体
        }

        public static StuManage stum ;//声明静态公有变量 
        public void Admin_Load(object sender, EventArgs e)
        {
            stum = new StuManage();//创建”学生管理“窗体的实例
            menuStrip1.BackColor = Color.Transparent;
            statusStrip1.BackColor = Color.Transparent;//设置底色透明
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;//禁止显示最后一行
            toolStripStatusLabel1.Text = "姓名：" + ComVal.userName + "   身份：管理员";
            string sql =
                "select ID as 系统编号,question as 选择题题目,A as 选项A ,B as 选项B ,C as 选项C ,D as 选项D ,rightKey as 正确选项 from TestInfo where testType=0";
            DataClass.DataGridViewBing(dataGridView1, sql);//显示选择题
            string sql1 = "select ID as 系统编号,question as 判断题题目,rightKey as 真假判断 from TestInfo where testType=1";
            DataClass.DataGridViewBing(dataGridView2, sql1);//显示判断题

            //设置dataGridView的显示大小
            dataGridView1.Columns[1].Width = dataGridView1.Columns[2].Width = dataGridView1.Columns[3].Width = dataGridView1.Columns[4].Width = dataGridView1.Columns[5].Width=170;
            dataGridView2.Columns[0].Width = 80;//设置第一列宽度
            dataGridView2.Columns[1].Width = 910;//设置第二例宽度
            dataGridView2.Columns[2].Width = 80;//设置第二例宽度
        }

        //实现右键删除选中选择题的功能
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int MMid = Convert.ToInt32(dataGridView1.SelectedCells[0].Value); //获取选择试题的ID值
                string sql = "delete from TestInfo where ID=" + MMid + "";//声明SQL语句用于删除
                DataClass.OperaData(sql);//调用DeleteData方法
                Admin_Load(sender, e);//刷新当前窗体
        }
        //实现右键修改选中选择题的功能
        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                int MMid = Convert.ToInt32(dataGridView1.SelectedCells[0].Value); //获取选择试题的ID值
                string sql = "select * from TestInfo where ID=" + MMid + "";
                OleDbConnection con = DataClass.DBCon();
                con.Open();
                OleDbCommand com = new OleDbCommand(sql, con);
                OleDbDataReader re = com.ExecuteReader();
                re.Read();
                //传递选择题数据
                ComVal.TeID = MMid; 
                ComVal.seTi = re[2].ToString();
                ComVal.seR = re[3].ToString();
                ComVal.seA = re[4].ToString();
                ComVal.seB = re[5].ToString();
                ComVal.seC = re[6].ToString();
                ComVal.seD = re[7].ToString();
                re.Close();
                con.Close();
                //选择题窗体出现
                AltSel se = new AltSel();
                se.Show();
                this.Hide();//隐藏本窗体
        }
        //实现右键添加选择题的功能
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltSel se = new AltSel();
            se.Show();
            this.Hide();//隐藏本窗体
        }
        //实现右键删除选中判断题的功能
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
                int MMid = Convert.ToInt32(dataGridView2.SelectedCells[0].Value); //获取选择试题的ID值
                string sql = "delete from TestInfo where ID=" + MMid + "";//声明SQL语句用于删除
                DataClass.OperaData(sql);//调用DeleteData方法
                Admin_Load(sender, e);//刷新当前窗体
        }
        //实现右键修改选中判断题的功能
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
                int MMid = Convert.ToInt32(dataGridView2.SelectedCells[0].Value); //获取选择试题的ID值
                string sql = "select * from TestInfo where ID=" + MMid + "";
                OleDbConnection con = DataClass.DBCon();
                con.Open();
                OleDbCommand com = new OleDbCommand(sql, con);
                OleDbDataReader re = com.ExecuteReader();
                re.Read();
                //传递选择题数据
                ComVal.TeID = MMid; 
                ComVal.seTi = re[2].ToString();
                ComVal.seR = re[3].ToString();
                re.Close();
                con.Close();
                //选择题窗体出现
                AltJud se = new AltJud();
                se.Show();
                this.Hide();//隐藏本窗体
            
        }
        //“添加判断”按钮
        private void 添加ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AltJud ju = new AltJud();
            ju.Show();
            this.Hide();//隐藏本窗体
        }

        //“考试设置”按钮
        private void 考试设置ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ExSet se = new ExSet();
            se.Show();
        }
        //查询成绩
        private void 考试设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueSc sc = new QueSc();
            sc.Show();
            
        }
       //帮助按钮
        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("所有的窗口，点选任意地区可移动\r管理员界面：\r在考生管理和试题管理中，\r单机选中一行完整的数据，\r可对数据右键进行增、删、改操作");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 )
            { //如果没有选中，禁用选择题右键
                contextMenuStrip1.Enabled = false;
            }
            else contextMenuStrip1.Enabled = true;
            if (dataGridView2.SelectedRows.Count == 0)
            { //如果没有选中，禁用判断题右键
                contextMenuStrip2.Enabled = false;
            }
            else contextMenuStrip2.Enabled = true;
        }
        //考生管理
        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stum.Show();
        }
        //退出按钮
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();//关闭应用程序
        }

        private void Admin_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

       
 

 
    
    }
}
