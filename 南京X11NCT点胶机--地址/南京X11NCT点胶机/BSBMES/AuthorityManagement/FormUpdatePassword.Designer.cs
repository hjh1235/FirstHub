namespace UpperComputer
{
    partial class FormUpdatePassword
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
            this.账号 = new System.Windows.Forms.TextBox();
            this.原密码 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.新密码 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.修改 = new System.Windows.Forms.Button();
            this.取消 = new System.Windows.Forms.Button();
            this.二次确认新密码 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号:";
            // 
            // 账号
            // 
            this.账号.Location = new System.Drawing.Point(113, 30);
            this.账号.Name = "账号";
            this.账号.Size = new System.Drawing.Size(136, 21);
            this.账号.TabIndex = 1;
            // 
            // 原密码
            // 
            this.原密码.Location = new System.Drawing.Point(113, 75);
            this.原密码.Name = "原密码";
            this.原密码.PasswordChar = '*';
            this.原密码.Size = new System.Drawing.Size(136, 21);
            this.原密码.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(53, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "原密码:";
            // 
            // 新密码
            // 
            this.新密码.Location = new System.Drawing.Point(113, 118);
            this.新密码.Name = "新密码";
            this.新密码.PasswordChar = '*';
            this.新密码.Size = new System.Drawing.Size(136, 21);
            this.新密码.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(53, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "新密码:";
            // 
            // 修改
            // 
            this.修改.Location = new System.Drawing.Point(84, 199);
            this.修改.Name = "修改";
            this.修改.Size = new System.Drawing.Size(75, 31);
            this.修改.TabIndex = 6;
            this.修改.Text = "修改";
            this.修改.UseVisualStyleBackColor = true;
            this.修改.Click += new System.EventHandler(this.修改_Click);
            // 
            // 取消
            // 
            this.取消.Location = new System.Drawing.Point(196, 199);
            this.取消.Name = "取消";
            this.取消.Size = new System.Drawing.Size(75, 31);
            this.取消.TabIndex = 7;
            this.取消.Text = "取消";
            this.取消.UseVisualStyleBackColor = true;
            this.取消.Click += new System.EventHandler(this.取消_Click);
            // 
            // 二次确认新密码
            // 
            this.二次确认新密码.Location = new System.Drawing.Point(113, 155);
            this.二次确认新密码.Name = "二次确认新密码";
            this.二次确认新密码.PasswordChar = '*';
            this.二次确认新密码.Size = new System.Drawing.Size(136, 21);
            this.二次确认新密码.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "再次输入新密码:";
            // 
            // FormUpdatePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(367, 261);
            this.Controls.Add(this.二次确认新密码);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.取消);
            this.Controls.Add(this.修改);
            this.Controls.Add(this.新密码);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.原密码);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.账号);
            this.Controls.Add(this.label1);
            this.Name = "FormUpdatePassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码修改";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 账号;
        private System.Windows.Forms.TextBox 原密码;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox 新密码;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button 修改;
        private System.Windows.Forms.Button 取消;
        private System.Windows.Forms.TextBox 二次确认新密码;
        private System.Windows.Forms.Label label4;
    }
}