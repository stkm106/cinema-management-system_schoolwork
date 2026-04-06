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
    public partial class frmTaiKhoan : Form
    {
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            txtHoTen.Text = CurrentUser.HoTen;
            txtSDT.Text = CurrentUser.SDT;
            txtEmail.Text = CurrentUser.Email;
            txtUser.Text = CurrentUser.TaiKhoan;
            txtPwd.Text = CurrentUser.MatKhau;
            txtRole.Text = CurrentUser.Roles;
            //dữ liệu được bên db đã được tải lên từ login nên chỉ cần gọi chứ không cần khai báo lại
        }
    }
}
