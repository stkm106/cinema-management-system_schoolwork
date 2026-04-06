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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //kiểm tra nếu đầu vào không nhập hoặc để trắng trơn
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                lblNotice.Visible = true;
                return;
            }
            using (var dbRoles = new CinemaManagementDBEntities()) //dùng variable thay vì gọi tên hai lần
            {
                string user = txtUser.Text.Trim(); //bỏ khoảng trắng thừa
                string pass = txtPass.Text.Trim();

                //lấy giá trị đầu tiên thấy trong danh sách
                var roles = dbRoles.tbNguoiDungs.FirstOrDefault(x => x.Username == user && x.Passwd == pass);
                if (roles != null)
                {
                    CurrentUser.HoTen = roles.HoTen;
                    CurrentUser.Roles = roles.Roles;
                    CurrentUser.HoTen = roles.HoTen;
                    CurrentUser.SDT = roles.SDT;
                    CurrentUser.Email = roles.Email;
                    CurrentUser.TaiKhoan = roles.Username;
                    CurrentUser.MatKhau = roles.Passwd;
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblNotice.Visible = true;
                }    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
