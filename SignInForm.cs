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
    public partial class SignInForm: Form
    {
        public SignInForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUpForm f = new SignUpForm();
            f.Show();
        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtPassword.Text) || string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            BLL.UserLoginBLL userLoginBLL = new BLL.UserLoginBLL();
            // Thay đổi ở đây: Nhận đối tượng USER
            DAL.USER loggedInUser = userLoginBLL.CheckLogin(username, password);

            if (loggedInUser != null) // Kiểm tra xem user có tồn tại không
            {
                this.Hide(); // Ẩn form đăng nhập thay vì đóng
                MessageBox.Show("Login successful!");
                // Truyền thông tin người dùng vào MainForm
                MainForm mainForm = new MainForm(loggedInUser);
                mainForm.FormClosed += (s, args) => this.Close(); // Đóng SignInForm khi MainForm đóng
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                // Không cần return ở đây nữa vì if-else đã xử lý
            }

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void checkboxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';

            }
        }
    }
}
