namespace Tablut
{
    partial class frm_Renaming
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Renaming));
            this.lbl_Renaming_New_Name = new System.Windows.Forms.Label();
            this.lbl_Renaming_Fail = new System.Windows.Forms.Label();
            this.txt_Renaming_Profile_Name = new System.Windows.Forms.TextBox();
            this.pic_Renaming_Confirm = new System.Windows.Forms.PictureBox();
            this.pic_Renaming_Cancel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Renaming_Confirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Renaming_Cancel)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Renaming_New_Name
            // 
            this.lbl_Renaming_New_Name.AutoSize = true;
            this.lbl_Renaming_New_Name.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Renaming_New_Name.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Renaming_New_Name.ForeColor = System.Drawing.Color.Black;
            this.lbl_Renaming_New_Name.Location = new System.Drawing.Point(63, 109);
            this.lbl_Renaming_New_Name.Name = "lbl_Renaming_New_Name";
            this.lbl_Renaming_New_Name.Size = new System.Drawing.Size(204, 40);
            this.lbl_Renaming_New_Name.TabIndex = 13;
            this.lbl_Renaming_New_Name.Text = "Nouveau nom:";
            // 
            // lbl_Renaming_Fail
            // 
            this.lbl_Renaming_Fail.AutoSize = true;
            this.lbl_Renaming_Fail.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Renaming_Fail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Renaming_Fail.ForeColor = System.Drawing.Color.Red;
            this.lbl_Renaming_Fail.Location = new System.Drawing.Point(66, 190);
            this.lbl_Renaming_Fail.Name = "lbl_Renaming_Fail";
            this.lbl_Renaming_Fail.Size = new System.Drawing.Size(136, 21);
            this.lbl_Renaming_Fail.TabIndex = 12;
            this.lbl_Renaming_Fail.Text = "lbl_Fail_Renaming";
            this.lbl_Renaming_Fail.Visible = false;
            // 
            // txt_Renaming_Profile_Name
            // 
            this.txt_Renaming_Profile_Name.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Renaming_Profile_Name.Location = new System.Drawing.Point(69, 152);
            this.txt_Renaming_Profile_Name.MaxLength = 20;
            this.txt_Renaming_Profile_Name.Name = "txt_Renaming_Profile_Name";
            this.txt_Renaming_Profile_Name.Size = new System.Drawing.Size(483, 35);
            this.txt_Renaming_Profile_Name.TabIndex = 11;
            // 
            // pic_Renaming_Confirm
            // 
            this.pic_Renaming_Confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Renaming_Confirm.BackgroundImage")));
            this.pic_Renaming_Confirm.Location = new System.Drawing.Point(58, 271);
            this.pic_Renaming_Confirm.Name = "pic_Renaming_Confirm";
            this.pic_Renaming_Confirm.Size = new System.Drawing.Size(222, 61);
            this.pic_Renaming_Confirm.TabIndex = 16;
            this.pic_Renaming_Confirm.TabStop = false;
            this.pic_Renaming_Confirm.Click += new System.EventHandler(this.pic_Renaming_Confirm_Click);
            // 
            // pic_Renaming_Cancel
            // 
            this.pic_Renaming_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Renaming_Cancel.BackgroundImage")));
            this.pic_Renaming_Cancel.Location = new System.Drawing.Point(319, 271);
            this.pic_Renaming_Cancel.Name = "pic_Renaming_Cancel";
            this.pic_Renaming_Cancel.Size = new System.Drawing.Size(222, 61);
            this.pic_Renaming_Cancel.TabIndex = 15;
            this.pic_Renaming_Cancel.TabStop = false;
            this.pic_Renaming_Cancel.Click += new System.EventHandler(this.pic_Renaming_Cancel_Click);
            // 
            // frm_Renaming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(601, 367);
            this.Controls.Add(this.pic_Renaming_Confirm);
            this.Controls.Add(this.pic_Renaming_Cancel);
            this.Controls.Add(this.lbl_Renaming_New_Name);
            this.Controls.Add(this.lbl_Renaming_Fail);
            this.Controls.Add(this.txt_Renaming_Profile_Name);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Renaming";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renommer profil";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Renaming_Confirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Renaming_Cancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Renaming_New_Name;
        private System.Windows.Forms.Label lbl_Renaming_Fail;
        private System.Windows.Forms.TextBox txt_Renaming_Profile_Name;
        private System.Windows.Forms.PictureBox pic_Renaming_Confirm;
        private System.Windows.Forms.PictureBox pic_Renaming_Cancel;
    }
}