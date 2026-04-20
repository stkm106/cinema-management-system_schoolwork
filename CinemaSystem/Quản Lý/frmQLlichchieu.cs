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
    public partial class frmQLlichchieu : Form
    {
        public frmQLlichchieu()
        {
            InitializeComponent();
        }

        private void LoadComboBox()
        {
            using (var db = new CinemaManagementDBEntities())
            {
                cbPhim.DataSource = db.tbPhims.ToList();
                cbPhim.DisplayMember = "TenPhim"; //hiện tên
                cbPhim.ValueMember = "MaPhim";    //thực tế là chọn mã

                cbPhong.DataSource = db.tbPhongChieux.ToList();
                cbPhong.DisplayMember = "MaPhong";
                cbPhong.ValueMember = "MaPhong";
            }
        }

        private void LoadLichChieu()
        {
            using (var db = new CinemaManagementDBEntities())
            {
                var lichchieu = db.tbLichChieux.Select(lc => new {
                    lc.MaLich,
                    lc.tbPhim.MaPhim,
                    lc.tbPhim.TenPhim,
                    lc.tbPhongChieu.MaPhong,
                    lc.tbPhongChieu.LoaiPhong,
                    lc.NgayChieu,
                    lc.GioChieu,
                    lc.GiaVe
                }).ToList();

                dataGridViewLichChieu.DataSource = lichchieu; //lấy danh sách
                dataGridViewLichChieu.Columns["MaLich"].Visible = false;
                dataGridViewLichChieu.Columns["MaPhim"].Visible = false;
                dataGridViewLichChieu.Columns["MaPhong"].HeaderText = "Số Phòng";
                dataGridViewLichChieu.Columns["TenPhim"].HeaderText = "Tên Phim";
                dataGridViewLichChieu.Columns["LoaiPhong"].HeaderText = "Loại Phòng";
                dataGridViewLichChieu.Columns["NgayChieu"].HeaderText = "Ngày Khởi Chiếu";
                dataGridViewLichChieu.Columns["GioChieu"].HeaderText = "Thời Gian";
                dataGridViewLichChieu.Columns["GiaVe"].HeaderText = "Giá Vé";
                dataGridViewLichChieu.Columns["GiaVe"].DefaultCellStyle.Format = "N0";
            }
        }

        private void frmQLlichchieu_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadLichChieu();
        }

        private void dataGridViewLichChieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //lấy dòng đang chọn, if để tránh chọn dòng rỗng
                DataGridViewRow row = dataGridViewLichChieu.Rows[e.RowIndex];

                cbPhim.Text = row.Cells["TenPhim"].Value.ToString();
                cbPhong.Text = row.Cells["MaPhong"].Value.ToString();
                dtpNgay.Value = Convert.ToDateTime(row.Cells["NgayChieu"].Value);
                if (row.Cells["GioChieu"].Value is TimeSpan time) //convert datetime cần có date nhưng GioChieu không có
                {
                    dtpGio.Value = DateTime.Today.Add(time); //lấy giờ hôm nay + giờ trong SQL
                }
                txtGiaVe.Text = row.Cells["GiaVe"].Value.ToString();
            }
        }

        private void cbPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhim.SelectedValue is int)
            {
                var phim = (tbPhim)cbPhim.SelectedItem; //ép kiểu thành dữ liệu của tbPhim
                if (phim != null && phim.NgayKhoiChieu.HasValue) //không rỗng và có dữ liệu
                {
                    dtpNgay.Value = phim.NgayKhoiChieu.Value;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (var dbLich = new CinemaManagementDBEntities())
            {
                tbLichChieu lc = new tbLichChieu();
                lc.MaPhim = (int)cbPhim.SelectedValue;
                lc.MaPhong = (int)cbPhong.SelectedValue;
                lc.NgayChieu = dtpNgay.Value.Date;
                lc.GioChieu = dtpGio.Value.TimeOfDay;
                lc.GiaVe = decimal.Parse(txtGiaVe.Text);

                dbLich.tbLichChieux.Add(lc);
                dbLich.SaveChanges(); //lưu lại
                LoadLichChieu(); //cập nhật lại bảng
                MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dataGridViewLichChieu.CurrentRow == null)
                return; //chọn dòng rỗng thì bỏ qua

            int maLich = (int)dataGridViewLichChieu.CurrentRow.Cells["MaLich"].Value;
            using (var db = new CinemaManagementDBEntities())
            {
                var lc = db.tbLichChieux.Find(maLich);
                if (lc != null)
                {
                    lc.MaPhim = (int)cbPhim.SelectedValue;
                    lc.MaPhong = (int)cbPhong.SelectedValue;
                    lc.NgayChieu = dtpNgay.Value;
                    lc.GioChieu = dtpGio.Value.TimeOfDay;
                    lc.GiaVe = decimal.Parse(txtGiaVe.Text);
                    db.SaveChanges();
                    LoadLichChieu();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int maLich = (int)dataGridViewLichChieu.CurrentRow.Cells["MaLich"].Value;

            if (MessageBox.Show("Bạn có chắc muốn xóa lịch khởi chiếu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                using (var db = new CinemaManagementDBEntities())
                {
                    var p = db.tbLichChieux.Find(maLich);
                    if (p != null)
                    {
                        db.tbLichChieux.Remove(p); //xóa
                        db.SaveChanges(); //lưu
                        LoadLichChieu();
                    }
                }
            }
        }
    }
}
