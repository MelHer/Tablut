namespace Tablut
{
    partial class frm_Confirmation_Game_Over
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Confirmation_Game_Over));
            this.pic_Confirmation_Game_Over_Finish = new System.Windows.Forms.PictureBox();
            this.lbl_Confirmation_Game_Over_Message = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Game_Over_Finish)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Confirmation_Game_Over_Finish
            // 
            this.pic_Confirmation_Game_Over_Finish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Confirmation_Game_Over_Finish.BackgroundImage")));
            this.pic_Confirmation_Game_Over_Finish.Location = new System.Drawing.Point(188, 271);
            this.pic_Confirmation_Game_Over_Finish.Name = "pic_Confirmation_Game_Over_Finish";
            this.pic_Confirmation_Game_Over_Finish.Size = new System.Drawing.Size(222, 61);
            this.pic_Confirmation_Game_Over_Finish.TabIndex = 0;
            this.pic_Confirmation_Game_Over_Finish.TabStop = false;
            this.pic_Confirmation_Game_Over_Finish.Click += new System.EventHandler(this.pic_Confirmation_Game_Over_Finish_Click);
            // 
            // lbl_Confirmation_Game_Over_Message
            // 
            this.lbl_Confirmation_Game_Over_Message.AutoSize = true;
            this.lbl_Confirmation_Game_Over_Message.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Confirmation_Game_Over_Message.Font = new System.Drawing.Font("Segoe UI", 21.75F);
            this.lbl_Confirmation_Game_Over_Message.Location = new System.Drawing.Point(143, 148);
            this.lbl_Confirmation_Game_Over_Message.Name = "lbl_Confirmation_Game_Over_Message";
            this.lbl_Confirmation_Game_Over_Message.Size = new System.Drawing.Size(310, 40);
            this.lbl_Confirmation_Game_Over_Message.TabIndex = 1;
            this.lbl_Confirmation_Game_Over_Message.Text = "Victoire des attaquants";
            // 
            // frm_Confirmation_Game_Over
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(601, 367);
            this.Controls.Add(this.lbl_Confirmation_Game_Over_Message);
            this.Controls.Add(this.pic_Confirmation_Game_Over_Finish);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Confirmation_Game_Over";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Partie terminée";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Confirmation_Game_Over_Finish)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Confirmation_Game_Over_Finish;
        private System.Windows.Forms.Label lbl_Confirmation_Game_Over_Message;
    }
}