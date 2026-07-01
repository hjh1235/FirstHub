namespace UpperComputer
{
    partial class MesLoadParForm
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
            this.cmb_Mode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Insert = new System.Windows.Forms.Button();
            this.cmb_Enable = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmb_CCDPos = new System.Windows.Forms.ComboBox();
            this.lb_Pos = new System.Windows.Forms.Label();
            this.txt_FixedValue = new System.Windows.Forms.TextBox();
            this.lb_FixedValue = new System.Windows.Forms.Label();
            this.cmb_DataType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_paramUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_paramCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_paramLower = new System.Windows.Forms.TextBox();
            this.lb_EndPos = new System.Windows.Forms.Label();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_MoveDown = new System.Windows.Forms.Button();
            this.btn_MoveUp = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.txt_paramUp = new System.Windows.Forms.TextBox();
            this.lb_StartPos = new System.Windows.Forms.Label();
            this.cmb_bControl = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_paramName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_Mode = new System.Windows.Forms.DataGridView();
            this.btn_CreatMode = new System.Windows.Forms.Button();
            this.btn_DeleteMode = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.txt_NewModeName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.参数名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.参数代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.激光字段名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.参数下限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否管控 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数据来源 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.固定值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ck_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Mode)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Mode
            // 
            this.cmb_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Mode.FormattingEnabled = true;
            this.cmb_Mode.Location = new System.Drawing.Point(78, 10);
            this.cmb_Mode.Name = "cmb_Mode";
            this.cmb_Mode.Size = new System.Drawing.Size(222, 20);
            this.cmb_Mode.TabIndex = 0;
            this.cmb_Mode.SelectedIndexChanged += new System.EventHandler(this.cmb_Mode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "配方名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(71, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "参数名称:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Insert);
            this.groupBox1.Controls.Add(this.cmb_Enable);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cmb_CCDPos);
            this.groupBox1.Controls.Add(this.lb_Pos);
            this.groupBox1.Controls.Add(this.txt_FixedValue);
            this.groupBox1.Controls.Add(this.lb_FixedValue);
            this.groupBox1.Controls.Add(this.cmb_DataType);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_paramUnit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_paramCode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_paramLower);
            this.groupBox1.Controls.Add(this.lb_EndPos);
            this.groupBox1.Controls.Add(this.btn_Update);
            this.groupBox1.Controls.Add(this.btn_MoveDown);
            this.groupBox1.Controls.Add(this.btn_MoveUp);
            this.groupBox1.Controls.Add(this.btn_Delete);
            this.groupBox1.Controls.Add(this.btn_Add);
            this.groupBox1.Controls.Add(this.txt_paramUp);
            this.groupBox1.Controls.Add(this.lb_StartPos);
            this.groupBox1.Controls.Add(this.cmb_bControl);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_paramName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 529);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "物料设置";
            // 
            // btn_Insert
            // 
            this.btn_Insert.Location = new System.Drawing.Point(227, 371);
            this.btn_Insert.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.Size = new System.Drawing.Size(56, 26);
            this.btn_Insert.TabIndex = 33;
            this.btn_Insert.Text = "插入";
            this.btn_Insert.UseVisualStyleBackColor = true;
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // cmb_Enable
            // 
            this.cmb_Enable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Enable.FormattingEnabled = true;
            this.cmb_Enable.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cmb_Enable.Location = new System.Drawing.Point(144, 264);
            this.cmb_Enable.Name = "cmb_Enable";
            this.cmb_Enable.Size = new System.Drawing.Size(186, 20);
            this.cmb_Enable.TabIndex = 32;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 267);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 31;
            this.label8.Text = "是否启用:";
            // 
            // cmb_CCDPos
            // 
            this.cmb_CCDPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CCDPos.FormattingEnabled = true;
            this.cmb_CCDPos.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmb_CCDPos.Location = new System.Drawing.Point(144, 238);
            this.cmb_CCDPos.Name = "cmb_CCDPos";
            this.cmb_CCDPos.Size = new System.Drawing.Size(186, 20);
            this.cmb_CCDPos.TabIndex = 30;
            // 
            // lb_Pos
            // 
            this.lb_Pos.AutoSize = true;
            this.lb_Pos.Location = new System.Drawing.Point(30, 241);
            this.lb_Pos.Name = "lb_Pos";
            this.lb_Pos.Size = new System.Drawing.Size(101, 12);
            this.lb_Pos.TabIndex = 29;
            this.lb_Pos.Text = "绑定CCD数据索引:";
            // 
            // txt_FixedValue
            // 
            this.txt_FixedValue.Location = new System.Drawing.Point(144, 211);
            this.txt_FixedValue.Name = "txt_FixedValue";
            this.txt_FixedValue.Size = new System.Drawing.Size(186, 21);
            this.txt_FixedValue.TabIndex = 26;
            this.txt_FixedValue.Text = "0";
            // 
            // lb_FixedValue
            // 
            this.lb_FixedValue.AutoSize = true;
            this.lb_FixedValue.Location = new System.Drawing.Point(106, 214);
            this.lb_FixedValue.Name = "lb_FixedValue";
            this.lb_FixedValue.Size = new System.Drawing.Size(23, 12);
            this.lb_FixedValue.TabIndex = 25;
            this.lb_FixedValue.Text = "值:";
            // 
            // cmb_DataType
            // 
            this.cmb_DataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_DataType.FormattingEnabled = true;
            this.cmb_DataType.Items.AddRange(new object[] {
            "系统采集",
            "视觉采集",
            "默认值"});
            this.cmb_DataType.Location = new System.Drawing.Point(144, 184);
            this.cmb_DataType.Name = "cmb_DataType";
            this.cmb_DataType.Size = new System.Drawing.Size(186, 20);
            this.cmb_DataType.TabIndex = 24;
            this.cmb_DataType.SelectedIndexChanged += new System.EventHandler(this.cmb_bFixedValue_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 23;
            this.label6.Text = "数据来源:";
            // 
            // txt_paramUnit
            // 
            this.txt_paramUnit.Location = new System.Drawing.Point(144, 129);
            this.txt_paramUnit.Name = "txt_paramUnit";
            this.txt_paramUnit.Size = new System.Drawing.Size(186, 21);
            this.txt_paramUnit.TabIndex = 22;
            this.txt_paramUnit.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "单位:";
            // 
            // txt_paramCode
            // 
            this.txt_paramCode.Location = new System.Drawing.Point(144, 48);
            this.txt_paramCode.Name = "txt_paramCode";
            this.txt_paramCode.Size = new System.Drawing.Size(186, 21);
            this.txt_paramCode.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 19;
            this.label3.Text = "参数代码:";
            // 
            // txt_paramLower
            // 
            this.txt_paramLower.Location = new System.Drawing.Point(144, 102);
            this.txt_paramLower.Name = "txt_paramLower";
            this.txt_paramLower.Size = new System.Drawing.Size(186, 21);
            this.txt_paramLower.TabIndex = 18;
            this.txt_paramLower.Text = "0";
            // 
            // lb_EndPos
            // 
            this.lb_EndPos.AutoSize = true;
            this.lb_EndPos.Location = new System.Drawing.Point(71, 105);
            this.lb_EndPos.Name = "lb_EndPos";
            this.lb_EndPos.Size = new System.Drawing.Size(59, 12);
            this.lb_EndPos.TabIndex = 17;
            this.lb_EndPos.Text = "参数下限:";
            // 
            // btn_Update
            // 
            this.btn_Update.Location = new System.Drawing.Point(136, 371);
            this.btn_Update.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(56, 26);
            this.btn_Update.TabIndex = 14;
            this.btn_Update.Text = "修改";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_MoveDown
            // 
            this.btn_MoveDown.Location = new System.Drawing.Point(136, 427);
            this.btn_MoveDown.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MoveDown.Name = "btn_MoveDown";
            this.btn_MoveDown.Size = new System.Drawing.Size(56, 26);
            this.btn_MoveDown.TabIndex = 13;
            this.btn_MoveDown.Text = "下移";
            this.btn_MoveDown.UseVisualStyleBackColor = true;
            this.btn_MoveDown.Click += new System.EventHandler(this.btn_MoveDown_Click);
            // 
            // btn_MoveUp
            // 
            this.btn_MoveUp.Location = new System.Drawing.Point(44, 427);
            this.btn_MoveUp.Margin = new System.Windows.Forms.Padding(2);
            this.btn_MoveUp.Name = "btn_MoveUp";
            this.btn_MoveUp.Size = new System.Drawing.Size(56, 26);
            this.btn_MoveUp.TabIndex = 12;
            this.btn_MoveUp.Text = "上移";
            this.btn_MoveUp.UseVisualStyleBackColor = true;
            this.btn_MoveUp.Click += new System.EventHandler(this.btn_MoveUp_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Location = new System.Drawing.Point(227, 427);
            this.btn_Delete.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(56, 26);
            this.btn_Delete.TabIndex = 11;
            this.btn_Delete.Text = "删除";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(44, 371);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(56, 26);
            this.btn_Add.TabIndex = 10;
            this.btn_Add.Text = "添加";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // txt_paramUp
            // 
            this.txt_paramUp.Location = new System.Drawing.Point(144, 75);
            this.txt_paramUp.Name = "txt_paramUp";
            this.txt_paramUp.Size = new System.Drawing.Size(186, 21);
            this.txt_paramUp.TabIndex = 5;
            this.txt_paramUp.Text = "0";
            // 
            // lb_StartPos
            // 
            this.lb_StartPos.AutoSize = true;
            this.lb_StartPos.Location = new System.Drawing.Point(71, 78);
            this.lb_StartPos.Name = "lb_StartPos";
            this.lb_StartPos.Size = new System.Drawing.Size(59, 12);
            this.lb_StartPos.TabIndex = 4;
            this.lb_StartPos.Text = "参数上限:";
            // 
            // cmb_bControl
            // 
            this.cmb_bControl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bControl.FormattingEnabled = true;
            this.cmb_bControl.Items.AddRange(new object[] {
            "是",
            "否"});
            this.cmb_bControl.Location = new System.Drawing.Point(144, 158);
            this.cmb_bControl.Name = "cmb_bControl";
            this.cmb_bControl.Size = new System.Drawing.Size(186, 20);
            this.cmb_bControl.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "是否管控:";
            // 
            // txt_paramName
            // 
            this.txt_paramName.Location = new System.Drawing.Point(144, 21);
            this.txt_paramName.Name = "txt_paramName";
            this.txt_paramName.Size = new System.Drawing.Size(186, 21);
            this.txt_paramName.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_Mode);
            this.groupBox2.Location = new System.Drawing.Point(364, 97);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(848, 529);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数表格";
            // 
            // dgv_Mode
            // 
            this.dgv_Mode.AllowUserToAddRows = false;
            this.dgv_Mode.AllowUserToResizeColumns = false;
            this.dgv_Mode.AllowUserToResizeRows = false;
            this.dgv_Mode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Mode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.参数名称,
            this.参数代码,
            this.激光字段名称,
            this.参数下限,
            this.单位,
            this.是否管控,
            this.数据来源,
            this.固定值,
            this.Column2,
            this.ck_Enable});
            this.dgv_Mode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Mode.Location = new System.Drawing.Point(2, 16);
            this.dgv_Mode.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_Mode.MultiSelect = false;
            this.dgv_Mode.Name = "dgv_Mode";
            this.dgv_Mode.ReadOnly = true;
            this.dgv_Mode.RowTemplate.Height = 27;
            this.dgv_Mode.Size = new System.Drawing.Size(844, 511);
            this.dgv_Mode.TabIndex = 0;
            this.dgv_Mode.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Mode_CellClick);
            // 
            // btn_CreatMode
            // 
            this.btn_CreatMode.Location = new System.Drawing.Point(337, 46);
            this.btn_CreatMode.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CreatMode.Name = "btn_CreatMode";
            this.btn_CreatMode.Size = new System.Drawing.Size(65, 29);
            this.btn_CreatMode.TabIndex = 7;
            this.btn_CreatMode.Text = "新建配方";
            this.btn_CreatMode.UseVisualStyleBackColor = true;
            this.btn_CreatMode.Click += new System.EventHandler(this.btn_CreatMode_Click);
            // 
            // btn_DeleteMode
            // 
            this.btn_DeleteMode.Location = new System.Drawing.Point(435, 5);
            this.btn_DeleteMode.Margin = new System.Windows.Forms.Padding(2);
            this.btn_DeleteMode.Name = "btn_DeleteMode";
            this.btn_DeleteMode.Size = new System.Drawing.Size(66, 28);
            this.btn_DeleteMode.TabIndex = 8;
            this.btn_DeleteMode.Text = "删除配方";
            this.btn_DeleteMode.UseVisualStyleBackColor = true;
            this.btn_DeleteMode.Click += new System.EventHandler(this.btn_DeleteMode_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(336, 5);
            this.btn_Save.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(66, 28);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "保存配方";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // txt_NewModeName
            // 
            this.txt_NewModeName.Location = new System.Drawing.Point(78, 53);
            this.txt_NewModeName.Margin = new System.Windows.Forms.Padding(2);
            this.txt_NewModeName.Name = "txt_NewModeName";
            this.txt_NewModeName.Size = new System.Drawing.Size(222, 21);
            this.txt_NewModeName.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "新名称:";
            // 
            // 参数名称
            // 
            this.参数名称.HeaderText = "参数名称";
            this.参数名称.Name = "参数名称";
            this.参数名称.Width = 80;
            // 
            // 参数代码
            // 
            this.参数代码.HeaderText = "参数代码";
            this.参数代码.Name = "参数代码";
            this.参数代码.Width = 78;
            // 
            // 激光字段名称
            // 
            this.激光字段名称.HeaderText = "参数上限";
            this.激光字段名称.Name = "激光字段名称";
            this.激光字段名称.Width = 78;
            // 
            // 参数下限
            // 
            this.参数下限.HeaderText = "参数下限";
            this.参数下限.Name = "参数下限";
            this.参数下限.Width = 78;
            // 
            // 单位
            // 
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            this.单位.Width = 54;
            // 
            // 是否管控
            // 
            this.是否管控.HeaderText = "是否管控";
            this.是否管控.Name = "是否管控";
            this.是否管控.Width = 78;
            // 
            // 数据来源
            // 
            this.数据来源.HeaderText = "数据来源";
            this.数据来源.Name = "数据来源";
            this.数据来源.Width = 90;
            // 
            // 固定值
            // 
            this.固定值.HeaderText = "数据";
            this.固定值.Name = "固定值";
            this.固定值.Width = 66;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "CCD数据索引";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // ck_Enable
            // 
            this.ck_Enable.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ck_Enable.HeaderText = "是否启用";
            this.ck_Enable.Name = "ck_Enable";
            this.ck_Enable.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ck_Enable.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MesLoadParForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 685);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_NewModeName);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_DeleteMode);
            this.Controls.Add(this.btn_CreatMode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmb_Mode);
            this.Name = "MesLoadParForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MES参数界面";
            this.Load += new System.EventHandler(this.MesLoadParForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Mode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Mode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_paramName;
        private System.Windows.Forms.ComboBox cmb_bControl;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_Mode;
        private System.Windows.Forms.TextBox txt_paramUp;
        private System.Windows.Forms.Label lb_StartPos;
        private System.Windows.Forms.Button btn_MoveDown;
        private System.Windows.Forms.Button btn_MoveUp;
        private System.Windows.Forms.Button btn_CreatMode;
        private System.Windows.Forms.Button btn_DeleteMode;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox txt_NewModeName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_paramLower;
        private System.Windows.Forms.Label lb_EndPos;
        private System.Windows.Forms.TextBox txt_paramUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_paramCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_FixedValue;
        private System.Windows.Forms.Label lb_FixedValue;
        private System.Windows.Forms.ComboBox cmb_DataType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_CCDPos;
        private System.Windows.Forms.Label lb_Pos;
        private System.Windows.Forms.ComboBox cmb_Enable;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_Insert;
        private System.Windows.Forms.DataGridViewTextBoxColumn 参数名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 参数代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 激光字段名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 参数下限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否管控;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数据来源;
        private System.Windows.Forms.DataGridViewTextBoxColumn 固定值;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ck_Enable;
    }
}