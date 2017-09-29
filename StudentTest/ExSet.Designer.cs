namespace StudentTest
{
    partial class ExSet
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.coXR = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.coX = new System.Windows.Forms.ComboBox();
            this.coPR = new System.Windows.Forms.ComboBox();
            this.coP = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.texT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "选择题比例：";
            // 
            // coXR
            // 
            this.coXR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coXR.FormattingEnabled = true;
            this.coXR.Items.AddRange(new object[] {
            "100",
            "75",
            "50",
            "0"});
            this.coXR.Location = new System.Drawing.Point(92, 51);
            this.coXR.Name = "coXR";
            this.coXR.Size = new System.Drawing.Size(67, 20);
            this.coXR.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "选择题分值：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "选择题分值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(196, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "判断题比例：";
            // 
            // coX
            // 
            this.coX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coX.FormattingEnabled = true;
            this.coX.Items.AddRange(new object[] {
            "10",
            "5"});
            this.coX.Location = new System.Drawing.Point(92, 103);
            this.coX.Name = "coX";
            this.coX.Size = new System.Drawing.Size(67, 20);
            this.coX.TabIndex = 5;
            // 
            // coPR
            // 
            this.coPR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coPR.FormattingEnabled = true;
            this.coPR.Items.AddRange(new object[] {
            "0",
            "25",
            "50",
            "100"});
            this.coPR.Location = new System.Drawing.Point(270, 51);
            this.coPR.Name = "coPR";
            this.coPR.Size = new System.Drawing.Size(70, 20);
            this.coPR.TabIndex = 6;
            // 
            // coP
            // 
            this.coP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.coP.FormattingEnabled = true;
            this.coP.Items.AddRange(new object[] {
            "5",
            "10"});
            this.coP.Location = new System.Drawing.Point(270, 103);
            this.coP.Name = "coP";
            this.coP.Size = new System.Drawing.Size(70, 20);
            this.coP.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "考试总时长：";
            // 
            // texT
            // 
            this.texT.Location = new System.Drawing.Point(106, 152);
            this.texT.Name = "texT";
            this.texT.Size = new System.Drawing.Size(105, 21);
            this.texT.TabIndex = 9;
            this.texT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.texT_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "分钟";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(92, 204);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(221, 204);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(346, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("华文隶书", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 20);
            this.label9.TabIndex = 15;
            this.label9.Text = "考试设置";
            // 
            // ExSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::StudentTest.Properties.Resources.timg25ba;
            this.ClientSize = new System.Drawing.Size(395, 244);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.texT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.coP);
            this.Controls.Add(this.coPR);
            this.Controls.Add(this.coX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.coXR);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ExSet";
            this.Text = "ExSet";
            this.Load += new System.EventHandler(this.ExSet_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ExSet_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox coXR;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox coX;
        private System.Windows.Forms.ComboBox coPR;
        private System.Windows.Forms.ComboBox coP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox texT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}