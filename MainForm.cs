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
        
        public MainForm()
        {
            InitializeComponent();
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
    }
}
