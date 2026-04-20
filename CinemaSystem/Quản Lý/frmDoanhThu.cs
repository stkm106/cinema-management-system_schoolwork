using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace CinemaSystem
{
    public partial class frmDoanhThu : Form
    {
        public frmDoanhThu()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            using (var db = new CinemaManagementDBEntities())
            {
                //liên kết bảng Vé với Lịch Chiếu
                var thongKe = db.tbVeXemPhims
                    .Where(v => v.NgayBan >= tuNgay && v.NgayBan <= denNgay)
                    .GroupBy(v => v.tbLichChieu.tbPhim.TenPhim) //theo tên phim
                    .Select(g => new
                    {
                        TenPhim = g.Key,
                        SoVeBan = g.Count(),
                        DoanhThu = g.Sum(v => v.TongTien)
                    })
                    .OrderByDescending(x => x.DoanhThu) //phim hot nhất lên đầu
                    .ToList();

                dgvDoanhThu.DataSource = thongKe;

                dgvDoanhThu.Columns["TenPhim"].HeaderText = "Tên Phim";
                dgvDoanhThu.Columns["SoVeBan"].HeaderText = "Số Vé Đã Bán";
                dgvDoanhThu.Columns["DoanhThu"].HeaderText = "Tổng Tiền (VND)";
                dgvDoanhThu.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";

                //tổng tất cả
                decimal tong = thongKe.Sum(x => x.DoanhThu ?? 0);
                lblTongDoanhThu.Text = string.Format("Tổng doanh thu: {0:N0} VND", tong);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Now;
            dtpDenNgay.Value = DateTime.Now;
            lblTongDoanhThu.Text = "Tổng doanh thu: ";
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvDoanhThu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                //kKhởi tạo Excel
                Excel.Application excelApp = new Excel.Application();
                excelApp.Application.Workbooks.Add(Type.Missing);

                for (int i = 1; i < dgvDoanhThu.Columns.Count + 1; i++)
                {
                    excelApp.Cells[1, i] = dgvDoanhThu.Columns[i - 1].HeaderText;
                    excelApp.Cells[1, i].Font.Bold = true;
                    excelApp.Cells[1, i].Interior.Color = Color.LightGray;
                }

                //đưa data từ dgv vào Excel
                for (int i = 0; i < dgvDoanhThu.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvDoanhThu.Columns.Count; j++)
                    {
                        excelApp.Cells[i + 2, j + 1] = dgvDoanhThu.Rows[i].Cells[j].Value.ToString();
                    }
                }

                int rowCuoi = dgvDoanhThu.Rows.Count + 3; //thêm tổng doanh thu
                excelApp.Cells[rowCuoi, 1] = "Tổng doanh thu:";
                excelApp.Cells[rowCuoi, 3] = lblTongDoanhThu.Text;
                excelApp.Cells[rowCuoi, 1].Font.Bold = true;

                excelApp.Columns.AutoFit();
                excelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
    }
}
