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
    public partial class frmDashboard : Form
    {
        List<string> listBanner = new List<string>();
        int currentIndex = 0;

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void ShowBanner(int index)
        {
            if (listBanner.Count > 0 && index < listBanner.Count)
            {
                string fileName = listBanner[index];
                string path = System.IO.Path.Combine(Application.StartupPath, "Posters", fileName);

                if (System.IO.File.Exists(path))
                {
                    using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        // Clone ảnh để giải phóng file stream ngay lập tức
                        picBanner.Image = Image.FromStream(fs);
                    }
                }
            }
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            lblUser.Text = CurrentUser.HoTen;
            using (var db = new CinemaManagementDBEntities())
            {
                var dsPhim = db.tbPhims.ToList();
                flpPhim.Controls.Clear();
                listBanner.Clear();

                foreach (var p in dsPhim)
                {
                    if (!string.IsNullOrEmpty(p.Poster) && p.Poster.StartsWith("p"))
                    {
                        string banner = "ba" + p.Poster.Substring(1);
                        listBanner.Add(banner);
                    }

                    ucMovieCard card = new ucMovieCard();
                    card.TenPhim = p.TenPhim;
                    card.TheLoai = p.TheLoai;
                    card.ImagePath = p.Poster; //lưu đường dẫn ảnh
                    card.LoadImage();

                    flpPhim.Controls.Add(card);
                }
                ShowBanner(0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (listBanner.Count > 0)
            {
                //tăng index lên 1, nếu vượt quá ds thì quay về 0
                currentIndex++;
                if (currentIndex >= listBanner.Count)
                {
                    currentIndex = 0;
                }
                ShowBanner(currentIndex);
            }
        }
    }
}
