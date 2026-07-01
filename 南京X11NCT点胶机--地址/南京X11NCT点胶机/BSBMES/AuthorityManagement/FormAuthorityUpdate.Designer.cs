namespace UpperComputer
{
    partial class FormAuthorityUpdate
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tenSelect = new System.Windows.Forms.Button();
            this.dataGridSource = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPermissionsLevel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDelPermissions = new System.Windows.Forms.Button();
            this.cbPermissions = new System.Windows.Forms.ComboBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(119, 168);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(82, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "用户添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(24, 207);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(72, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "用户修改";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(119, 207);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(82, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "用户删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tenSelect
            // 
            this.tenSelect.Location = new System.Drawing.Point(24, 168);
            this.tenSelect.Name = "tenSelect";
            this.tenSelect.Size = new System.Drawing.Size(72, 23);
            this.tenSelect.TabIndex = 4;
            this.tenSelect.Text = "用户查询";
            this.tenSelect.UseVisualStyleBackColor = true;
            this.tenSelect.Click += new System.EventHandler(this.tenSelect_Click);
            // 
            // dataGridSource
            // 
            this.dataGridSource.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSource.Location = new System.Drawing.Point(3, 17);
            this.dataGridSource.Name = "dataGridSource";
            this.dataGridSource.ReadOnly = true;
            this.dataGridSource.RowHeadersVisible = false;
            this.dataGridSource.RowTemplate.Height = 23;
            this.dataGridSource.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSource.Size = new System.Drawing.Size(503, 233);
            this.dataGridSource.TabIndex = 5;
            this.dataGridSource.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridSource_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(509, 253);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户权限显示";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPermissionsLevel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnDelPermissions);
            this.groupBox2.Controls.Add(this.cbPermissions);
            this.groupBox2.Controls.Add(this.txtUserPassword);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tenSelect);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnUpdate);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtUserName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtUserID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(527, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(297, 250);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息显示";
            // 
            // txtPermissionsLevel
            // 
            this.txtPermissionsLevel.Location = new System.Drawing.Point(75, 141);
            this.txtPermissionsLevel.Name = "txtPermissionsLevel";
            this.txtPermissionsLevel.Size = new System.Drawing.Size(100, 21);
            this.txtPermissionsLevel.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 146);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "权限等级:";
            // 
            // btnDelPermissions
            // 
            this.btnDelPermissions.Location = new System.Drawing.Point(193, 83);
            this.btnDelPermissions.Name = "btnDelPermissions";
            this.btnDelPermissions.Size = new System.Drawing.Size(98, 23);
            this.btnDelPermissions.TabIndex = 9;
            this.btnDelPermissions.Text = "删除选中权限";
            this.btnDelPermissions.UseVisualStyleBackColor = true;
            this.btnDelPermissions.Click += new System.EventHandler(this.btnDelPermissions_Click);
            // 
            // cbPermissions
            // 
            this.cbPermissions.FormattingEnabled = true;
            this.cbPermissions.Location = new System.Drawing.Point(75, 83);
            this.cbPermissions.Name = "cbPermissions";
            this.cbPermissions.Size = new System.Drawing.Size(100, 20);
            this.cbPermissions.TabIndex = 8;
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Location = new System.Drawing.Point(75, 114);
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(100, 21);
            this.txtUserPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户密码:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户权限:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(75, 51);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 21);
            this.txtUserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户姓名:";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(75, 18);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(100, 21);
            this.txtUserID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户ID:";
            // 
            // FormAuthorityUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 291);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAuthorityUpdate";
            this.Text = "权限管理";
            this.Load += new System.EventHandler(this.FormAuthorityUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button tenSelect;
        private System.Windows.Forms.DataGridView dataGridSource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPermissions;
        private System.Windows.Forms.Button btnDelPermissions;
        private System.Windows.Forms.TextBox txtPermissionsLevel;
        private System.Windows.Forms.Label label5;
    }
}