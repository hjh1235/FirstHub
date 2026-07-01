namespace UpperComputer
{
    partial class MunLoadMESForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgv_MES = new System.Windows.Forms.DataGridView();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.名字 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.首测上限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.首测下限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.复测上限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.复测下限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.上传值 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MES)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgv_MES);
            this.groupBox1.Location = new System.Drawing.Point(3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(693, 471);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "手动上传参数";
            // 
            // dgv_MES
            // 
            this.dgv_MES.AllowUserToAddRows = false;
            this.dgv_MES.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_MES.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名字,
            this.代码,
            this.首测上限,
            this.首测下限,
            this.复测上限,
            this.复测下限,
            this.单位,
            this.上传值});
            this.dgv_MES.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_MES.Location = new System.Drawing.Point(3, 17);
            this.dgv_MES.Name = "dgv_MES";
            this.dgv_MES.RowTemplate.Height = 23;
            this.dgv_MES.Size = new System.Drawing.Size(687, 451);
            this.dgv_MES.TabIndex = 1;
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(143, 482);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 23);
            this.btn_OK.TabIndex = 1;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(448, 482);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 23);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "取消";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // 名字
            // 
            this.名字.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.名字.HeaderText = "名字";
            this.名字.Name = "名字";
            this.名字.Width = 54;
            // 
            // 代码
            // 
            this.代码.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.代码.HeaderText = "代码";
            this.代码.Name = "代码";
            this.代码.Width = 54;
            // 
            // 首测上限
            // 
            this.首测上限.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.首测上限.HeaderText = "首测上限";
            this.首测上限.Name = "首测上限";
            this.首测上限.Width = 78;
            // 
            // 首测下限
            // 
            this.首测下限.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.首测下限.HeaderText = "首测下限";
            this.首测下限.Name = "首测下限";
            this.首测下限.Width = 78;
            // 
            // 复测上限
            // 
            this.复测上限.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.复测上限.HeaderText = "复测上限";
            this.复测上限.Name = "复测上限";
            this.复测上限.Width = 78;
            // 
            // 复测下限
            // 
            this.复测下限.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.复测下限.HeaderText = "复测下限";
            this.复测下限.Name = "复测下限";
            this.复测下限.Width = 78;
            // 
            // 单位
            // 
            this.单位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            this.单位.Width = 54;
            // 
            // 上传值
            // 
            this.上传值.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.上传值.HeaderText = "上传值";
            this.上传值.Name = "上传值";
            // 
            // MunLoadMESForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 517);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox1);
            this.Name = "MunLoadMESForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动上传";
            this.Load += new System.EventHandler(this.MunLoadMESForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MES)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_MES;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名字;
        private System.Windows.Forms.DataGridViewTextBoxColumn 代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 首测上限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 首测下限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 复测上限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 复测下限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 上传值;
    }
}