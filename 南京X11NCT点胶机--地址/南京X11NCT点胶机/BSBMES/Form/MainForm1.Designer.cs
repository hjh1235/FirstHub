
namespace UpperComputer
{
    partial class MainForm1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lb_SoftwareName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_User = new System.Windows.Forms.Button();
            this.btn_Minimized = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Set = new System.Windows.Forms.Button();
            this.btn_Closed = new System.Windows.Forms.Button();
            this.statusStripSta = new System.Windows.Forms.StatusStrip();
            this.tls_时间 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tls_当前配方 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tls_当前权限 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tls_当前用户 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tls_制令单 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cms_User = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.用户登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户注销ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Setting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC监控ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生产记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mES登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.手动过站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mES下发参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.初始化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_StartForm = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.line_Station1 = new Sunny.UI.UILine();
            this.line_Station2 = new Sunny.UI.UILine();
            this.line_Alarm = new Sunny.UI.UILine();
            this.rch_txtStation1 = new Sunny.UI.UIRichTextBox();
            this.rch_txtStation2 = new Sunny.UI.UIRichTextBox();
            this.rchtxt_Alarm = new Sunny.UI.UIRichTextBox();
            this.timer_Update = new System.Windows.Forms.Timer(this.components);
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listView_Status = new System.Windows.Forms.ListView();
            this.名称 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AGV信号交互日志 = new System.Windows.Forms.TabPage();
            this.rch_txtAGV = new Sunny.UI.UIRichTextBox();
            this.打胶参数 = new System.Windows.Forms.TabPage();
            this.lv_Glue = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AGV信号刷新日志 = new System.Windows.Forms.TabPage();
            this.rch_txtAGVRes = new Sunny.UI.UIRichTextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_下料A = new System.Windows.Forms.Button();
            this.btn_上料A = new System.Windows.Forms.Button();
            this.txt_胶水A物料条码 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.指纹模块ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel3.SuspendLayout();
            this.statusStripSta.SuspendLayout();
            this.cms_User.SuspendLayout();
            this.cms_Setting.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AGV信号交互日志.SuspendLayout();
            this.打胶参数.SuspendLayout();
            this.AGV信号刷新日志.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.lb_SoftwareName);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1344, 55);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(7, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(148, 42);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 79;
            this.pictureBox2.TabStop = false;
            // 
            // lb_SoftwareName
            // 
            this.lb_SoftwareName.AutoSize = true;
            this.lb_SoftwareName.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_SoftwareName.ForeColor = System.Drawing.Color.DimGray;
            this.lb_SoftwareName.Location = new System.Drawing.Point(161, 18);
            this.lb_SoftwareName.Name = "lb_SoftwareName";
            this.lb_SoftwareName.Size = new System.Drawing.Size(174, 24);
            this.lb_SoftwareName.TabIndex = 20;
            this.lb_SoftwareName.Text = "NTC点胶上位机";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btn_User);
            this.panel3.Controls.Add(this.btn_Minimized);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.btn_Set);
            this.panel3.Controls.Add(this.btn_Closed);
            this.panel3.Location = new System.Drawing.Point(1191, 2);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(152, 36);
            this.panel3.TabIndex = 19;
            this.panel3.Tag = "用户";
            // 
            // btn_User
            // 
            this.btn_User.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_User.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_User.FlatAppearance.BorderSize = 0;
            this.btn_User.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_User.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_User.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_User.Image = ((System.Drawing.Image)(resources.GetObject("btn_User.Image")));
            this.btn_User.Location = new System.Drawing.Point(12, 6);
            this.btn_User.Margin = new System.Windows.Forms.Padding(2);
            this.btn_User.Name = "btn_User";
            this.btn_User.Size = new System.Drawing.Size(20, 20);
            this.btn_User.TabIndex = 18;
            this.btn_User.TabStop = false;
            this.btn_User.UseVisualStyleBackColor = false;
            this.btn_User.Click += new System.EventHandler(this.btn_User_Click);
            // 
            // btn_Minimized
            // 
            this.btn_Minimized.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Minimized.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Minimized.FlatAppearance.BorderSize = 0;
            this.btn_Minimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Minimized.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Minimized.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Minimized.Image = ((System.Drawing.Image)(resources.GetObject("btn_Minimized.Image")));
            this.btn_Minimized.Location = new System.Drawing.Point(74, 6);
            this.btn_Minimized.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Minimized.Name = "btn_Minimized";
            this.btn_Minimized.Size = new System.Drawing.Size(20, 20);
            this.btn_Minimized.TabIndex = 12;
            this.btn_Minimized.TabStop = false;
            this.btn_Minimized.UseVisualStyleBackColor = false;
            this.btn_Minimized.Click += new System.EventHandler(this.btn_Minimized_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(99, 6);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 19;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btn_Maximized_Click);
            // 
            // btn_Set
            // 
            this.btn_Set.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Set.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Set.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Set.FlatAppearance.BorderSize = 0;
            this.btn_Set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Set.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Set.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Set.Image = ((System.Drawing.Image)(resources.GetObject("btn_Set.Image")));
            this.btn_Set.Location = new System.Drawing.Point(42, 6);
            this.btn_Set.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(20, 20);
            this.btn_Set.TabIndex = 17;
            this.btn_Set.TabStop = false;
            this.btn_Set.UseVisualStyleBackColor = false;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // btn_Closed
            // 
            this.btn_Closed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Closed.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btn_Closed.FlatAppearance.BorderSize = 0;
            this.btn_Closed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Closed.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Closed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_Closed.Image = ((System.Drawing.Image)(resources.GetObject("btn_Closed.Image")));
            this.btn_Closed.Location = new System.Drawing.Point(125, 6);
            this.btn_Closed.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Closed.Name = "btn_Closed";
            this.btn_Closed.Size = new System.Drawing.Size(20, 20);
            this.btn_Closed.TabIndex = 13;
            this.btn_Closed.TabStop = false;
            this.btn_Closed.UseVisualStyleBackColor = false;
            this.btn_Closed.Click += new System.EventHandler(this.btn_Closed_Click);
            // 
            // statusStripSta
            // 
            this.statusStripSta.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.statusStripSta.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStripSta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tls_时间,
            this.tls_当前配方,
            this.tls_当前权限,
            this.tls_当前用户,
            this.tls_制令单});
            this.statusStripSta.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripSta.Location = new System.Drawing.Point(0, 797);
            this.statusStripSta.Name = "statusStripSta";
            this.statusStripSta.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStripSta.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStripSta.Size = new System.Drawing.Size(1344, 26);
            this.statusStripSta.TabIndex = 104;
            this.statusStripSta.Tag = "  ";
            this.statusStripSta.Text = "statusStrip1";
            // 
            // tls_时间
            // 
            this.tls_时间.Name = "tls_时间";
            this.tls_时间.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tls_时间.Size = new System.Drawing.Size(126, 21);
            this.tls_时间.Text = "2023/10/13 00:00:00";
            // 
            // tls_当前配方
            // 
            this.tls_当前配方.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tls_当前配方.Name = "tls_当前配方";
            this.tls_当前配方.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tls_当前配方.Size = new System.Drawing.Size(87, 21);
            this.tls_当前配方.Text = "当前配方：***";
            this.tls_当前配方.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // tls_当前权限
            // 
            this.tls_当前权限.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tls_当前权限.Name = "tls_当前权限";
            this.tls_当前权限.Size = new System.Drawing.Size(108, 21);
            this.tls_当前权限.Text = "当前用户：未登录";
            // 
            // tls_当前用户
            // 
            this.tls_当前用户.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tls_当前用户.Name = "tls_当前用户";
            this.tls_当前用户.Size = new System.Drawing.Size(108, 21);
            this.tls_当前用户.Text = "当前用户：未登录";
            // 
            // tls_制令单
            // 
            this.tls_制令单.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tls_制令单.Name = "tls_制令单";
            this.tls_制令单.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tls_制令单.Size = new System.Drawing.Size(60, 21);
            this.tls_制令单.Text = "制令单：";
            // 
            // cms_User
            // 
            this.cms_User.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms_User.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.用户登录ToolStripMenuItem,
            this.用户管理ToolStripMenuItem,
            this.用户注销ToolStripMenuItem});
            this.cms_User.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.cms_User.Name = "cms_ClearListView";
            this.cms_User.Size = new System.Drawing.Size(125, 70);
            // 
            // 用户登录ToolStripMenuItem
            // 
            this.用户登录ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.用户登录ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.用户登录ToolStripMenuItem.Name = "用户登录ToolStripMenuItem";
            this.用户登录ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.用户登录ToolStripMenuItem.Text = "用户登录";
            this.用户登录ToolStripMenuItem.Click += new System.EventHandler(this.用户登录ToolStripMenuItem_Click);
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.用户管理ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            this.用户管理ToolStripMenuItem.Click += new System.EventHandler(this.用户管理ToolStripMenuItem_Click);
            // 
            // 用户注销ToolStripMenuItem
            // 
            this.用户注销ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.用户注销ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.用户注销ToolStripMenuItem.Name = "用户注销ToolStripMenuItem";
            this.用户注销ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.用户注销ToolStripMenuItem.Text = "用户注销";
            this.用户注销ToolStripMenuItem.Click += new System.EventHandler(this.用户注销ToolStripMenuItem_Click);
            // 
            // cms_Setting
            // 
            this.cms_Setting.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.cms_Setting.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms_Setting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.参数设置ToolStripMenuItem,
            this.pLC监控ToolStripMenuItem,
            this.生产记录ToolStripMenuItem,
            this.mES登录ToolStripMenuItem,
            this.手动过站ToolStripMenuItem,
            this.mES下发参数ToolStripMenuItem,
            this.初始化ToolStripMenuItem,
            this.指纹模块ToolStripMenuItem});
            this.cms_Setting.Name = "contextMenuStrip1";
            this.cms_Setting.Size = new System.Drawing.Size(181, 202);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.参数设置ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.参数设置ToolStripMenuItem_Click);
            // 
            // pLC监控ToolStripMenuItem
            // 
            this.pLC监控ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pLC监控ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.pLC监控ToolStripMenuItem.Name = "pLC监控ToolStripMenuItem";
            this.pLC监控ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pLC监控ToolStripMenuItem.Text = "PLC点位监控";
            this.pLC监控ToolStripMenuItem.Click += new System.EventHandler(this.pLC监控ToolStripMenuItem_Click);
            // 
            // 生产记录ToolStripMenuItem
            // 
            this.生产记录ToolStripMenuItem.Name = "生产记录ToolStripMenuItem";
            this.生产记录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.生产记录ToolStripMenuItem.Text = "生产记录";
            this.生产记录ToolStripMenuItem.Click += new System.EventHandler(this.生产记录ToolStripMenuItem_Click);
            // 
            // mES登录ToolStripMenuItem
            // 
            this.mES登录ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.mES登录ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.mES登录ToolStripMenuItem.Name = "mES登录ToolStripMenuItem";
            this.mES登录ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mES登录ToolStripMenuItem.Text = "MES登录";
            this.mES登录ToolStripMenuItem.Click += new System.EventHandler(this.mES登录ToolStripMenuItem_Click);
            // 
            // 手动过站ToolStripMenuItem
            // 
            this.手动过站ToolStripMenuItem.Name = "手动过站ToolStripMenuItem";
            this.手动过站ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.手动过站ToolStripMenuItem.Text = "手动过站";
            this.手动过站ToolStripMenuItem.Click += new System.EventHandler(this.手动过站ToolStripMenuItem_Click);
            // 
            // mES下发参数ToolStripMenuItem
            // 
            this.mES下发参数ToolStripMenuItem.Name = "mES下发参数ToolStripMenuItem";
            this.mES下发参数ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.mES下发参数ToolStripMenuItem.Text = "MES下发参数";
            this.mES下发参数ToolStripMenuItem.Click += new System.EventHandler(this.mES下发参数ToolStripMenuItem_Click);
            // 
            // 初始化ToolStripMenuItem
            // 
            this.初始化ToolStripMenuItem.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.初始化ToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.初始化ToolStripMenuItem.Name = "初始化ToolStripMenuItem";
            this.初始化ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.初始化ToolStripMenuItem.Text = "初始化";
            this.初始化ToolStripMenuItem.Click += new System.EventHandler(this.初始化ToolStripMenuItem_Click);
            // 
            // panel_StartForm
            // 
            this.panel_StartForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_StartForm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_StartForm.Location = new System.Drawing.Point(0, 56);
            this.panel_StartForm.Name = "panel_StartForm";
            this.panel_StartForm.Size = new System.Drawing.Size(993, 469);
            this.panel_StartForm.TabIndex = 106;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.line_Station1);
            this.panel2.Controls.Add(this.line_Station2);
            this.panel2.Controls.Add(this.line_Alarm);
            this.panel2.Controls.Add(this.rch_txtStation1);
            this.panel2.Controls.Add(this.rch_txtStation2);
            this.panel2.Controls.Add(this.rchtxt_Alarm);
            this.panel2.Location = new System.Drawing.Point(0, 592);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1343, 203);
            this.panel2.TabIndex = 107;
            // 
            // line_Station1
            // 
            this.line_Station1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.line_Station1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.line_Station1.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.line_Station1.Location = new System.Drawing.Point(415, 2);
            this.line_Station1.Margin = new System.Windows.Forms.Padding(2);
            this.line_Station1.MinimumSize = new System.Drawing.Size(12, 13);
            this.line_Station1.Name = "line_Station1";
            this.line_Station1.Size = new System.Drawing.Size(460, 22);
            this.line_Station1.TabIndex = 115;
            this.line_Station1.Text = "工位1";
            this.line_Station1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // line_Station2
            // 
            this.line_Station2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.line_Station2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.line_Station2.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.line_Station2.Location = new System.Drawing.Point(893, 2);
            this.line_Station2.Margin = new System.Windows.Forms.Padding(2);
            this.line_Station2.MinimumSize = new System.Drawing.Size(12, 13);
            this.line_Station2.Name = "line_Station2";
            this.line_Station2.Size = new System.Drawing.Size(443, 22);
            this.line_Station2.TabIndex = 113;
            this.line_Station2.Text = "工位2";
            this.line_Station2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // line_Alarm
            // 
            this.line_Alarm.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.line_Alarm.LineColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.line_Alarm.Location = new System.Drawing.Point(3, 2);
            this.line_Alarm.Margin = new System.Windows.Forms.Padding(2);
            this.line_Alarm.MinimumSize = new System.Drawing.Size(12, 13);
            this.line_Alarm.Name = "line_Alarm";
            this.line_Alarm.Size = new System.Drawing.Size(408, 22);
            this.line_Alarm.TabIndex = 114;
            this.line_Alarm.Text = "报警记录";
            this.line_Alarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rch_txtStation1
            // 
            this.rch_txtStation1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rch_txtStation1.FillColor = System.Drawing.Color.White;
            this.rch_txtStation1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rch_txtStation1.Location = new System.Drawing.Point(415, 27);
            this.rch_txtStation1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rch_txtStation1.MinimumSize = new System.Drawing.Size(1, 1);
            this.rch_txtStation1.Name = "rch_txtStation1";
            this.rch_txtStation1.Padding = new System.Windows.Forms.Padding(2);
            this.rch_txtStation1.Radius = 1;
            this.rch_txtStation1.ShowText = false;
            this.rch_txtStation1.Size = new System.Drawing.Size(460, 172);
            this.rch_txtStation1.Style = Sunny.UI.UIStyle.Custom;
            this.rch_txtStation1.TabIndex = 112;
            this.rch_txtStation1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rch_txtStation2
            // 
            this.rch_txtStation2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rch_txtStation2.FillColor = System.Drawing.Color.White;
            this.rch_txtStation2.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rch_txtStation2.Location = new System.Drawing.Point(893, 28);
            this.rch_txtStation2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rch_txtStation2.MinimumSize = new System.Drawing.Size(1, 1);
            this.rch_txtStation2.Name = "rch_txtStation2";
            this.rch_txtStation2.Padding = new System.Windows.Forms.Padding(2);
            this.rch_txtStation2.Radius = 1;
            this.rch_txtStation2.ShowText = false;
            this.rch_txtStation2.Size = new System.Drawing.Size(447, 170);
            this.rch_txtStation2.Style = Sunny.UI.UIStyle.Custom;
            this.rch_txtStation2.TabIndex = 110;
            this.rch_txtStation2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rchtxt_Alarm
            // 
            this.rchtxt_Alarm.FillColor = System.Drawing.Color.White;
            this.rchtxt_Alarm.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rchtxt_Alarm.Location = new System.Drawing.Point(3, 28);
            this.rchtxt_Alarm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rchtxt_Alarm.MinimumSize = new System.Drawing.Size(1, 1);
            this.rchtxt_Alarm.Name = "rchtxt_Alarm";
            this.rchtxt_Alarm.Padding = new System.Windows.Forms.Padding(2);
            this.rchtxt_Alarm.Radius = 1;
            this.rchtxt_Alarm.ShowText = false;
            this.rchtxt_Alarm.Size = new System.Drawing.Size(408, 171);
            this.rchtxt_Alarm.Style = Sunny.UI.UIStyle.Custom;
            this.rchtxt_Alarm.TabIndex = 111;
            this.rchtxt_Alarm.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer_Update
            // 
            this.timer_Update.Interval = 10;
            this.timer_Update.Tick += new System.EventHandler(this.timer_Update_Tick);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Location = new System.Drawing.Point(2, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(347, 194);
            this.panel4.TabIndex = 108;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listView_Status);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 192);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态显示";
            // 
            // listView_Status
            // 
            this.listView_Status.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.名称});
            this.listView_Status.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Status.GridLines = true;
            this.listView_Status.HideSelection = false;
            this.listView_Status.Location = new System.Drawing.Point(3, 17);
            this.listView_Status.Name = "listView_Status";
            this.listView_Status.Size = new System.Drawing.Size(339, 172);
            this.listView_Status.SmallImageList = this.imageList1;
            this.listView_Status.TabIndex = 0;
            this.listView_Status.UseCompatibleStateImageBehavior = false;
            this.listView_Status.View = System.Windows.Forms.View.Details;
            // 
            // 名称
            // 
            this.名称.Text = "状态";
            this.名称.Width = 245;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lamp_white.png");
            this.imageList1.Images.SetKeyName(1, "lamp_green.png");
            // 
            // panel7
            // 
            this.panel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.panel5);
            this.panel7.Controls.Add(this.panel4);
            this.panel7.Location = new System.Drawing.Point(994, 56);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(349, 534);
            this.panel7.TabIndex = 108;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tabControl1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 204);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(347, 328);
            this.panel5.TabIndex = 111;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AGV信号交互日志);
            this.tabControl1.Controls.Add(this.打胶参数);
            this.tabControl1.Controls.Add(this.AGV信号刷新日志);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(345, 326);
            this.tabControl1.TabIndex = 0;
            // 
            // AGV信号交互日志
            // 
            this.AGV信号交互日志.Controls.Add(this.rch_txtAGV);
            this.AGV信号交互日志.Location = new System.Drawing.Point(4, 22);
            this.AGV信号交互日志.Name = "AGV信号交互日志";
            this.AGV信号交互日志.Padding = new System.Windows.Forms.Padding(3);
            this.AGV信号交互日志.Size = new System.Drawing.Size(337, 300);
            this.AGV信号交互日志.TabIndex = 0;
            this.AGV信号交互日志.Text = "AGV信号交互日志";
            this.AGV信号交互日志.UseVisualStyleBackColor = true;
            // 
            // rch_txtAGV
            // 
            this.rch_txtAGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rch_txtAGV.FillColor = System.Drawing.Color.White;
            this.rch_txtAGV.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rch_txtAGV.Location = new System.Drawing.Point(3, 3);
            this.rch_txtAGV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rch_txtAGV.MinimumSize = new System.Drawing.Size(1, 1);
            this.rch_txtAGV.Name = "rch_txtAGV";
            this.rch_txtAGV.Padding = new System.Windows.Forms.Padding(2);
            this.rch_txtAGV.Radius = 1;
            this.rch_txtAGV.ShowText = false;
            this.rch_txtAGV.Size = new System.Drawing.Size(331, 294);
            this.rch_txtAGV.Style = Sunny.UI.UIStyle.Custom;
            this.rch_txtAGV.TabIndex = 113;
            this.rch_txtAGV.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 打胶参数
            // 
            this.打胶参数.Controls.Add(this.lv_Glue);
            this.打胶参数.Location = new System.Drawing.Point(4, 22);
            this.打胶参数.Name = "打胶参数";
            this.打胶参数.Padding = new System.Windows.Forms.Padding(3);
            this.打胶参数.Size = new System.Drawing.Size(337, 300);
            this.打胶参数.TabIndex = 1;
            this.打胶参数.Text = "打胶参数";
            this.打胶参数.UseVisualStyleBackColor = true;
            // 
            // lv_Glue
            // 
            this.lv_Glue.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_Glue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_Glue.GridLines = true;
            this.lv_Glue.HideSelection = false;
            this.lv_Glue.Location = new System.Drawing.Point(3, 3);
            this.lv_Glue.Name = "lv_Glue";
            this.lv_Glue.Size = new System.Drawing.Size(331, 294);
            this.lv_Glue.TabIndex = 0;
            this.lv_Glue.UseCompatibleStateImageBehavior = false;
            this.lv_Glue.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名称";
            this.columnHeader1.Width = 122;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "值";
            this.columnHeader2.Width = 205;
            // 
            // AGV信号刷新日志
            // 
            this.AGV信号刷新日志.Controls.Add(this.rch_txtAGVRes);
            this.AGV信号刷新日志.Location = new System.Drawing.Point(4, 22);
            this.AGV信号刷新日志.Name = "AGV信号刷新日志";
            this.AGV信号刷新日志.Size = new System.Drawing.Size(337, 300);
            this.AGV信号刷新日志.TabIndex = 2;
            this.AGV信号刷新日志.Text = "AGV信号刷新日志";
            this.AGV信号刷新日志.UseVisualStyleBackColor = true;
            // 
            // rch_txtAGVRes
            // 
            this.rch_txtAGVRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rch_txtAGVRes.FillColor = System.Drawing.Color.White;
            this.rch_txtAGVRes.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.rch_txtAGVRes.Location = new System.Drawing.Point(0, 0);
            this.rch_txtAGVRes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rch_txtAGVRes.MinimumSize = new System.Drawing.Size(1, 1);
            this.rch_txtAGVRes.Name = "rch_txtAGVRes";
            this.rch_txtAGVRes.Padding = new System.Windows.Forms.Padding(2);
            this.rch_txtAGVRes.Radius = 1;
            this.rch_txtAGVRes.ShowText = false;
            this.rch_txtAGVRes.Size = new System.Drawing.Size(337, 300);
            this.rch_txtAGVRes.Style = Sunny.UI.UIStyle.Custom;
            this.rch_txtAGVRes.TabIndex = 114;
            this.rch_txtAGVRes.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btn_下料A);
            this.panel6.Controls.Add(this.btn_上料A);
            this.panel6.Controls.Add(this.txt_胶水A物料条码);
            this.panel6.Controls.Add(this.label1);
            this.panel6.Location = new System.Drawing.Point(0, 531);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(993, 59);
            this.panel6.TabIndex = 109;
            // 
            // btn_下料A
            // 
            this.btn_下料A.Location = new System.Drawing.Point(420, 18);
            this.btn_下料A.Name = "btn_下料A";
            this.btn_下料A.Size = new System.Drawing.Size(75, 23);
            this.btn_下料A.TabIndex = 5;
            this.btn_下料A.Text = "下料";
            this.btn_下料A.UseVisualStyleBackColor = true;
            this.btn_下料A.Click += new System.EventHandler(this.btn_下料A_Click);
            // 
            // btn_上料A
            // 
            this.btn_上料A.Location = new System.Drawing.Point(330, 19);
            this.btn_上料A.Name = "btn_上料A";
            this.btn_上料A.Size = new System.Drawing.Size(75, 23);
            this.btn_上料A.TabIndex = 4;
            this.btn_上料A.Text = "上料";
            this.btn_上料A.UseVisualStyleBackColor = true;
            this.btn_上料A.Click += new System.EventHandler(this.btn_上料A_Click);
            // 
            // txt_胶水A物料条码
            // 
            this.txt_胶水A物料条码.Location = new System.Drawing.Point(94, 19);
            this.txt_胶水A物料条码.Name = "txt_胶水A物料条码";
            this.txt_胶水A物料条码.Size = new System.Drawing.Size(211, 21);
            this.txt_胶水A物料条码.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "胶水物料条码：";
            // 
            // 指纹模块ToolStripMenuItem
            // 
            this.指纹模块ToolStripMenuItem.Name = "指纹模块ToolStripMenuItem";
            this.指纹模块ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.指纹模块ToolStripMenuItem.Text = "指纹模块";
            this.指纹模块ToolStripMenuItem.Click += new System.EventHandler(this.指纹模块ToolStripMenuItem_Click);
            // 
            // MainForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 823);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel_StartForm);
            this.Controls.Add(this.statusStripSta);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm1_FormClosed);
            this.Load += new System.EventHandler(this.MainForm1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.statusStripSta.ResumeLayout(false);
            this.statusStripSta.PerformLayout();
            this.cms_User.ResumeLayout(false);
            this.cms_Setting.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.AGV信号交互日志.ResumeLayout(false);
            this.打胶参数.ResumeLayout(false);
            this.AGV信号刷新日志.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lb_SoftwareName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Set;
        private System.Windows.Forms.Button btn_Closed;
        private System.Windows.Forms.Button btn_Minimized;
        private System.Windows.Forms.StatusStrip statusStripSta;
        private System.Windows.Forms.ContextMenuStrip cms_Setting;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cms_User;
        private System.Windows.Forms.ToolStripMenuItem 用户登录ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_StartForm;
        private System.Windows.Forms.ToolStripMenuItem pLC监控ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mES登录ToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_User;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户注销ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tls_当前用户;
        private System.Windows.Forms.ToolStripStatusLabel tls_时间;
        private Sunny.UI.UIRichTextBox rch_txtStation1;
        private Sunny.UI.UIRichTextBox rch_txtStation2;
        private Sunny.UI.UIRichTextBox rchtxt_Alarm;
        private Sunny.UI.UILine line_Station1;
        private Sunny.UI.UILine line_Station2;
        private Sunny.UI.UILine line_Alarm;
        private System.Windows.Forms.Timer timer_Update;
        private System.Windows.Forms.ToolStripMenuItem 初始化ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tls_当前配方;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListView listView_Status;
        private System.Windows.Forms.ColumnHeader 名称;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ToolStripStatusLabel tls_制令单;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AGV信号交互日志;
        private Sunny.UI.UIRichTextBox rch_txtAGV;
        private System.Windows.Forms.TabPage 打胶参数;
        private System.Windows.Forms.ListView lv_Glue;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txt_胶水A物料条码;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_下料A;
        private System.Windows.Forms.Button btn_上料A;
        private System.Windows.Forms.ToolStripMenuItem 生产记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tls_当前权限;
        private System.Windows.Forms.ToolStripMenuItem 手动过站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mES下发参数ToolStripMenuItem;
        private System.Windows.Forms.TabPage AGV信号刷新日志;
        private Sunny.UI.UIRichTextBox rch_txtAGVRes;
        private System.Windows.Forms.ToolStripMenuItem 指纹模块ToolStripMenuItem;
    }
}