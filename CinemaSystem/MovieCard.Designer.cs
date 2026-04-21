namespace CinemaSystem
{
    partial class ucMovieCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.lblTenPhim = new System.Windows.Forms.Label();
            this.lblTheLoai = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.SuspendLayout();
            // 
            // picPoster
            // 
            this.picPoster.BackColor = System.Drawing.SystemColors.ControlLight;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Top;
            this.picPoster.Location = new System.Drawing.Point(0, 0);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(135, 180);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;
            // 
            // lblTenPhim
            // 
            this.lblTenPhim.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTenPhim.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenPhim.Location = new System.Drawing.Point(0, 180);
            this.lblTenPhim.Name = "lblTenPhim";
            this.lblTenPhim.Size = new System.Drawing.Size(135, 25);
            this.lblTenPhim.TabIndex = 1;
            this.lblTenPhim.Text = "Tên Phim";
            this.lblTenPhim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTheLoai
            // 
            this.lblTheLoai.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTheLoai.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTheLoai.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTheLoai.Location = new System.Drawing.Point(0, 205);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.Size = new System.Drawing.Size(135, 21);
            this.lblTheLoai.TabIndex = 2;
            this.lblTheLoai.Text = "Thể Loại";
            this.lblTheLoai.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ucMovieCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTheLoai);
            this.Controls.Add(this.lblTenPhim);
            this.Controls.Add(this.picPoster);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "ucMovieCard";
            this.Size = new System.Drawing.Size(135, 230);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Label lblTenPhim;
        private System.Windows.Forms.Label lblTheLoai;
    }
}
