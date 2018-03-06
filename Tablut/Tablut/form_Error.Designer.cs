namespace Tablut
{
    partial class frm_Error
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Error));
            this.pic_Error_Confirm = new System.Windows.Forms.PictureBox();
            this.lbl_Error_Title = new System.Windows.Forms.Label();
            this.lbl_Error_Message = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Error_Confirm)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Error_Confirm
            // 
            this.pic_Error_Confirm.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Error_Confirm.BackgroundImage")));
            this.pic_Error_Confirm.Location = new System.Drawing.Point(189, 271);
            this.pic_Error_Confirm.Name = "pic_Error_Confirm";
            this.pic_Error_Confirm.Size = new System.Drawing.Size(222, 61);
            this.pic_Error_Confirm.TabIndex = 15;
            this.pic_Error_Confirm.TabStop = false;
            this.pic_Error_Confirm.Click += new System.EventHandler(this.pic_Error_Confirm_Click);
            // 
            // lbl_Error_Title
            // 
            this.lbl_Error_Title.AutoSize = true;
            this.lbl_Error_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Error_Title.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Error_Title.ForeColor = System.Drawing.Color.Black;
            this.lbl_Error_Title.Location = new System.Drawing.Point(37, 51);
            this.lbl_Error_Title.MaximumSize = new System.Drawing.Size(617, 0);
            this.lbl_Error_Title.Name = "lbl_Error_Title";
            this.lbl_Error_Title.Size = new System.Drawing.Size(81, 40);
            this.lbl_Error_Title.TabIndex = 18;
            this.lbl_Error_Title.Text = "Titre";
            // 
            // lbl_Error_Message
            // 
            this.lbl_Error_Message.AutoSize = true;
            this.lbl_Error_Message.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Error_Message.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Error_Message.ForeColor = System.Drawing.Color.Black;
            this.lbl_Error_Message.Location = new System.Drawing.Point(37, 94);
            this.lbl_Error_Message.MaximumSize = new System.Drawing.Size(617, 0);
            this.lbl_Error_Message.Name = "lbl_Error_Message";
            this.lbl_Error_Message.Size = new System.Drawing.Size(129, 40);
            this.lbl_Error_Message.TabIndex = 19;
            this.lbl_Error_Message.Text = "Message";
            // 
            // frm_Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(601, 367);
            this.Controls.Add(this.lbl_Error_Message);
            this.Controls.Add(this.lbl_Error_Title);
            this.Controls.Add(this.pic_Error_Confirm);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Error";
            this.Text = "Attention";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Error_Confirm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Error_Confirm;
        private System.Windows.Forms.Label lbl_Error_Title;
        private System.Windows.Forms.Label lbl_Error_Message;
    }
}