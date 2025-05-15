using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class Login: Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void submit_btn_Click(object sender, EventArgs e)
        {
            string username = username_txt.Text;
            string password = password_txt.Text;
            BLL.UserLoginBLL userLoginBLL = new BLL.UserLoginBLL();
            // Thay đổi ở đây: Nhận đối tượng USER
            DAL.USER loggedInUser = userLoginBLL.CheckLogin(username, password);

            if (loggedInUser != null) // Kiểm tra xem user có tồn tại không
            {   
                this.Hide(); // Ẩn form đăng nhập thay vì đóng
                MessageBox.Show("Login successful!");
                // Truyền thông tin người dùng vào MainForm
                MainForm mainForm = new MainForm(loggedInUser);
                mainForm.FormClosed += (s, args) => this.Close(); // Đóng LoginForm khi MainForm đóng
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                // Không cần return ở đây nữa vì if-else đã xử lý
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
