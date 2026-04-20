using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaSystem.Quản_Lý
{
    public partial class frmQLphim : Form
    {
        public frmQLphim()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            using (var dbPhim = new CinemaManagementDBEntities()) //dùng variable
            {
                var listPhim = dbPhim.tbPhims.ToList(); //lấy danh sách
                dataGridViewDSphim.DataSource = listPhim;
                //đặt lại tên hiển thị trong gridview
                dataGridViewDSphim.Columns["MaPhim"].Visible = false; //ẩn cột mã phim
                dataGridViewDSphim.Columns["TenPhim"].HeaderText = "Tên Phim";
                dataGridViewDSphim.Columns["DaoDien"].HeaderText = "Đạo Diễn";
                dataGridViewDSphim.Columns["TheLoai"].HeaderText = "Thể Loại";
                dataGridViewDSphim.Columns["ThoiLuong"].HeaderText = "Thời Lượng (Phút)";
                dataGridViewDSphim.Columns["NgayKhoiChieu"].HeaderText = "Ngày Khởi Chiếu";
                dataGridViewDSphim.Columns["NgayKhoiChieu"].DefaultCellStyle.Format = "dd/MM/yyyy"; //sửa thành ngày tháng năm
                dataGridViewDSphim.Columns["Poster"].Visible = false; //ẩn cột poster tránh hiện đường dẫn ảnh
                dataGridViewDSphim.Columns["tbLichChieux"].Visible = false; //ẩn cột liên kết khóa chính
            }
        }

        private void frmQLphim_Load(object sender, EventArgs e)
        {
            cbTheLoai.Items.Add("Hài hước");
            cbTheLoai.Items.Add("Hành động");
            cbTheLoai.Items.Add("Hoạt hình");
            cbTheLoai.Items.Add("Kinh dị");
            cbTheLoai.Items.Add("Lãng mạn");
            cbTheLoai.Items.Add("Phiêu lưu");
            cbTheLoai.Items.Add("Tài liệu");
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var dbPhim = new CinemaManagementDBEntities())
            {
                tbPhim p = new tbPhim();
                p.TenPhim = txtTenPhim.Text;
                p.DaoDien = txtDaoDien.Text;
                p.TheLoai = cbTheLoai.Text;
                p.ThoiLuong = (int)numThoiLuong.Value;
                p.NgayKhoiChieu = dtpNgayKhoiChieu.Value;
                p.Poster = txtPosterName.Text;

                dbPhim.tbPhims.Add(p);
                dbPhim.SaveChanges(); //lưu lại
                LoadData(); //cập nhật lại bảng
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewDSphim.CurrentRow == null) 
                return; //chọn dòng rỗng thì bỏ qua

            int maPhim = (int)dataGridViewDSphim.CurrentRow.Cells["MaPhim"].Value;
                using (var db = new CinemaManagementDBEntities())
                {
                    var p = db.tbPhims.Find(maPhim);
                    if (p != null)
                    {
                    p.TenPhim = txtTenPhim.Text;
                    p.DaoDien = txtDaoDien.Text;
                    p.TheLoai = cbTheLoai.Text;
                    p.ThoiLuong = (int)numThoiLuong.Value;
                    p.NgayKhoiChieu = dtpNgayKhoiChieu.Value;
                    p.Poster = txtPosterName.Text;
                    db.SaveChanges();
                    LoadData();
                    }
                }
        }

        private void dataGridViewDSphim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //lấy dòng đang chọn, if để tránh chọn dòng rỗng
                DataGridViewRow row = dataGridViewDSphim.Rows[e.RowIndex];

                txtTenPhim.Text = row.Cells["TenPhim"].Value.ToString();
                txtDaoDien.Text = row.Cells["DaoDien"].Value.ToString();
                cbTheLoai.Text = row.Cells["TheLoai"].Value.ToString();
                numThoiLuong.Value = Convert.ToInt32(row.Cells["ThoiLuong"].Value);
                dtpNgayKhoiChieu.Value = Convert.ToDateTime(row.Cells["NgayKhoiChieu"].Value);

                //hiển thị luôn hình ảnh lên picturebox
                string fileName = row.Cells["Poster"].Value.ToString();
                string path = Path.Combine(Application.StartupPath, "Posters", fileName);
                if (File.Exists(path))
                {
                    if (picPoster.Image != null)
                        picPoster.Image.Dispose(); //giải phóng ram ảnh cũ trước khi nạp ảnh mới (máy yếu)
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) //đọc đường dẫn, mở file ở đường dẫn
                    {
                        picPoster.Image = Image.FromStream(fs);
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maPhim = (int)dataGridViewDSphim.CurrentRow.Cells["MaPhim"].Value;

            if (MessageBox.Show("Bạn có chắc muốn xóa phim này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                using (var db = new CinemaManagementDBEntities())
                {
                    var p = db.tbPhims.Find(maPhim);
                    if (p != null)
                    {
                        db.tbPhims.Remove(p); //xóa
                        db.SaveChanges(); //lưu
                        LoadData();
                    }
                }
            }
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            if (open.ShowDialog() == DialogResult.OK)
            {
                picPoster.Image = Image.FromFile(open.FileName); //hiện ảnh lên picturebox để xem trước
                txtPosterName.Text = Path.GetFileName(open.FileName); //lưu tên file để lát nữa lưu vào SQL
            }
        }
    }
}
