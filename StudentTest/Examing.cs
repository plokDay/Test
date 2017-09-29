using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Data.OleDb;


namespace StudentTest
{
    public partial class Examing : Form
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
        public Examing()
        {
            InitializeComponent();
        }
        public string _studentNum = ComVal.userAcc;//传递用户名
        OleDbConnection _conn = DataClass.DBCon();                   
        OleDbCommand _cmd;
        public static ArrayList al = new ArrayList();//存储随机产生的选择题ID
        public static ArrayList al1 = new ArrayList(); //存储随机产生的判断题ID
        int i = 0;//选择题计数
        int j = 0;//判断题计数
        public static string RightAns = "";//选择题正确答案
        public static string[] StudentAns;//考生的选择题答案
        public static string RightAns1 = "";//判断题正确答案
        public static string[] StudentAns1;//考生的判断题答案
        int examTime;//考试总时间
        int xzRat;//选择题比例
        int xzFz;//选择题分值
        int pdRat; //判断题比例
        int pdFz;//判断题分值
        int xzflag; //随机选择题数量
        int pdflag;//随机判断题数量
        int StuScore = 0;  //将选择题成绩初始化为0
        int StuScore1 = 0;  //将判断题成绩初始化为0

        private void Examing_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label5.BackColor = Color.Transparent;
            label6.BackColor = Color.Transparent;
            statusStrip1.BackColor = Color.Transparent;

            label1.Text += _studentNum;
            label2.Text = ComVal.userName;//用户真实姓名
            if (i == 0)
                toolStripButton1.Enabled = false;//如果是选择题第一道，禁用“上一题”
            if (j == 0)
                toolStripButton3.Enabled = false;//如果是判断题第一道，禁用“上一题”
            //获取考试总时间及相关信息
            _conn.Open();
            OleDbCommand cd = new OleDbCommand("select *  from ExamSet", _conn);
            OleDbDataReader sr = cd.ExecuteReader();
            sr.Read();
            examTime = Convert.ToInt32(sr[5].ToString()); //获取考试时间
            xzRat = Convert.ToInt32(sr[1].ToString());//获取选择题比例
            xzFz = Convert.ToInt32(sr[2].ToString());//选择题分值
            pdRat = Convert.ToInt32(sr[3].ToString()); //判断题比例
            pdFz = Convert.ToInt32(sr[4].ToString()); //判断题分值
            sr.Close();
            _conn.Close();
            toolStripStatusLabel1.Text = examTime.ToString(); //显示考试时间
            toolStripStatusLabel4.Text = examTime.ToString(); //显示剩余时间

            //随机产生选择题
      
            xzflag = xzRat / xzFz; //获取抽取选择题的数量
            _conn.Open(); 
            string strSQL1 = "select count(*) from TestInfo where testType=0"; //获取数据库中选择题数量
            OleDbCommand cmdx = new OleDbCommand(strSQL1, _conn);
            int jj = Convert.ToInt32(cmdx.ExecuteScalar()); //获取数据表中选择题的数量
            _conn.Close(); 
            //判断抽取试题的数量是否大于数据表中选择题的数量
            if (xzflag > jj)
            {
               MessageBox.Show ("提示：抽取试题数量大于数据库中选择题的数量，请联系管理员向数据库中添加选择题后再来答卷！");    
               this.Close(); //关闭当前窗体
            }
            //如果判断抽取试题的数量小于数据表中选择题的数量
            else
            {
                //根据抽取试题的数量实例化StudentAns
                StudentAns = new string[xzflag];
                for (int kk = 0; kk < StudentAns.Length; kk++) 
                {
                    StudentAns[kk] = "F";
                }
                if (xzRat == 0) //如果抽取试题数量为0
                {
                    toolStripLabel1.Text = "没有抽取试题！"; //显示“没有抽取试题”
                    toolStripButton1.Enabled = false;
                    toolStripButton2.Enabled = false;
                }
                else
                {
                    _conn.Open();
                    _cmd = new OleDbCommand("select top " + xzflag + " * from TestInfo where testType=0 order by right(cstr(rnd(-int(rnd(-timer())*100+ID)))*1000*Now(),2) ", _conn);
                    OleDbDataReader sdr = _cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        al.Add(sdr[0].ToString());  //循环将每道题的ID添加到al中
                        RightAns += sdr[3].ToString().Trim();//循环将每道题的正确答案赋值给RightAns
                    }
                    sdr.Close();
                    //根据ID检索数据
                    string sql = "select * from TestInfo where ID=" + Convert.ToInt32(al[0]);
                    OleDbCommand cmd1 = new OleDbCommand(sql, _conn);
                    OleDbDataReader sdr1 = cmd1.ExecuteReader();
                    sdr1.Read();
                    label3.Text = sdr1[2].ToString().Trim();  //显示试题题目
                    labelA.Text = sdr1[4].ToString(); //显示答案A
                    labelB.Text = sdr1[5].ToString();//显示答案B
                    labelC.Text = sdr1[6].ToString();//显示答案C
                    labelD.Text = sdr1[7].ToString();//显示答案D
                    _conn.Close();  
                    //显示试题数量
                   toolStripLabel1. Text = "[共有" + al.Count.ToString() + "道选择题]";
                   toolStripLabel2.Text = "当前第" + (i+1) + "道";

                }


