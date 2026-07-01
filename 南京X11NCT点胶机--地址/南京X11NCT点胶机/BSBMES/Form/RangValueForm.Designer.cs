namespace UpperComputer
{
    partial class RangValueForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_RangValue = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Online0_1 = new System.Windows.Forms.Button();
            this.btn_Online0_2 = new System.Windows.Forms.Button();
            this.btn_Online1_1 = new System.Windows.Forms.Button();
            this.btn_Online1_2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RangValue)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_RangValue
            // 
            this.dgv_RangValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_RangValue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column2});
            this.dgv_RangValue.Location = new System.Drawing.Point(52, 49);
            this.dgv_RangValue.Name = "dgv_RangValue";
            this.dgv_RangValue.RowHeadersVisible = false;
            this.dgv_RangValue.RowTemplate.Height = 23;
            this.dgv_RangValue.Size = new System.Drawing.Size(523, 427);
            this.dgv_RangValue.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "地址";
            this.Column3.Name = "Column3";
            this.Column3.Width = 54;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column2.HeaderText = "测距值";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // btn_Online0_1
            // 
            this.btn_Online0_1.Location = new System.Drawing.Point(63, 13);
            this.btn_Online0_1.Name = "btn_Online0_1";
            this.btn_Online0_1.Size = new System.Drawing.Size(99, 23);
            this.btn_Online0_1.TabIndex = 1;
            this.btn_Online0_1.Text = "工位0-1监控";
            this.btn_Online0_1.UseVisualStyleBackColor = true;
            this.btn_Online0_1.Click += new System.EventHandler(this.btn_Online0_1_Click);
            // 
            // btn_Online0_2
            // 
            this.btn_Online0_2.Location = new System.Drawing.Point(168, 13);
            this.btn_Online0_2.Name = "btn_Online0_2";
            this.btn_Online0_2.Size = new System.Drawing.Size(99, 23);
            this.btn_Online0_2.TabIndex = 2;
            this.btn_Online0_2.Text = "工位0-2监控";
            this.btn_Online0_2.UseVisualStyleBackColor = true;
            this.btn_Online0_2.Click += new System.EventHandler(this.btn_Online0_2_Click);
            // 
            // btn_Online1_1
            // 
            this.btn_Online1_1.Location = new System.Drawing.Point(361, 13);
            this.btn_Online1_1.Name = "btn_Online1_1";
            this.btn_Online1_1.Size = new System.Drawing.Size(99, 23);
            this.btn_Online1_1.TabIndex = 3;
            this.btn_Online1_1.Text = "工位1-1监控";
            this.btn_Online1_1.UseVisualStyleBackColor = true;
            this.btn_Online1_1.Click += new System.EventHandler(this.btn_Online1_1_Click);
            // 
            // btn_Online1_2
            // 
            this.btn_Online1_2.Location = new System.Drawing.Point(466, 13);
            this.btn_Online1_2.Name = "btn_Online1_2";
            this.btn_Online1_2.Size = new System.Drawing.Size(99, 23);
            this.btn_Online1_2.TabIndex = 4;
            this.btn_Online1_2.Text = "工位1-2监控";
            this.btn_Online1_2.UseVisualStyleBackColor = true;
            this.btn_Online1_2.Click += new System.EventHandler(this.btn_Online1_2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // RangValueForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 477);
            this.Controls.Add(this.btn_Online1_2);
            this.Controls.Add(this.btn_Online1_1);
            this.Controls.Add(this.btn_Online0_2);
            this.Controls.Add(this.btn_Online0_1);
            this.Controls.Add(this.dgv_RangValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RangValueForm";
            this.Text = "RangValueForm";
            this.Load += new System.EventHandler(this.RangValueForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RangValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_RangValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btn_Online0_1;
        private System.Windows.Forms.Button btn_Online0_2;
        private System.Windows.Forms.Button btn_Online1_1;
        private System.Windows.Forms.Button btn_Online1_2;
        private System.Windows.Forms.Timer timer1;
    }
}