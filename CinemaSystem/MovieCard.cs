using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaSystem
{
    public partial class ucMovieCard : UserControl
    {
        public ucMovieCard()
        {
            InitializeComponent();
        }

        //gán Tên phim
        public string TenPhim
        {
            get { return lblTenPhim.Text; }
            set { lblTenPhim.Text = value; }
        }

        //gán Thể loại
        public string TheLoai
        {
            get { return lblTheLoai.Text; }
            set { lblTheLoai.Text = value; }
        }

        //gán Hình ảnh
        public string ImagePath { get; set; }
        
        public void LoadImage()
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {
                // Kết hợp đường dẫn từ StartupPath + thư mục Posters + tên file
                string fullPath = System.IO.Path.Combine(Application.StartupPath, "Posters", ImagePath);

                if (System.IO.File.Exists(fullPath))
                {
                    // Dùng FileStream để tránh việc khóa file ảnh (giúp bạn xóa/sửa ảnh dễ hơn)
                    using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        picPoster.Image = Image.FromStream(fs);
                    }
                }
            }
        }
    }
}
