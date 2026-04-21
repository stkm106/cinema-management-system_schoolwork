using CinemaSystem.Quản_Lý;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CinemaSystem
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        public void MenuSetting(string UserType)
        {
            if (UserType == "Admin")
            {
                MenuQuanLy.Visible = true;
                MenuBanVe.Visible = true;
                MenuBaoCao.Visible = true;
            }
            else if (UserType == "NhanVien")
            {
                MenuQuanLy.Visible = true;
                MenuBanVe.Visible = true;
                MenuBaoCao.Visible = false;
            }
            else if(UserType == "QuanLy")
            {
                MenuQuanLy.Visible = false;
                MenuBanVe.Visible = false;
                MenuBaoCao.Visible = true;
            }
            else if (UserType == "KhachHang")
            {
                frmDashboard khach = new frmDashboard();
                this.Hide();
                khach.ShowDialog();
                this.Close();
            }
            else //nếu chưa đăng nhập
            {
                MenuQuanLy.Visible = false;
                MenuBanVe.Visible = false;
                MenuBaoCao.Visible = false;
                thôngTinTàiKhoảnToolStripMenuItem.Enabled = false;
                đăngXuấtToolStripMenuItem.Visible = false;
            }

            if(UserType != "None")
            {
                lblWelcome.Text = "Chào mừng trở lại, " + CurrentUser.HoTen + "!";
                lblWelcome.Visible = true;
                picWelcome.Visible = true;
                thôngTinTàiKhoảnToolStripMenuItem.Enabled = true;
                đăngXuấtToolStripMenuItem.Visible = true;
            }    
        }

        private void OpenMDIForm(Form fMDI)
        {
            foreach(Form frm in this.MdiChildren)
            {
                if(frm.Name == fMDI.Name)
                {
                    frm.Show();
                    return;
                }    
            }
            fMDI.Show();
            fMDI.BringToFront();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                //đăng nhập xong dr trả về đúng giá trị Role được chọn)
                MenuSetting(CurrentUser.Roles);
            }
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmTaiKhoan());
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            MenuSetting("None"); //vừa mở form lên là mặc định chưa có usertype
        }

        private void danhSáchPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmQLphim());
        }

        private void phòngChiếuPhimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmQLphong());
        }

        private void lịchChiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmQLlichchieu());
        }

        private void MenuBanVe_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmBanVe());
        }

        private void MenuBaoCao_Click(object sender, EventArgs e)
        {
            OpenMDIForm(new frmDoanhThu());
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                CurrentUser.HoTen = "";
                CurrentUser.Roles = "";
                MenuSetting("None");

                lblWelcome.Visible = false;
                picWelcome.Visible = false;
            }
        }
    }
}