             //随机产生判断题
         
             pdflag = pdRat / pdFz; //计算随机抽取判断题的数量
             _conn.Open(); 
             string strSQL2 = "select count(*) from TestInfo where testType=1";//获取数据库中判断题的数量
             OleDbCommand cmdm = new OleDbCommand(strSQL2, _conn);
             int dd = Convert.ToInt32(cmdm.ExecuteScalar());  //获取判断题数量
             _conn.Close(); //关闭连接
             //判断随机抽取试题的数量是否大于数据表中判断题的数量
            if (pdflag > dd)
            {
                MessageBox.Show("提示：抽取试题数量大于数据库中判断题的数量，请联系管理员向数据库中添加判断题后再来答卷！");
                this.Close();  //关闭当前窗体
            }
             //判断随机抽取试题的数量小于数据表中判断题的数量
            else
           {
              //根据随机抽取试题的数量实例化StudentAns1
              StudentAns1 = new string[pdflag];
              for (int mm = 0; mm < StudentAns1.Length; mm++)
              {
               StudentAns1[mm] = "F";
              }
              if (pdRat == 0)//如果抽取试题的数量为0
              {
                  toolStripLabel5.Text = "没有抽取试题！";//显示“没有抽取试题”
                  toolStripButton3.Enabled = false;
                  toolStripButton4.Enabled = false;
              }
              else  
              {
                  _conn.Open();  
                  //随机抽取判断题试题

                  OleDbCommand cmd2 = new OleDbCommand("select top " + pdflag + " * from TestInfo where testType=1 order by right(cstr(rnd(-int(rnd(-timer())*100+ID)))*1000*Now(),2)", _conn);
                  OleDbDataReader sdr2 = cmd2.ExecuteReader();
                  while (sdr2.Read())
                  {
                      al1.Add(sdr2[0].ToString()); 
                      //循环将每道题的正确答案赋值给RightAns1
                      RightAns1 += sdr2[3].ToString().Trim();
                  }
                  sdr2.Close();
                  //根据ID检索数据

                  string sql1 = "select * from TestInfo where ID=" + Convert.ToInt32(al1[0]);
                  OleDbCommand cmd3 = new OleDbCommand(sql1, _conn);
                  OleDbDataReader sdr3 = cmd3.ExecuteReader();
                  sdr3.Read();
                  label4.Text = sdr3[2].ToString().Trim(); //获取判断题题目
                  _conn.Close();  
                  toolStripLabel5.Text = "[共有" + al1.Count.ToString() + "道判断题]";//显示判断题数量
                  toolStripLabel6.Text = "当前第" + (j + 1) + "道";

              }

           }
            }
        }
  
        //选择题下一题按钮
        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            radioButtonA.Checked = false;//取消A的选择状态
            radioButtonB.Checked = false;//取消B的选择状态
            radioButtonC.Checked = false;//取消C的选择状态
            radioButtonD.Checked = false;//取消D的选择状态
            i++; 
            //如果到达最后索引则禁用“下一题”
            if (i == xzflag - 1)
                toolStripButton2.Enabled = false;
            else
                toolStripButton2.Enabled = true;
            //如果在数组的索引范围内
            if (i <= al.Count - 1)
            {
                toolStripButton1.Enabled = true;  //“上一题”按钮可用
                toolStripLabel2.Text = "当前第"+Convert.ToString(i + 1)+"题"; //显示当前试题数
                _conn.Open();  
                //根据ID检索数据
                string sql = "select * from TestInfo where ID=" + Convert.ToInt32(al[i]);
                OleDbCommand cmd1 = new OleDbCommand(sql, _conn);
                OleDbDataReader sdr1 = cmd1.ExecuteReader();
                sdr1.Read();
                label3.Text = sdr1[2].ToString();  //显示试题题目
                labelA.Text = sdr1[4].ToString(); //显示答案A
                labelB.Text = sdr1[5].ToString();//显示答案B
                labelC.Text = sdr1[6].ToString();//显示答案C
                labelD.Text = sdr1[7].ToString();//显示答案D
                sdr1.Close();
                _conn.Close();
            }
            else
            {
                toolStripButton2.Enabled = false;  //禁用“下一题”按钮
            }

        }
        //选择题上一题按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            radioButtonA.Checked = false;//取消A的选择状态
            radioButtonB.Checked = false;//取消B的选择状态
            radioButtonC.Checked = false;//取消C的选择状态
            radioButtonD.Checked = false;//取消D的选择状态
            i--;
            if (i >0||i==0)
            {
                if (i == 0)
                    toolStripButton1.Enabled = false;//如果是第一道，禁用“上一题”
                else
                    toolStripButton2.Enabled = true;
                toolStripLabel2.Text = "当前第" + Convert.ToString(i + 1) + "题"; //显示当前试题数
                _conn.Open();
                //根据ID检索数据
                string sql = "select * from TestInfo where ID=" + Convert.ToInt32(al[i]);
                OleDbCommand cmd1 = new OleDbCommand(sql, _conn);
                OleDbDataReader sdr1 = cmd1.ExecuteReader();
                sdr1.Read();
                label3.Text = sdr1[2].ToString();  //显示试题题目
                labelA.Text = sdr1[4].ToString(); //显示答案A
                labelB.Text = sdr1[5].ToString();//显示答案B
                labelC.Text = sdr1[6].ToString();//显示答案C
                labelD.Text = sdr1[7].ToString();//显示答案D
                sdr1.Close();
                _conn.Close();
            }

        }
        //判断题下一题按钮
        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            radioButTr.Checked = false;//取消选择
            radioButFa.Checked = false;//取消选择
            j++;
        
            //如果到达最后索引则禁用“下一题”
            if (j == pdflag - 1)
                toolStripButton4.Enabled = false;
            else
                toolStripButton4.Enabled = true;
            //如果在数组的索引范围内
            if (j < al1.Count - 1 || j == al1.Count - 1)
            {
                toolStripButton3.Enabled = true;  //“上一题”按钮可用
                toolStripLabel6.Text = "当前第" + Convert.ToString(j + 1) + "题"; //显示当前试题数
                _conn.Open();
                //根据ID检索数据
                string sqll = "select * from TestInfo where ID=" + Convert.ToInt32(al1[j]);
                OleDbCommand cmdd1 = new OleDbCommand(sqll, _conn);
                OleDbDataReader sdrr1 = cmdd1.ExecuteReader();
                sdrr1.Read();
                label4.Text = sdrr1[2].ToString();  //显示试题题目
                sdrr1.Close();
                _conn.Close();

            }
            else
            {
                toolStripButton4.Enabled = false;  //禁用“下一题”按钮
            }
        }
        //判断题上一题按钮
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            radioButTr.Checked = false;//取消选择
            radioButFa.Checked = false;//取消选择
            j--;
            if (j == pdflag - 1)
                toolStripButton4.Enabled = false;
            else
                toolStripButton4.Enabled = true;
            if (j > 0||j==0)
            {
                if (j == 0)
                    toolStripButton3.Enabled = false;//如果是第一道，禁用“上一题”
                else
                    toolStripButton3.Enabled = true;
                toolStripLabel6.Text = "当前第" + Convert.ToString(j + 1) + "题"; //显示当前试题数
                _conn.Open();
                
                //根据ID检索数据
                string sql = "select * from TestInfo where ID=" + Convert.ToInt32(al1[j]);
                OleDbCommand c = new OleDbCommand(sql, _conn);
                OleDbDataReader sddrr1 = c.ExecuteReader();
                sddrr1.Read();
                label4.Text = sddrr1[2].ToString();  //显示试题题目
                sddrr1.Close();
                _conn.Close();
            }

         }

        int flag = 1;    //初始化变量flag为1
        int nowTime = 1;  //记录已用时间
        Random ran = new Random();
        //判断时间
        private void timer1_Tick(object sender, EventArgs e)
        {
           
            toolStripStatusLabel2.Text = flag.ToString(); //显示当前已用时间
            if (toolStripStatusLabel2.Text == "60") //判断flag是否可以整除60
            {
                //如果可以整除60则说明已经过了1分钟
                toolStripStatusLabel3.Text = nowTime.ToString();
                //剩余时间
                toolStripStatusLabel4.Text = (examTime - nowTime).ToString();   
                flag = 0;
                nowTime++;//计算已用时间
            }

            if (toolStripStatusLabel4.Text == "5")
            {
                label7.Text = "还有5分钟交卷";
                hanbutt.BackColor = Color.FromArgb(ran.Next(0, 256), ran.Next(0, 256), ran.Next(0, 256));
            }
            else
            {
                label7.Text = "";
                
            }
            if (toolStripStatusLabel4.Text == "0")                                         //判断剩余时间是否为0
            {
                //如果剩余时间为0，则停止Timer控件并强制提交试卷

                hanbutt_Click(sender, e);

            }
            flag++;
        }

        //提交函数
        private void Handin()
        {

            //提交选择题
            string stuAns = "";
            for (int aa = 0; aa < StudentAns.Length; aa++)  //获取考生答案
            {
                stuAns += StudentAns[aa].ToString();
            }
            for (int i = 0; i < xzflag; i++)
            {
                //将考生答案与正确答案做比较
                if (RightAns.Substring(i, 1).Equals(stuAns.Substring(i, 1)))
                {
                    StuScore += xzFz; //如果答案正确加分
                }
            }
            //提交判断题
            string pdstuAns = "";
            for (int aa = 0; aa < StudentAns1.Length; aa++) //获取考生答案
            {
                pdstuAns += StudentAns1[aa].ToString();
            }
            for (int i = 0; i < pdflag; i++)
            {
                //将考生答案与正确答案做比较
                if (RightAns1.Substring(i, 1).Equals(pdstuAns.Substring(i, 1)))
                {
                    StuScore1 += pdFz; //如果答案正确加分
                }
            }
            int allscore = StuScore + StuScore1;  //计算总分
            //将考试分数上传数据库
            _conn.Open();
            _cmd = new OleDbCommand("update ExamResult set selRes='" + StuScore + "',judRes='" + StuScore1 + "', allRes='"+allscore+"' where userID='" + _studentNum + "'", _conn);
            
            if (_cmd.ExecuteNonQuery() > 0)
                _conn.Close();
            else 
            {
                _cmd = new OleDbCommand("insert into ExamResult(userID,selRes,judRes,allRes,userName) values('"
                   + _studentNum + "','" + StuScore + "','" + StuScore1 + "','" + allscore + "','" + label2.Text + "')", _conn);
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
            string sql = "update UserInfo set IsTest=1 where userAcc='" + _studentNum + "'";
            OleDbConnection conn = DataClass.DBCon();
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        //如果选A
        private void radioButtonA_CheckedChanged(object sender, EventArgs e)
        {
          if (radioButtonA.Checked == true)    
           {
              StudentAns[i] = "A"; //将答案“A”添加到数组中
           }
        }
        //B
        private void radioButtonB_CheckedChanged(object sender, EventArgs e)
        {
          if (radioButtonB.Checked == true)    
           {
              StudentAns[i] = "B"; //将答案“A”添加到数组中
           }
        }
        //C
        private void radioButtonC_CheckedChanged(object sender, EventArgs e)
        {
          if (radioButtonC.Checked == true)    
           {
              StudentAns[i] = "C"; //将答案“A”添加到数组中
           }
        }
        //D
        private void radioButtonD_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonD.Checked == true)    
           {
              StudentAns[i] = "D"; //将答案“A”添加到数组中
           }
        }
        //正确
        private void radioButTr_CheckedChanged(object sender, EventArgs e)
        {
             if (radioButTr.Checked == true) 
           {
             StudentAns1[j] = "1";//将答案“1”添加到数组中
            }

        }
        //错误
        private void radioButFa_CheckedChanged(object sender, EventArgs e)
        {
             if (radioButFa.Checked == true) 
           {
             StudentAns1[j] = "0";//将答案“1”添加到数组中
            }
        }
        //提交按钮
        private void hanbutt_Click(object sender, EventArgs e)
        {
            //判断剩余时间是否为0
            if (toolStripStatusLabel4.Text.Trim() == "0")
            {
                Handin();
                Form1.ex.Exam_Load(sender, e);
                Form1.ex.checkIsTest();
                Form1.ex.Show();//打开Exam窗体
              
                this.Close();//关闭本窗体
            }
            else                                                          //否则，说明是考生自己提交考卷
            {
                if (MessageBox.Show("提示：您真的要提交考卷吗？", "提示", MessageBoxButtons.OKCancel,
         MessageBoxIcon.Exclamation) == DialogResult.OK)                             //弹出提示
                {
                    timer1.Stop(); //停止Timer控件
                    Handin();
                    //声明SQL语句，用于更新用户数据表中的IsTest字段，标记用户是否参加过考试

                    Form1.ex.Exam_Load(sender, e);
                    Form1.ex.checkIsTest();
                    Form1.ex.Show();//打开Exam窗体
                    this.Close();//关闭本窗体
                }

            }
        }

        private void Examing_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            this.Cursor = System.Windows.Forms.Cursors.Hand;//改变鼠标样式
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }
 
    }
}
