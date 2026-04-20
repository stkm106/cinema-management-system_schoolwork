using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaSystem.Quản_Lý
{
    public partial class frmQLphong : Form
    {
        public frmQLphong()
        {
            InitializeComponent();
        }
        public void LoadData()
        {
            using (var dbPhong = new CinemaManagementDBEntities()) //dùng variable
            {
                var listPhong = dbPhong.tbPhongChieux.ToList(); //lấy danh sách
                dataGridViewPhongChieu.DataSource = listPhong;
                //đặt lại tên hiển thị trong gridview
                dataGridViewPhongChieu.Columns["MaPhong"].HeaderText = "Số phòng"; //ẩn cột mã phim
                dataGridViewPhongChieu.Columns["LoaiPhong"].HeaderText = "Loại phòng";
                dataGridViewPhongChieu.Columns["SoGhe"].HeaderText = "Số ghế ngồi";
                dataGridViewPhongChieu.Columns["tbLichChieux"].Visible = false; //ẩn cột liên kết khóa chính
            }
        }

        private void frmQLphong_Load(object sender, EventArgs e)
        {
            cbLoaiPhong.Items.Add("2D");
            cbLoaiPhong.Items.Add("3D");
            cbLoaiPhong.Items.Add("IMAX");
            LoadData();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var dbPhong = new CinemaManagementDBEntities())
            {
                tbPhongChieu p = new tbPhongChieu();
                p.MaPhong = (int)numSoPhong.Value;
                p.LoaiPhong = cbLoaiPhong.Text;
                p.SoGhe = (int)numSoGhe.Value;
                
                dbPhong.tbPhongChieux.Add(p);
                dbPhong.SaveChanges(); //lưu lại
                LoadData(); //cập nhật lại bảng
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewPhongChieu.CurrentRow == null)
                return; //chọn dòng rỗng thì bỏ qua

            int maPhong = (int)dataGridViewPhongChieu.CurrentRow.Cells["MaPhong"].Value;
                using (var db = new CinemaManagementDBEntities())
                {
                    var p = db.tbPhongChieux.Find(maPhong);
                    if (p != null)
                    {
                    p.LoaiPhong = cbLoaiPhong.Text;
                    p.SoGhe = (int)numSoGhe.Value;
                    db.SaveChanges();
                    LoadData();
                    }
                }
        }

        private void dataGridViewPhongChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //lấy dòng đang chọn, if để tránh chọn dòng rỗng
                DataGridViewRow row = dataGridViewPhongChieu.Rows[e.RowIndex];

                numSoPhong.Value = Convert.ToInt32(row.Cells["MaPhong"].Value);
                cbLoaiPhong.Text = row.Cells["LoaiPhong"].Value.ToString();
                numSoGhe.Value = Convert.ToInt32(row.Cells["SoGhe"].Value);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maPhong = (int)dataGridViewPhongChieu.CurrentRow.Cells["MaPhong"].Value;

            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                using (var db = new CinemaManagementDBEntities())
                {
                    var p = db.tbPhongChieux.Find(maPhong);
                    if (p != null)
                    {
                        db.tbPhongChieux.Remove(p); //xóa
                        db.SaveChanges(); //lưu
                        LoadData();
                    }
                }
            }
        }
    }
}
