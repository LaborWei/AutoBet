namespace AutoSubmit
{
    partial class FrmConfigSetup
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
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtBackupWeb = new System.Windows.Forms.TextBox();
            this.TxtWeb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(524, 72);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(83, 22);
            this.BtnSave.TabIndex = 83;
            this.BtnSave.Text = "保存";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtBackupWeb
            // 
            this.TxtBackupWeb.Location = new System.Drawing.Point(92, 39);
            this.TxtBackupWeb.Name = "TxtBackupWeb";
            this.TxtBackupWeb.Size = new System.Drawing.Size(515, 21);
            this.TxtBackupWeb.TabIndex = 85;
            // 
            // TxtWeb
            // 
            this.TxtWeb.Location = new System.Drawing.Point(92, 12);
            this.TxtWeb.Name = "TxtWeb";
            this.TxtWeb.Size = new System.Drawing.Size(515, 21);
            this.TxtWeb.TabIndex = 84;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 86;
            this.label2.Text = "网站 ：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 87;
            this.label3.Text = "备用网站 ：";
            // 
            // FrmConfigSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 101);
            this.Controls.Add(this.TxtBackupWeb);
            this.Controls.Add(this.TxtWeb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.BtnSave);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigSetup";
            this.Text = "网站设置";
            this.Load += new System.EventHandler(this.FrmConfigSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtBackupWeb;
        private System.Windows.Forms.TextBox TxtWeb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}