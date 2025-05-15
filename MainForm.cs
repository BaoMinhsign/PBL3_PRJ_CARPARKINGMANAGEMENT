using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class MainForm: Form
    {
        // Store reference to ThongKeUC for easy access
        private ThongKeUC thongKeUCInstance;
        private DAL.USER currentUser; // Thêm trường để lưu thông tin người dùng hiện tại
        
        // Cập nhật constructor để nhận thông tin người dùng
        public MainForm(DAL.USER user)
        {
            InitializeComponent();
            currentUser = user; // Lưu thông tin người dùng
            ApplyRolesAndPermissions(); // Áp dụng phân quyền
        }
        
        // Phương thức để áp dụng phân quyền
        private void ApplyRolesAndPermissions()
        {
            // For debugging: Show that the function is called and what the role is.
            if (currentUser != null)
            {
                // Trim the role string to handle potential leading/trailing whitespace
                string userRole = currentUser.Role != null ? currentUser.Role.Trim() : string.Empty;
            
                // Determine if the user is a manager, case-insensitively
                bool isManager = string.Equals(userRole, "Quản lý", StringComparison.OrdinalIgnoreCase);
                // Determine if the user can view statistics (Manager or Employee)
                bool canViewStatistics = isManager || string.Equals(userRole, "Nhân viên", StringComparison.OrdinalIgnoreCase);

                System.Diagnostics.Debug.WriteLine($"ApplyRolesAndPermissions - User: {currentUser.Username}, Role: '{userRole}', IsManager: {isManager}, CanViewStatistics: {canViewStatistics}");

                // Set visibility and enabled state for Employee button (Managers only)
                Employee_btn.Visible = isManager;
                Employee_btn.Enabled = isManager;

                // Set visibility and enabled state for Analysis button (Managers and Employees)
                Analys_btn.Visible = canViewStatistics;
                Analys_btn.Enabled = canViewStatistics;

                // Debugging output for clarity
                if (isManager)
                {
                    System.Diagnostics.Debug.WriteLine($"Role '{userRole}' is Quản lý. Employee_btn: Visible/Enabled. Analys_btn: Visible/Enabled.");
                }
                else if (canViewStatistics) // This means role is "Nhân viên"
                {
                    System.Diagnostics.Debug.WriteLine($"Role '{userRole}' is Nhân viên. Employee_btn: Hidden/Disabled. Analys_btn: Visible/Enabled.");
                }
                else // Other roles, or no role
                {
                    System.Diagnostics.Debug.WriteLine($"Role '{userRole}' is neither Quản lý nor Nhân viên. Employee_btn: Hidden/Disabled. Analys_btn: Hidden/Disabled.");
                }
                
                // Log actual state after setting them to help diagnose if the settings are applied
                System.Diagnostics.Debug.WriteLine($"Post-setting: Employee_btn.Visible={Employee_btn.Visible}, Employee_btn.Enabled={Employee_btn.Enabled}");
                System.Diagnostics.Debug.WriteLine($"Post-setting: Analys_btn.Visible={Analys_btn.Visible}, Analys_btn.Enabled={Analys_btn.Enabled}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("ApplyRolesAndPermissions called, but currentUser is null.");
                // Xử lý trường hợp không có thông tin người dùng (ví dụ: đóng form hoặc hiển thị lỗi)
                MessageBox.Show("Không có thông tin người dùng để phân quyền.", "Lỗi Phân Quyền", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        
        public void LoadUC(UserControl uc)
        {
            try
            {
                PanelMain.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                PanelMain.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong LoadUC: {ex.Message}\n\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to refresh statistics if the panel is currently visible
        public void RefreshStatisticsIfVisible()
        {
            if (thongKeUCInstance != null && PanelMain.Controls.Contains(thongKeUCInstance))
            {
                thongKeUCInstance.RefreshAllStatistics();
            }
        }

        private void OverView_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new OverViewUC());
        }

        private void Employee_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new EmployeeUC());
            // Employee changes might affect statistics
            RefreshStatisticsIfVisible();
        }

        private void Customer_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new CustomerUC());
            RefreshStatisticsIfVisible();
        }

        private void Vehicle_btn_Click(object sender, EventArgs e)
        {
            // Vehicle UserControl would be loaded here
            // Vehicle changes affect statistics
            RefreshStatisticsIfVisible();
        }

        private void Parkinglot_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new ParkingLotUC());
            RefreshStatisticsIfVisible();
        }

        private void Analys_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Create ThongKeUC instance and save reference
                thongKeUCInstance = new ThongKeUC();
                
                // Load the control
                LoadUC(thongKeUCInstance);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Transaction_btn_Click(object sender, EventArgs e)
        {
            LoadUC(new GiaoDien());
            // Transaction UserControl would be loaded here
            // Transaction changes definitely affect statistics
            RefreshStatisticsIfVisible();
        }

        private void Logout_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            SignInForm f = new SignInForm();
            f.Show();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
