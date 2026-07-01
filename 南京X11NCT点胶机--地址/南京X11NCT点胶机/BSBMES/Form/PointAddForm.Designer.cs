namespace UpperComputer
{
    partial class PointAddForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_PLCPointName = new System.Windows.Forms.TextBox();
            this.txt_PLCPointAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_DeletePLCPoint = new System.Windows.Forms.Button();
            this.txt_WriteTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_WritePLCPoint = new System.Windows.Forms.Button();
            this.txt_ReadTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_ReadPLCPoint = new System.Windows.Forms.Button();
            this.btn_AddPointToRight = new System.Windows.Forms.Button();
            this.cmb_PLCPointType = new System.Windows.Forms.ComboBox();
            this.grb = new System.Windows.Forms.GroupBox();
            this.dgv_PLC = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.grb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PLC)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "点位名称";
            // 
            // txt_PLCPointName
            // 
            this.txt_PLCPointName.Location = new System.Drawing.Point(67, 29);
            this.txt_PLCPointName.Name = "txt_PLCPointName";
            this.txt_PLCPointName.Size = new System.Drawing.Size(148, 21);
            this.txt_PLCPointName.TabIndex = 1;
            // 
            // txt_PLCPointAddress
            // 
            this.txt_PLCPointAddress.Location = new System.Drawing.Point(67, 68);
            this.txt_PLCPointAddress.Name = "txt_PLCPointAddress";
            this.txt_PLCPointAddress.Size = new System.Drawing.Size(148, 21);
            this.txt_PLCPointAddress.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "类型";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_DeletePLCPoint);
            this.groupBox1.Controls.Add(this.txt_WriteTxt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_WritePLCPoint);
            this.groupBox1.Controls.Add(this.txt_ReadTxt);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btn_ReadPLCPoint);
            this.groupBox1.Controls.Add(this.btn_AddPointToRight);
            this.groupBox1.Controls.Add(this.cmb_PLCPointType);
            this.groupBox1.Controls.Add(this.txt_PLCPointName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_PLCPointAddress);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 469);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "点位添加";
            // 
            // btn_DeletePLCPoint
            // 
            this.btn_DeletePLCPoint.Location = new System.Drawing.Point(128, 157);
            this.btn_DeletePLCPoint.Name = "btn_DeletePLCPoint";
            this.btn_DeletePLCPoint.Size = new System.Drawing.Size(87, 39);
            this.btn_DeletePLCPoint.TabIndex = 13;
            this.btn_DeletePLCPoint.Text = "删除右方选择点位";
            this.btn_DeletePLCPoint.UseVisualStyleBackColor = true;
            this.btn_DeletePLCPoint.Click += new System.EventHandler(this.btn_DeletePLCPoint_Click);
            // 
            // txt_WriteTxt
            // 
            this.txt_WriteTxt.Location = new System.Drawing.Point(10, 357);
            this.txt_WriteTxt.Name = "txt_WriteTxt";
            this.txt_WriteTxt.Size = new System.Drawing.Size(205, 21);
            this.txt_WriteTxt.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "写上面地址↑：";
            // 
            // btn_WritePLCPoint
            // 
            this.btn_WritePLCPoint.Location = new System.Drawing.Point(12, 395);
            this.btn_WritePLCPoint.Name = "btn_WritePLCPoint";
            this.btn_WritePLCPoint.Size = new System.Drawing.Size(203, 28);
            this.btn_WritePLCPoint.TabIndex = 10;
            this.btn_WritePLCPoint.Text = "写PLC";
            this.btn_WritePLCPoint.UseVisualStyleBackColor = true;
            this.btn_WritePLCPoint.Click += new System.EventHandler(this.btn_WritePLCPoint_Click);
            // 
            // txt_ReadTxt
            // 
            this.txt_ReadTxt.Location = new System.Drawing.Point(10, 244);
            this.txt_ReadTxt.Name = "txt_ReadTxt";
            this.txt_ReadTxt.Size = new System.Drawing.Size(205, 21);
            this.txt_ReadTxt.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "读上面地址↑：";
            // 
            // btn_ReadPLCPoint
            // 
            this.btn_ReadPLCPoint.Location = new System.Drawing.Point(12, 282);
            this.btn_ReadPLCPoint.Name = "btn_ReadPLCPoint";
            this.btn_ReadPLCPoint.Size = new System.Drawing.Size(203, 28);
            this.btn_ReadPLCPoint.TabIndex = 7;
            this.btn_ReadPLCPoint.Text = "读PLC";
            this.btn_ReadPLCPoint.UseVisualStyleBackColor = true;
            this.btn_ReadPLCPoint.Click += new System.EventHandler(this.btn_ReadPLCPoint_Click);
            // 
            // btn_AddPointToRight
            // 
            this.btn_AddPointToRight.Location = new System.Drawing.Point(12, 157);
            this.btn_AddPointToRight.Name = "btn_AddPointToRight";
            this.btn_AddPointToRight.Size = new System.Drawing.Size(87, 39);
            this.btn_AddPointToRight.TabIndex = 6;
            this.btn_AddPointToRight.Text = "添加上方点位到右表→";
            this.btn_AddPointToRight.UseVisualStyleBackColor = true;
            this.btn_AddPointToRight.Click += new System.EventHandler(this.btn_AddPointToRight_Click);
            // 
            // cmb_PLCPointType
            // 
            this.cmb_PLCPointType.FormattingEnabled = true;
            this.cmb_PLCPointType.Items.AddRange(new object[] {
            "Short",
            "Double",
            "Long",
            "Int",
            "uint",
            "string",
            "float",
            "Bool"});
            this.cmb_PLCPointType.Location = new System.Drawing.Point(68, 113);
            this.cmb_PLCPointType.Name = "cmb_PLCPointType";
            this.cmb_PLCPointType.Size = new System.Drawing.Size(147, 20);
            this.cmb_PLCPointType.TabIndex = 5;
            // 
            // grb
            // 
            this.grb.Controls.Add(this.dgv_PLC);
            this.grb.Location = new System.Drawing.Point(334, 12);
            this.grb.Name = "grb";
            this.grb.Size = new System.Drawing.Size(509, 469);
            this.grb.TabIndex = 7;
            this.grb.TabStop = false;
            this.grb.Text = "信息展示";
            // 
            // dgv_PLC
            // 
            this.dgv_PLC.AllowUserToAddRows = false;
            this.dgv_PLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PLC.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv_PLC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_PLC.Location = new System.Drawing.Point(3, 17);
            this.dgv_PLC.Name = "dgv_PLC";
            this.dgv_PLC.RowHeadersVisible = false;
            this.dgv_PLC.RowTemplate.Height = 23;
            this.dgv_PLC.Size = new System.Drawing.Size(503, 449);
            this.dgv_PLC.TabIndex = 0;
            this.dgv_PLC.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_PLC_CellClick);
            this.dgv_PLC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_PLC_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "点位名称";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column2.HeaderText = "地址";
            this.Column2.Name = "Column2";
            this.Column2.Width = 54;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "类型";
            this.Column3.Name = "Column3";
            this.Column3.Width = 54;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "结果";
            this.Column4.Name = "Column4";
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 516);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(203, 28);
            this.button1.TabIndex = 14;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PointAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 556);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grb);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PointAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PLC通讯界面";
            this.Load += new System.EventHandler(this.PointAddForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PLC)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_PLCPointName;
        private System.Windows.Forms.TextBox txt_PLCPointAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_AddPointToRight;
        private System.Windows.Forms.ComboBox cmb_PLCPointType;
        private System.Windows.Forms.TextBox txt_WriteTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_WritePLCPoint;
        private System.Windows.Forms.TextBox txt_ReadTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_ReadPLCPoint;
        private System.Windows.Forms.GroupBox grb;
        private System.Windows.Forms.DataGridView dgv_PLC;
        private System.Windows.Forms.Button btn_DeletePLCPoint;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}