
namespace UpperComputer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_PLCStatus = new System.Windows.Forms.Button();
            this.btn_CCD1 = new System.Windows.Forms.Button();
            this.btn_CCD0 = new System.Windows.Forms.Button();
            this.btn_Scan = new System.Windows.Forms.Button();
            this.btn_Setting = new System.Windows.Forms.Button();
            this.btn_Inital = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ModeName = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button_mesState = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label_制令单 = new System.Windows.Forms.Label();
            this.uiLine1 = new Sunny.UI.UILine();
            this.uiLine3 = new Sunny.UI.UILine();
            this.uiRichTextBox2 = new Sunny.UI.UIRichTextBox();
            this.uiRichTextBox1 = new Sunny.UI.UIRichTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_MESSetting = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_PLCStatus);
            this.panel1.Controls.Add(this.btn_CCD1);
            this.panel1.Controls.Add(this.btn_CCD0);
            this.panel1.Controls.Add(this.btn_Scan);
            this.panel1.Location = new System.Drawing.Point(3, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 269);
            this.panel1.TabIndex = 5;
            // 
            // btn_PLCStatus
            // 
            this.btn_PLCStatus.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_PLCStatus.FlatAppearance.BorderSize = 0;
            this.btn_PLCStatus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_PLCStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_PLCStatus.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PLCStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_PLCStatus.Location = new System.Drawing.Point(16, 15);
            this.btn_PLCStatus.Name = "btn_PLCStatus";
            this.btn_PLCStatus.Size = new System.Drawing.Size(116, 38);
            this.btn_PLCStatus.TabIndex = 7;
            this.btn_PLCStatus.Text = "PLC离线";
            this.btn_PLCStatus.UseVisualStyleBackColor = false;
            // 
            // btn_CCD1
            // 
            this.btn_CCD1.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_CCD1.FlatAppearance.BorderSize = 0;
            this.btn_CCD1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_CCD1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CCD1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CCD1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_CCD1.Location = new System.Drawing.Point(16, 148);
            this.btn_CCD1.Name = "btn_CCD1";
            this.btn_CCD1.Size = new System.Drawing.Size(116, 38);
            this.btn_CCD1.TabIndex = 11;
            this.btn_CCD1.Text = "CCD2离线";
            this.btn_CCD1.UseVisualStyleBackColor = false;
            // 
            // btn_CCD0
            // 
            this.btn_CCD0.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_CCD0.FlatAppearance.BorderSize = 0;
            this.btn_CCD0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_CCD0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CCD0.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CCD0.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_CCD0.Location = new System.Drawing.Point(16, 103);
            this.btn_CCD0.Name = "btn_CCD0";
            this.btn_CCD0.Size = new System.Drawing.Size(116, 38);
            this.btn_CCD0.TabIndex = 11;
            this.btn_CCD0.Text = "CCD1离线";
            this.btn_CCD0.UseVisualStyleBackColor = false;
            // 
            // btn_Scan
            // 
            this.btn_Scan.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_Scan.FlatAppearance.BorderSize = 0;
            this.btn_Scan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Scan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Scan.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Scan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Scan.Location = new System.Drawing.Point(16, 59);
            this.btn_Scan.Name = "btn_Scan";
            this.btn_Scan.Size = new System.Drawing.Size(116, 38);
            this.btn_Scan.TabIndex = 8;
            this.btn_Scan.Text = "扫码连接";
            this.btn_Scan.UseVisualStyleBackColor = false;
            // 
            // btn_Setting
            // 
            this.btn_Setting.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Setting.FlatAppearance.BorderSize = 0;
            this.btn_Setting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Setting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Setting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Setting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Setting.Location = new System.Drawing.Point(16, 214);
            this.btn_Setting.Name = "btn_Setting";
            this.btn_Setting.Size = new System.Drawing.Size(116, 31);
            this.btn_Setting.TabIndex = 24;
            this.btn_Setting.Text = "参数设置";
            this.btn_Setting.UseVisualStyleBackColor = false;
            this.btn_Setting.Click += new System.EventHandler(this.btn_Setting_Click);
            // 
            // btn_Inital
            // 
            this.btn_Inital.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Inital.FlatAppearance.BorderSize = 0;
            this.btn_Inital.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Inital.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Inital.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Inital.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Inital.Location = new System.Drawing.Point(16, 253);
            this.btn_Inital.Name = "btn_Inital";
            this.btn_Inital.Size = new System.Drawing.Size(116, 31);
            this.btn_Inital.TabIndex = 12;
            this.btn_Inital.Text = "初始化";
            this.btn_Inital.UseVisualStyleBackColor = false;
            this.btn_Inital.Click += new System.EventHandler(this.btn_Inital_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Add.FlatAppearance.BorderSize = 0;
            this.btn_Add.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Add.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Add.Location = new System.Drawing.Point(16, 177);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(116, 31);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "点位监控";
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(606, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 53;
            this.label2.Text = "***";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(546, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "时  间:";
            // 
            // btn_ModeName
            // 
            this.btn_ModeName.BackColor = System.Drawing.Color.AliceBlue;
            this.btn_ModeName.FlatAppearance.BorderSize = 0;
            this.btn_ModeName.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_ModeName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ModeName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ModeName.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ModeName.Location = new System.Drawing.Point(108, 3);
            this.btn_ModeName.Name = "btn_ModeName";
            this.btn_ModeName.Size = new System.Drawing.Size(94, 32);
            this.btn_ModeName.TabIndex = 51;
            this.btn_ModeName.Text = "***";
            this.btn_ModeName.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.AliceBlue;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(10, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 32);
            this.button1.TabIndex = 50;
            this.button1.Text = "当前配方:";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button_mesState
            // 
            this.button_mesState.AutoSize = true;
            this.button_mesState.BackColor = System.Drawing.Color.AliceBlue;
            this.button_mesState.FlatAppearance.BorderSize = 0;
            this.button_mesState.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button_mesState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_mesState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_mesState.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_mesState.Location = new System.Drawing.Point(208, 3);
            this.button_mesState.Name = "button_mesState";
            this.button_mesState.Size = new System.Drawing.Size(116, 32);
            this.button_mesState.TabIndex = 8;
            this.button_mesState.Text = "MES连接状态";
            this.button_mesState.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(341, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 54;
            this.label4.Text = "制令单：";
            // 
            // label_制令单
            // 
            this.label_制令单.AutoSize = true;
            this.label_制令单.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_制令单.Location = new System.Drawing.Point(400, 14);
            this.label_制令单.Name = "label_制令单";
            this.label_制令单.Size = new System.Drawing.Size(49, 14);
            this.label_制令单.TabIndex = 55;
            this.label_制令单.Text = "label5";
            // 
            // uiLine1
            // 
            this.uiLine1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiLine1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine1.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiLine1.Location = new System.Drawing.Point(646, 425);
            this.uiLine1.Margin = new System.Windows.Forms.Padding(2);
            this.uiLine1.MinimumSize = new System.Drawing.Size(12, 13);
            this.uiLine1.Name = "uiLine1";
            this.uiLine1.Size = new System.Drawing.Size(508, 20);
            this.uiLine1.TabIndex = 93;
            this.uiLine1.Text = "系统日志";
            this.uiLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLine3
            // 
            this.uiLine3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiLine3.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLine3.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.uiLine3.Location = new System.Drawing.Point(176, 423);
            this.uiLine3.Margin = new System.Windows.Forms.Padding(2);
            this.uiLine3.MinimumSize = new System.Drawing.Size(12, 13);
            this.uiLine3.Name = "uiLine3";
            this.uiLine3.Size = new System.Drawing.Size(451, 20);
            this.uiLine3.TabIndex = 94;
            this.uiLine3.Text = "报警记录";
            this.uiLine3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiRichTextBox2
            // 
            this.uiRichTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiRichTextBox2.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiRichTextBox2.Location = new System.Drawing.Point(646, 451);
            this.uiRichTextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiRichTextBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox2.Name = "uiRichTextBox2";
            this.uiRichTextBox2.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox2.ShowText = false;
            this.uiRichTextBox2.Size = new System.Drawing.Size(508, 225);
            this.uiRichTextBox2.Style = Sunny.UI.UIStyle.Custom;
            this.uiRichTextBox2.TabIndex = 91;
            this.uiRichTextBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiRichTextBox1
            // 
            this.uiRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uiRichTextBox1.FillColor = System.Drawing.Color.White;
            this.uiRichTextBox1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.uiRichTextBox1.Location = new System.Drawing.Point(178, 451);
            this.uiRichTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.uiRichTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiRichTextBox1.Name = "uiRichTextBox1";
            this.uiRichTextBox1.Padding = new System.Windows.Forms.Padding(2);
            this.uiRichTextBox1.ShowText = false;
            this.uiRichTextBox1.Size = new System.Drawing.Size(451, 225);
            this.uiRichTextBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiRichTextBox1.TabIndex = 92;
            this.uiRichTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(176, 76);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(978, 335);
            this.panel6.TabIndex = 90;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button7);
            this.groupBox1.Controls.Add(this.button6);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.btn_MESSetting);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.btn_Setting);
            this.groupBox1.Controls.Add(this.btn_Inital);
            this.groupBox1.Location = new System.Drawing.Point(4, 358);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 303);
            this.groupBox1.TabIndex = 95;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(16, 27);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(116, 26);
            this.button7.TabIndex = 42;
            this.button7.Text = "用户注销";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(16, 95);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(116, 26);
            this.button6.TabIndex = 41;
            this.button6.Text = "用户管理";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(16, 62);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 26);
            this.button4.TabIndex = 40;
            this.button4.Text = "用户登录";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_MESSetting
            // 
            this.btn_MESSetting.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_MESSetting.FlatAppearance.BorderSize = 0;
            this.btn_MESSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.btn_MESSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MESSetting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_MESSetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_MESSetting.Location = new System.Drawing.Point(16, 130);
            this.btn_MESSetting.Name = "btn_MESSetting";
            this.btn_MESSetting.Size = new System.Drawing.Size(116, 26);
            this.btn_MESSetting.TabIndex = 39;
            this.btn_MESSetting.Text = "MES设置";
            this.btn_MESSetting.UseVisualStyleBackColor = false;
            this.btn_MESSetting.Click += new System.EventHandler(this.btn_MESSetting_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::UpperComputer.Properties.Resources.速博达;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(938, 28);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(216, 42);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button_mesState);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btn_ModeName);
            this.panel2.Controls.Add(this.label_制令单);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(176, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 42);
            this.panel2.TabIndex = 96;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 680);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.uiLine1);
            this.Controls.Add(this.uiLine3);
            this.Controls.Add(this.uiRichTextBox2);
            this.Controls.Add(this.uiRichTextBox1);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(1078, 680);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 15, 16);
            this.Text = "NTC打胶机";
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btn_PLCStatus;
        private System.Windows.Forms.Button btn_Scan;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_CCD0;
        private System.Windows.Forms.Button btn_Inital;
        private System.Windows.Forms.Button btn_Setting;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ModeName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_mesState;
        private System.Windows.Forms.Button btn_CCD1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_制令单;
        private Sunny.UI.UILine uiLine1;
        private Sunny.UI.UILine uiLine3;
        private Sunny.UI.UIRichTextBox uiRichTextBox2;
        private Sunny.UI.UIRichTextBox uiRichTextBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_MESSetting;
        private System.Windows.Forms.Panel panel2;
    }
}

