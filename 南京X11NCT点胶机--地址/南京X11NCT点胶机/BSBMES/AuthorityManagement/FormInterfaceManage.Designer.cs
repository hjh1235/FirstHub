namespace UpperComputer
{
    partial class FormInterfaceManage
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
            this.dataviewInterface = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInterfaceName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tenSelect = new System.Windows.Forms.Button();
            this.cbPermissions = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataviewInterface)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataviewInterface);
            this.groupBox1.Location = new System.Drawing.Point(17, 16);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(273, 445);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "界面管理";
            // 
            // dataviewInterface
            // 
            this.dataviewInterface.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataviewInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataviewInterface.Location = new System.Drawing.Point(4, 22);
            this.dataviewInterface.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataviewInterface.Name = "dataviewInterface";
            this.dataviewInterface.ReadOnly = true;
            this.dataviewInterface.RowHeadersVisible = false;
            this.dataviewInterface.RowTemplate.Height = 23;
            this.dataviewInterface.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataviewInterface.Size = new System.Drawing.Size(265, 419);
            this.dataviewInterface.TabIndex = 0;
            this.dataviewInterface.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataviewInterface_CellClick);
            this.dataviewInterface.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataviewInterface_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "界面名称:";
            // 
            // txtInterfaceName
            // 
            this.txtInterfaceName.Location = new System.Drawing.Point(107, 22);
            this.txtInterfaceName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtInterfaceName.Name = "txtInterfaceName";
            this.txtInterfaceName.Size = new System.Drawing.Size(132, 25);
            this.txtInterfaceName.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(25, 72);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(107, 29);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加界面";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 155);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户权限:";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(157, 72);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(119, 29);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "删除界面";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tenSelect
            // 
            this.tenSelect.Location = new System.Drawing.Point(107, 202);
            this.tenSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tenSelect.Name = "tenSelect";
            this.tenSelect.Size = new System.Drawing.Size(119, 29);
            this.tenSelect.TabIndex = 4;
            this.tenSelect.Text = "查询该权限";
            this.tenSelect.UseVisualStyleBackColor = true;
            this.tenSelect.Click += new System.EventHandler(this.tenSelect_Click);
            // 
            // cbPermissions
            // 
            this.cbPermissions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPermissions.FormattingEnabled = true;
            this.cbPermissions.Location = new System.Drawing.Point(107, 145);
            this.cbPermissions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbPermissions.Name = "cbPermissions";
            this.cbPermissions.Size = new System.Drawing.Size(132, 23);
            this.cbPermissions.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbPermissions);
            this.groupBox2.Controls.Add(this.tenSelect);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtInterfaceName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(299, 20);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(331, 441);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息显示";
            // 
            // FormInterfaceManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 489);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormInterfaceManage";
            this.Text = "FormInterfaceManage";
            this.Load += new System.EventHandler(this.FormInterfaceManage_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataviewInterface)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataviewInterface;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterfaceName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button tenSelect;
        private System.Windows.Forms.ComboBox cbPermissions;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}