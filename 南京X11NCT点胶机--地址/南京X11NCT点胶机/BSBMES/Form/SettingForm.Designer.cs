namespace UpperComputer
{
    partial class SettingForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.功能参数设置 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_胶水A编码长度 = new System.Windows.Forms.TextBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_ModeName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ck_检测NG放行 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Len = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ck_Scan = new System.Windows.Forms.CheckBox();
            this.txt_Station0 = new System.Windows.Forms.TextBox();
            this.串口设置 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_删除串口 = new System.Windows.Forms.Button();
            this.textBox_串口名称 = new System.Windows.Forms.TextBox();
            this.button_添加 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.panel_串口 = new System.Windows.Forms.Panel();
            this.串口列表 = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.功能参数设置.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.串口设置.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.功能参数设置);
            this.tabControl1.Controls.Add(this.串口设置);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1187, 632);
            this.tabControl1.TabIndex = 0;
            // 
            // 功能参数设置
            // 
            this.功能参数设置.Controls.Add(this.groupBox4);
            this.功能参数设置.Controls.Add(this.btn_Exit);
            this.功能参数设置.Controls.Add(this.btn_Save);
            this.功能参数设置.Controls.Add(this.groupBox2);
            this.功能参数设置.Controls.Add(this.groupBox1);
            this.功能参数设置.Location = new System.Drawing.Point(4, 22);
            this.功能参数设置.Name = "功能参数设置";
            this.功能参数设置.Size = new System.Drawing.Size(1179, 606);
            this.功能参数设置.TabIndex = 1;
            this.功能参数设置.Text = "功能参数设置";
            this.功能参数设置.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txt_胶水A编码长度);
            this.groupBox4.Location = new System.Drawing.Point(7, 193);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(266, 184);
            this.groupBox4.TabIndex = 36;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "物料校验";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "胶水编码条码长度;";
            // 
            // txt_胶水A编码长度
            // 
            this.txt_胶水A编码长度.Location = new System.Drawing.Point(124, 20);
            this.txt_胶水A编码长度.Name = "txt_胶水A编码长度";
            this.txt_胶水A编码长度.Size = new System.Drawing.Size(125, 21);
            this.txt_胶水A编码长度.TabIndex = 34;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(622, 523);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 33);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "取消";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(397, 523);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 33);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_ModeName);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(279, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 184);
            this.groupBox2.TabIndex = 33;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配方切换";
            // 
            // cmb_ModeName
            // 
            this.cmb_ModeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ModeName.FormattingEnabled = true;
            this.cmb_ModeName.Items.AddRange(new object[] {
            "E171"});
            this.cmb_ModeName.Location = new System.Drawing.Point(42, 20);
            this.cmb_ModeName.Name = "cmb_ModeName";
            this.cmb_ModeName.Size = new System.Drawing.Size(228, 20);
            this.cmb_ModeName.TabIndex = 25;
            this.cmb_ModeName.SelectedIndexChanged += new System.EventHandler(this.cmb_ModeName_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 24;
            this.label6.Text = "配方:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ck_检测NG放行);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_Len);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ck_Scan);
            this.groupBox1.Controls.Add(this.txt_Station0);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 184);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "功能设置";
            // 
            // ck_检测NG放行
            // 
            this.ck_检测NG放行.AutoSize = true;
            this.ck_检测NG放行.Location = new System.Drawing.Point(108, 24);
            this.ck_检测NG放行.Name = "ck_检测NG放行";
            this.ck_检测NG放行.Size = new System.Drawing.Size(84, 16);
            this.ck_检测NG放行.TabIndex = 36;
            this.ck_检测NG放行.Text = "检测NG放行";
            this.ck_检测NG放行.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "条码长度;";
            // 
            // txt_Len
            // 
            this.txt_Len.Location = new System.Drawing.Point(70, 112);
            this.txt_Len.Name = "txt_Len";
            this.txt_Len.Size = new System.Drawing.Size(179, 21);
            this.txt_Len.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "注：屏蔽扫码后会启用下方固定条码存储数据";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 12);
            this.label7.TabIndex = 32;
            this.label7.Text = "固定条码;";
            // 
            // ck_Scan
            // 
            this.ck_Scan.AutoSize = true;
            this.ck_Scan.Location = new System.Drawing.Point(8, 25);
            this.ck_Scan.Name = "ck_Scan";
            this.ck_Scan.Size = new System.Drawing.Size(72, 16);
            this.ck_Scan.TabIndex = 27;
            this.ck_Scan.Text = "屏蔽扫码";
            this.ck_Scan.UseVisualStyleBackColor = true;
            this.ck_Scan.CheckedChanged += new System.EventHandler(this.ck_Scan_CheckedChanged);
            // 
            // txt_Station0
            // 
            this.txt_Station0.Location = new System.Drawing.Point(70, 73);
            this.txt_Station0.Name = "txt_Station0";
            this.txt_Station0.Size = new System.Drawing.Size(179, 21);
            this.txt_Station0.TabIndex = 30;
            this.txt_Station0.TextChanged += new System.EventHandler(this.txt_Statiton0_TextChanged);
            // 
            // 串口设置
            // 
            this.串口设置.Controls.Add(this.groupBox3);
            this.串口设置.Controls.Add(this.panel_串口);
            this.串口设置.Controls.Add(this.串口列表);
            this.串口设置.Location = new System.Drawing.Point(4, 22);
            this.串口设置.Margin = new System.Windows.Forms.Padding(2);
            this.串口设置.Name = "串口设置";
            this.串口设置.Padding = new System.Windows.Forms.Padding(2);
            this.串口设置.Size = new System.Drawing.Size(1179, 606);
            this.串口设置.TabIndex = 2;
            this.串口设置.Text = "串口设置";
            this.串口设置.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_删除串口);
            this.groupBox3.Controls.Add(this.textBox_串口名称);
            this.groupBox3.Controls.Add(this.button_添加);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(6, 326);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(170, 97);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // button_删除串口
            // 
            this.button_删除串口.Location = new System.Drawing.Point(90, 53);
            this.button_删除串口.Margin = new System.Windows.Forms.Padding(2);
            this.button_删除串口.Name = "button_删除串口";
            this.button_删除串口.Size = new System.Drawing.Size(56, 25);
            this.button_删除串口.TabIndex = 4;
            this.button_删除串口.Text = "删除";
            this.button_删除串口.UseVisualStyleBackColor = true;
            this.button_删除串口.Click += new System.EventHandler(this.button_删除串口_Click);
            // 
            // textBox_串口名称
            // 
            this.textBox_串口名称.Location = new System.Drawing.Point(70, 18);
            this.textBox_串口名称.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_串口名称.Name = "textBox_串口名称";
            this.textBox_串口名称.Size = new System.Drawing.Size(77, 21);
            this.textBox_串口名称.TabIndex = 1;
            // 
            // button_添加
            // 
            this.button_添加.Location = new System.Drawing.Point(12, 53);
            this.button_添加.Margin = new System.Windows.Forms.Padding(2);
            this.button_添加.Name = "button_添加";
            this.button_添加.Size = new System.Drawing.Size(56, 25);
            this.button_添加.TabIndex = 3;
            this.button_添加.Text = "添加";
            this.button_添加.UseVisualStyleBackColor = true;
            this.button_添加.Click += new System.EventHandler(this.button_添加_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 26);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "串口名称:";
            // 
            // panel_串口
            // 
            this.panel_串口.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_串口.Location = new System.Drawing.Point(201, 6);
            this.panel_串口.Margin = new System.Windows.Forms.Padding(2);
            this.panel_串口.Name = "panel_串口";
            this.panel_串口.Size = new System.Drawing.Size(525, 416);
            this.panel_串口.TabIndex = 4;
            // 
            // 串口列表
            // 
            this.串口列表.ColumnWidth = 226;
            this.串口列表.Font = new System.Drawing.Font("宋体", 10F);
            this.串口列表.FormattingEnabled = true;
            this.串口列表.Location = new System.Drawing.Point(6, 6);
            this.串口列表.Margin = new System.Windows.Forms.Padding(2, 4, 2, 2);
            this.串口列表.MultiColumn = true;
            this.串口列表.Name = "串口列表";
            this.串口列表.Size = new System.Drawing.Size(170, 303);
            this.串口列表.TabIndex = 0;
            this.串口列表.Click += new System.EventHandler(this.串口列表_Click);
            this.串口列表.SelectedIndexChanged += new System.EventHandler(this.串口列表_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(1, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1187, 632);
            this.panel2.TabIndex = 2;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 629);
            this.Controls.Add(this.panel2);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "参数设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.功能参数设置.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.串口设置.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.TabPage 功能参数设置;
        private System.Windows.Forms.ComboBox cmb_ModeName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ck_Scan;
        private System.Windows.Forms.TextBox txt_Station0;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage 串口设置;
        private System.Windows.Forms.ListBox 串口列表;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_串口名称;
        private System.Windows.Forms.Button button_添加;
        private System.Windows.Forms.Panel panel_串口;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_删除串口;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Len;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_胶水A编码长度;
        private System.Windows.Forms.CheckBox ck_检测NG放行;
    }
}