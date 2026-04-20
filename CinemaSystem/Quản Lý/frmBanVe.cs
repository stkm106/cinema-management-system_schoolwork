using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace CinemaSystem
{
    public partial class frmBanVe : Form
    {
        public frmBanVe()
        {
            InitializeComponent();
        }

        private void VeSoDoGhe(int tongSoGhe)
        {
            if (cbLichChieu.SelectedValue == null || !(cbLichChieu.SelectedValue is int))
                return;

            using (var db = new CinemaManagementDBEntities())
            {
                int maLich = (int)cbLichChieu.SelectedValue;
                //truy xuất danh sách các ghế đã bán
                var gheDaBan = db.tbVeXemPhims
                                 .Where(v => v.MaLich == maLich)
                                 .Select(v => v.ViTriGhe)
                                 .ToList();
                flpGhe.Controls.Clear(); //xóa ghế cũ

                for (int i = 1; i <= tongSoGhe; i++)
                {
                    Button btn = new Button();
                    btn.Text = i.ToString();
                    btn.Width = 40;
                    btn.Height = 40;
                    btn.Font = new Font("Calibri", 15, FontStyle.Bold);
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.BackColor = Color.FromArgb(242, 214, 194); //mặc định ghế trống

                    //sender tọa độ của button
                    btn.Click += (s, ev) =>
                    {
                        Button btnGhe = (Button)s;

                        //lấy giá vé txt (xóa . hàng nghìn nếu có)
                        decimal giaVe = decimal.Parse(txtGiaVe.Text.Replace(".", ""));
                        decimal tongTien = decimal.Parse(txtTongTien.Text.Replace(".", ""));

                        if (btnGhe.BackColor == Color.FromArgb(242, 214, 194)) //ghế trống
                        {
                            btnGhe.BackColor = Color.Yellow;
                            tongTien += giaVe;
                        }
                        else //ghế đang màu vàng muốn bỏ chọn
                        {
                            btnGhe.BackColor = Color.FromArgb(242, 214, 194);
                            tongTien -= giaVe;
                        }
                        txtTongTien.Text = tongTien.ToString("N0"); //cập nhật tổng tiền
                    };
                    
                    if (gheDaBan.Contains(btn.Text))
                    {
                        btn.BackColor = Color.Red; //ghế đã bán
                        btn.Enabled = false;       
                    }

                    flpGhe.Controls.Add(btn);
                }
            }
        }

        private void frmBanVe_Load(object sender, EventArgs e)
        {
            using (var db = new CinemaManagementDBEntities())
            {
                cbPhim.DataSource = db.tbPhims.ToList();
                cbPhim.DisplayMember = "TenPhim";
                cbPhim.ValueMember = "MaPhim";
                cbPhim.SelectedIndex = -1; //để trống lúc mới mở
            }
        }

        private void cbPhim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPhim.SelectedValue == null || !(cbPhim.SelectedValue is int))
                return;

            if (cbPhim.SelectedValue is int maPhim)
            {
                using (var db = new CinemaManagementDBEntities())
                {
                    //truy xuất lịch chiếu từ bảng lịch chiếu
                    var dsLichChieu = db.tbLichChieux
                        .Include(x => x.tbPhongChieu) //tải nhanh, lấy hết dữ liệu cần có từ các bảng rồi mới đóng connect
                        .Where(lc => lc.MaPhim == maPhim)
                        .ToList();

                    cbLichChieu.DataSource = dsLichChieu;
                    cbLichChieu.DisplayMember = "GioChieu";
                    cbLichChieu.ValueMember = "MaLich";
                    cbLichChieu.SelectedIndex = -1;
                }
            }
        }

        private void cbLichChieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbLichChieu.SelectedIndex == -1)
                return;

            if (cbLichChieu.SelectedItem is tbLichChieu lc)
            {
                txtMaPhong.Text = lc.MaPhong.ToString();
                if (lc.tbPhongChieu != null)
                {
                    txtLoaiPhong.Text = lc.tbPhongChieu.LoaiPhong;
                    int tongGhe = (int)lc.tbPhongChieu.SoGhe;
                    VeSoDoGhe(tongGhe);
                }
                txtGiaVe.Text = lc.GiaVe.Value.ToString("N0");
                txtTongTien.Text = "0";//reset tổng tiền
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (cbLichChieu.SelectedValue == null)
                return;

            int maLich = (int)cbLichChieu.SelectedValue;
            int soGheDaChon = 0;
            string DSGhe = "";

            using (var db = new CinemaManagementDBEntities())
            {
                foreach (Control ghe in flpGhe.Controls)
                {
                    if (ghe is Button btn && btn.BackColor == Color.Yellow)
                    {
                        soGheDaChon++;
                        DSGhe += btn.Text + ", ";
                        tbVeXemPhim ve = new tbVeXemPhim();
                        ve.MaLich = maLich;
                        ve.ViTriGhe = btn.Text; // Lưu số ghế
                        ve.NgayBan = DateTime.Now;

                        decimal GiaVe = decimal.Parse(txtGiaVe.Text.Replace(".", ""));
                        ve.TongTien = GiaVe;
                        db.tbVeXemPhims.Add(ve);
                    }
                }

                if (soGheDaChon == 0)
                    {
                    MessageBox.Show("Vui lòng chọn ít nhất một ghế trước khi thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                else if (soGheDaChon > 0)
                    {
                        DSGhe = DSGhe.TrimEnd(',', ' '); //bỏ dấu , thừa
                        string thongBao = string.Format("Đã chọn ghế số: {0}\nTổng tiền: {1} VND", DSGhe, txtTongTien.Text);
                        DialogResult dr = MessageBox.Show(thongBao, "Xác nhận thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            db.SaveChanges(); //lưu vào SQL
                            foreach (Control ghe in flpGhe.Controls)
                            {
                                if (ghe is Button btn && btn.BackColor == Color.Yellow)
                                {
                                    btn.BackColor = Color.Red;
                                    btn.Enabled = false;
                                }
                            }
                        MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        txtTongTien.Text = "0";
                    }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (cbLichChieu.SelectedItem is tbLichChieu lc)
            {
                VeSoDoGhe(lc.tbPhongChieu.SoGhe);
                txtTongTien.Text = "0";
            }
        }
    }
}
