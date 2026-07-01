
namespace UpperComputer
{
    partial class MESParForm
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
            this.btn_Get = new System.Windows.Forms.Button();
            this.dgv_MES = new System.Windows.Forms.DataGridView();
            this.名字 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.首测上限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.首测下限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.复测上限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.复测下限 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MES)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Get
            // 
            this.btn_Get.Location = new System.Drawing.Point(691, 25);
            this.btn_Get.Name = "btn_Get";
            this.btn_Get.Size = new System.Drawing.Size(75, 23);
            this.btn_Get.TabIndex = 3;
            this.btn_Get.Text = "参数获取";
            this.btn_Get.UseVisualStyleBackColor = true;
            this.btn_Get.Click += new System.EventHandler(this.btn_Get_Click);
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
            this.单位});
            this.dgv_MES.Location = new System.Drawing.Point(12, 12);
            this.dgv_MES.Name = "dgv_MES";
            this.dgv_MES.RowTemplate.Height = 23;
            this.dgv_MES.Size = new System.Drawing.Size(644, 593);
            this.dgv_MES.TabIndex = 2;
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
            this.代码.HeaderText = "代码";
            this.代码.Name = "代码";
            this.代码.Width = 60;
            // 
            // 首测上限
            // 
            this.首测上限.HeaderText = "首测上限";
            this.首测上限.Name = "首测上限";
            this.首测上限.Width = 80;
            // 
            // 首测下限
            // 
            this.首测下限.HeaderText = "首测下限";
            this.首测下限.Name = "首测下限";
            this.首测下限.Width = 80;
            // 
            // 复测上限
            // 
            this.复测上限.HeaderText = "复测上限";
            this.复测上限.Name = "复测上限";
            this.复测上限.Width = 80;
            // 
            // 复测下限
            // 
            this.复测下限.HeaderText = "复测下限";
            this.复测下限.Name = "复测下限";
            this.复测下限.Width = 80;
            // 
            // 单位
            // 
            this.单位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            // 
            // MESParForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 631);
            this.Controls.Add(this.btn_Get);
            this.Controls.Add(this.dgv_MES);
            this.Name = "MESParForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MES下发参数";
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MES)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Get;
        private System.Windows.Forms.DataGridView dgv_MES;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名字;
        private System.Windows.Forms.DataGridViewTextBoxColumn 代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 首测上限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 首测下限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 复测上限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 复测下限;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
    }
}