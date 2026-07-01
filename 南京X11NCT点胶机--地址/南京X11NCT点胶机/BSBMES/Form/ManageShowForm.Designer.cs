namespace UpperComputer
{
    partial class ManageShowForm
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
            this.panel_Select = new System.Windows.Forms.Panel();
            this.btn_RangPos = new System.Windows.Forms.Button();
            this.btn_RangValue = new System.Windows.Forms.Button();
            this.btn_IO = new System.Windows.Forms.Button();
            this.panel_Form = new System.Windows.Forms.Panel();
            this.panel_Select.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_Select
            // 
            this.panel_Select.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Select.Controls.Add(this.btn_RangPos);
            this.panel_Select.Controls.Add(this.btn_RangValue);
            this.panel_Select.Controls.Add(this.btn_IO);
            this.panel_Select.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_Select.Location = new System.Drawing.Point(0, 0);
            this.panel_Select.Name = "panel_Select";
            this.panel_Select.Size = new System.Drawing.Size(204, 631);
            this.panel_Select.TabIndex = 0;
            // 
            // btn_RangPos
            // 
            this.btn_RangPos.Location = new System.Drawing.Point(31, 363);
            this.btn_RangPos.Name = "btn_RangPos";
            this.btn_RangPos.Size = new System.Drawing.Size(133, 44);
            this.btn_RangPos.TabIndex = 2;
            this.btn_RangPos.Text = "测距坐标监控";
            this.btn_RangPos.UseVisualStyleBackColor = true;
            this.btn_RangPos.Visible = false;
            this.btn_RangPos.Click += new System.EventHandler(this.btn_RangPos_Click);
            // 
            // btn_RangValue
            // 
            this.btn_RangValue.Location = new System.Drawing.Point(31, 209);
            this.btn_RangValue.Name = "btn_RangValue";
            this.btn_RangValue.Size = new System.Drawing.Size(133, 44);
            this.btn_RangValue.TabIndex = 1;
            this.btn_RangValue.Text = "测距值监控";
            this.btn_RangValue.UseVisualStyleBackColor = true;
            this.btn_RangValue.Visible = false;
            this.btn_RangValue.Click += new System.EventHandler(this.btn_RangValue_Click);
            // 
            // btn_IO
            // 
            this.btn_IO.Location = new System.Drawing.Point(31, 75);
            this.btn_IO.Name = "btn_IO";
            this.btn_IO.Size = new System.Drawing.Size(133, 44);
            this.btn_IO.TabIndex = 0;
            this.btn_IO.Text = "信号监控";
            this.btn_IO.UseVisualStyleBackColor = true;
            this.btn_IO.Click += new System.EventHandler(this.btn_IO_Click);
            // 
            // panel_Form
            // 
            this.panel_Form.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Form.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Form.Location = new System.Drawing.Point(210, 0);
            this.panel_Form.Name = "panel_Form";
            this.panel_Form.Size = new System.Drawing.Size(887, 631);
            this.panel_Form.TabIndex = 1;
            // 
            // ManageShowForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 631);
            this.Controls.Add(this.panel_Form);
            this.Controls.Add(this.panel_Select);
            this.Name = "ManageShowForm";
            this.Text = "ManageShowForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManageShowForm_FormClosed);
            this.Load += new System.EventHandler(this.ManageShowForm_Load);
            this.panel_Select.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_Select;
        private System.Windows.Forms.Button btn_RangPos;
        private System.Windows.Forms.Button btn_RangValue;
        private System.Windows.Forms.Button btn_IO;
        private System.Windows.Forms.Panel panel_Form;
    }
}