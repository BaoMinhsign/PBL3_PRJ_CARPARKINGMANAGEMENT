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
            if (userLoginBLL.CheckLogin(username, password))
            {   this.Close();   
                MessageBox.Show("Login successful!");
                MainForm mainForm = new MainForm();
                mainForm.Show();

            }
            else
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }

        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
