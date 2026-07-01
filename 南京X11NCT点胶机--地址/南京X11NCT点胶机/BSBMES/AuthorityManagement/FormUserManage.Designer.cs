namespace UpperComputer
{
    partial class FormUserManage
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
            this.dgv_UserManage = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.用户名 = new System.Windows.Forms.TextBox();
            this.密码 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.添加 = new System.Windows.Forms.Button();
            this.删除 = new System.Windows.Forms.Button();
            this.权限 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UserManage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_UserManage
            // 
            this.dgv_UserManage.BackgroundColor = System.Drawing.Color.White;
            this.dgv_UserManage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_UserManage.Location = new System.Drawing.Point(12, 12);
            this.dgv_UserManage.MultiSelect = false;
            this.dgv_UserManage.Name = "dgv_UserManage";
            this.dgv_UserManage.ReadOnly = true;
            this.dgv_UserManage.RowTemplate.Height = 23;
            this.dgv_UserManage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_UserManage.Size = new System.Drawing.Size(386, 241);
            this.dgv_UserManage.TabIndex = 0;
            this.dgv_UserManage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_UserManage_CellClick);
            this.dgv_UserManage.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_UserManage_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(448, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "账  号：";
            // 
            // 用户名
            // 
            this.用户名.Location = new System.Drawing.Point(505, 36);
            this.用户名.Name = "用户名";
            this.用户名.Size = new System.Drawing.Size(144, 21);
            this.用户名.TabIndex = 2;
            // 
            // 密码
            // 
            this.密码.Location = new System.Drawing.Point(505, 75);
            this.密码.Name = "密码";
            this.密码.Size = new System.Drawing.Size(144, 21);
            this.密码.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "密  码：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(448, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "权  限：";
            // 
            // 添加
            // 
            this.添加.Location = new System.Drawing.Point(482, 183);
            this.添加.Name = "添加";
            this.添加.Size = new System.Drawing.Size(75, 30);
            this.添加.TabIndex = 7;
            this.添加.Text = "添加";
            this.添加.UseVisualStyleBackColor = true;
            this.添加.Click += new System.EventHandler(this.添加_Click);
            // 
            // 删除
            // 
            this.删除.Location = new System.Drawing.Point(574, 183);
            this.删除.Name = "删除";
            this.删除.Size = new System.Drawing.Size(75, 30);
            this.删除.TabIndex = 8;
            this.删除.Text = "删除";
            this.删除.UseVisualStyleBackColor = true;
            this.删除.Click += new System.EventHandler(this.删除_Click);
            // 
            // 权限
            // 
            this.权限.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.权限.FormattingEnabled = true;
            this.权限.Items.AddRange(new object[] {
            "技术员",
            "工程师",
            "管理员"});
            this.权限.Location = new System.Drawing.Point(505, 121);
            this.权限.Name = "权限";
            this.权限.Size = new System.Drawing.Size(144, 20);
            this.权限.TabIndex = 9;
            // 
            // FormUserManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(776, 400);
            this.Controls.Add(this.权限);
            this.Controls.Add(this.删除);
            this.Controls.Add(this.添加);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.密码);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.用户名);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_UserManage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormUserManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.FormUserManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_UserManage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_UserManage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 用户名;
        private System.Windows.Forms.TextBox 密码;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button 添加;
        private System.Windows.Forms.Button 删除;
        private System.Windows.Forms.ComboBox 权限;
    }
}