
namespace UpperComputer
{
    partial class SNForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCellCode = new System.Windows.Forms.TextBox();
            this.btn_手动过站 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "PACK条码";
            // 
            // txtCellCode
            // 
            this.txtCellCode.Location = new System.Drawing.Point(93, 45);
            this.txtCellCode.Name = "txtCellCode";
            this.txtCellCode.Size = new System.Drawing.Size(256, 21);
            this.txtCellCode.TabIndex = 1;
            // 
            // btn_手动过站
            // 
            this.btn_手动过站.Location = new System.Drawing.Point(375, 45);
            this.btn_手动过站.Name = "btn_手动过站";
            this.btn_手动过站.Size = new System.Drawing.Size(89, 23);
            this.btn_手动过站.TabIndex = 2;
            this.btn_手动过站.Text = "手动过站";
            this.btn_手动过站.UseVisualStyleBackColor = true;
            this.btn_手动过站.Click += new System.EventHandler(this.btn_手动过站_Click);
            // 
            // SNForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 122);
            this.Controls.Add(this.btn_手动过站);
            this.Controls.Add(this.txtCellCode);
            this.Controls.Add(this.label1);
            this.Name = "SNForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "手动过站";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCellCode;
        private System.Windows.Forms.Button btn_手动过站;
    }
}