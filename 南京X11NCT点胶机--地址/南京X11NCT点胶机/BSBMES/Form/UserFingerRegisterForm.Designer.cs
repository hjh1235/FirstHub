namespace UpperComputer
{
    partial class UserFingerRegisterForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUserFingerInformation = new System.Windows.Forms.DataGridView();
            this.CUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PanelOperation = new System.Windows.Forms.Panel();
            this.labUserLevel = new System.Windows.Forms.Label();
            this.cmbUserLevel = new System.Windows.Forms.ComboBox();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.labUserName = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.PromptText1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserFingerInformation)).BeginInit();
            this.PanelOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.Controls.Add(this.dgvUserFingerInformation, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.PanelOperation, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(530, 400);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvUserFingerInformation
            // 
            this.dgvUserFingerInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUserFingerInformation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CUserName,
            this.Column2});
            this.dgvUserFingerInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUserFingerInformation.Location = new System.Drawing.Point(3, 3);
            this.dgvUserFingerInformation.Name = "dgvUserFingerInformation";
            this.dgvUserFingerInformation.RowTemplate.Height = 23;
            this.dgvUserFingerInformation.Size = new System.Drawing.Size(338, 378);
            this.dgvUserFingerInformation.TabIndex = 0;
            // 
            // CUserName
            // 
            this.CUserName.HeaderText = "名称";
            this.CUserName.Name = "CUserName";
            this.CUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "职位";
            this.Column2.Name = "Column2";
            // 
            // PanelOperation
            // 
            this.PanelOperation.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.PanelOperation.Controls.Add(this.labUserLevel);
            this.PanelOperation.Controls.Add(this.cmbUserLevel);
            this.PanelOperation.Controls.Add(this.txbUserName);
            this.PanelOperation.Controls.Add(this.labUserName);
            this.PanelOperation.Controls.Add(this.btnDelete);
            this.PanelOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelOperation.Location = new System.Drawing.Point(347, 3);
            this.PanelOperation.Name = "PanelOperation";
            this.PanelOperation.Size = new System.Drawing.Size(180, 378);
            this.PanelOperation.TabIndex = 1;
            // 
            // labUserLevel
            // 
            this.labUserLevel.AutoSize = true;
            this.labUserLevel.Location = new System.Drawing.Point(22, 88);
            this.labUserLevel.Name = "labUserLevel";
            this.labUserLevel.Size = new System.Drawing.Size(29, 12);
            this.labUserLevel.TabIndex = 5;
            this.labUserLevel.Text = "职位";
            // 
            // cmbUserLevel
            // 
            this.cmbUserLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUserLevel.FormattingEnabled = true;
            this.cmbUserLevel.Items.AddRange(new object[] {
            "管理员",
            "工程师",
            "操作员"});
            this.cmbUserLevel.Location = new System.Drawing.Point(71, 85);
            this.cmbUserLevel.Name = "cmbUserLevel";
            this.cmbUserLevel.Size = new System.Drawing.Size(100, 20);
            this.cmbUserLevel.TabIndex = 4;
            // 
            // txbUserName
            // 
            this.txbUserName.Location = new System.Drawing.Point(71, 37);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(100, 21);
            this.txbUserName.TabIndex = 3;
            // 
            // labUserName
            // 
            this.labUserName.AutoSize = true;
            this.labUserName.Location = new System.Drawing.Point(22, 40);
            this.labUserName.Name = "labUserName";
            this.labUserName.Size = new System.Drawing.Size(29, 12);
            this.labUserName.TabIndex = 2;
            this.labUserName.Text = "名称";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(24, 131);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(99, 23);
            this.btnDelete.TabIndex = 0;
            this.btnDelete.Text = "删除用户指纹";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(533, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "提示信息:";
            // 
            // PromptText1
            // 
            this.PromptText1.CausesValidation = false;
            this.PromptText1.Location = new System.Drawing.Point(536, 22);
            this.PromptText1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PromptText1.Name = "PromptText1";
            this.PromptText1.Size = new System.Drawing.Size(193, 370);
            this.PromptText1.TabIndex = 9;
            this.PromptText1.Text = "";
            // 
            // UserFingerRegisterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 400);
            this.Controls.Add(this.PromptText1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "UserFingerRegisterForm";
            this.Text = "用户指纹注册界面";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserFingerRegisterForm_Close);
            this.Load += new System.EventHandler(this.UserFingerRegisterForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserFingerInformation)).EndInit();
            this.PanelOperation.ResumeLayout(false);
            this.PanelOperation.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvUserFingerInformation;
        private System.Windows.Forms.Panel PanelOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label labUserLevel;
        private System.Windows.Forms.ComboBox cmbUserLevel;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.Label labUserName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox PromptText1;
    }
}