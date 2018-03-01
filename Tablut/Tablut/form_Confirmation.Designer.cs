namespace Tablut
{
    partial class frm_Confirmation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Confirmation));
            this.pic_Confirmation_Cancel = new System.Windows.Forms.PictureBox();
            this.pic_Confirmation_Confirm = new System.Windows.Forms.PictureBox();
            this.lbl_Confirmation_Message = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Cancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Confirmation_Cancel
            // 
            this.pic_Confirmation_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Confirmation_Cancel.BackgroundImage")));
            this.pic_Confirmation_Cancel.Location = new System.Drawing.Point(319, 271);
            this.pic_Confirmation_Cancel.Name = "pic_Confirmation_Cancel";
            this.pic_Confirmation_Cancel.Size = new System.Drawing.Size(222, 61);
            this.pic_Confirmation_Cancel.TabIndex = 13;
            this.pic_Confirmation_Cancel.TabStop = false;
            this.pic_Confirmation_Cancel.Click += new System.EventHandler(this.pic_Confirmation_Cancel_Click);
            // 
            // pic_Confirmation_Confirm
            // 
            this.pic_Confirmation_Confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Confirmation_Confirm.BackgroundImage")));
            this.pic_Confirmation_Confirm.Location = new System.Drawing.Point(58, 271);
            this.pic_Confirmation_Confirm.Name = "pic_Confirmation_Confirm";
            this.pic_Confirmation_Confirm.Size = new System.Drawing.Size(222, 61);
            this.pic_Confirmation_Confirm.TabIndex = 14;
            this.pic_Confirmation_Confirm.TabStop = false;
            this.pic_Confirmation_Confirm.Click += new System.EventHandler(this.pic_Confirmation_Confirm_Click);
            // 
            // lbl_Confirmation_Message
            // 
            this.lbl_Confirmation_Message.AutoSize = true;
            this.lbl_Confirmation_Message.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Confirmation_Message.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Confirmation_Message.ForeColor = System.Drawing.Color.Black;
            this.lbl_Confirmation_Message.Location = new System.Drawing.Point(37, 51);
            this.lbl_Confirmation_Message.MaximumSize = new System.Drawing.Size(617, 0);
            this.lbl_Confirmation_Message.Name = "lbl_Confirmation_Message";
            this.lbl_Confirmation_Message.Size = new System.Drawing.Size(129, 40);
            this.lbl_Confirmation_Message.TabIndex = 17;
            this.lbl_Confirmation_Message.Text = "Message";
            // 
            // frm_Confirmation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(601, 367);
            this.Controls.Add(this.lbl_Confirmation_Message);
            this.Controls.Add(this.pic_Confirmation_Confirm);
            this.Controls.Add(this.pic_Confirmation_Cancel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Confirmation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Confirmation";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Cancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Confirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Confirmation_Cancel;
        private System.Windows.Forms.PictureBox pic_Confirmation_Confirm;
        private System.Windows.Forms.Label lbl_Confirmation_Message;
    }
}