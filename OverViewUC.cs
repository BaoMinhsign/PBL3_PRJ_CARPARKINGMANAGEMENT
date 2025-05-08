using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class OverViewUC: UserControl
    {
        private ThongKeBLL thongKeBLL;
        
        public OverViewUC()
        {
            InitializeComponent();
            thongKeBLL = new ThongKeBLL();
            
            // Wire up the button click event
            guna2Button1.Click += Guna2Button1_Click;
            
            // Load data when the control is loaded
            this.Load += OverViewUC_Load;
            
            // Apply color styles to the text boxes
            ApplyTextBoxStyles();
        }
        
        private void ApplyTextBoxStyles()
        {
            // Set text properties for all parking status text boxes
            SetTextBoxProperties(guna2TextBox1);
            SetTextBoxProperties(guna2TextBox2);
            SetTextBoxProperties(guna2TextBox3);
            SetTextBoxProperties(guna2TextBox4);
            SetTextBoxProperties(guna2TextBox5);
            SetTextBoxProperties(guna2TextBox6);
            
            // Style labels for better contrast
            guna2HtmlLabel1.ForeColor = Color.White;
            guna2HtmlLabel2.ForeColor = Color.White;
            guna2HtmlLabel3.ForeColor = Color.White;
            guna2HtmlLabel4.ForeColor = Color.White;
            guna2HtmlLabel5.ForeColor = Color.White;
            guna2HtmlLabel6.ForeColor = Color.White;
            guna2HtmlLabel7.ForeColor = Color.White;
        }
        
        private void SetTextBoxProperties(Guna.UI2.WinForms.Guna2TextBox textBox)
        {
            // Set common properties for all text boxes
            textBox.ForeColor = Color.FromArgb(34, 37, 58);  // Dark blue text color
            textBox.BackColor = Color.White;
            textBox.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            
            // Set placeholder text color
            textBox.PlaceholderForeColor = Color.FromArgb(125, 137, 149);
            
            // Set focus colors
            textBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox.FocusedState.ForeColor = Color.FromArgb(34, 37, 58);
        }
        
        private void OverViewUC_Load(object sender, EventArgs e)
        {
            LoadParkingStatusData();
            LoadOverviewData();
        }
        
        // Load data for the main parking status panel
        private void LoadParkingStatusData()
        {
            try
            {
                // Get total vehicles
                int totalVehicles = thongKeBLL.GetTotalVehicles();
                guna2TextBox1.Text = totalVehicles.ToString();
                
                // Get available parking spaces
                int availableSpaces = thongKeBLL.GetAvailableParkingSpaces();
                guna2TextBox2.Text = availableSpaces.ToString();
                
                // Get vehicles currently parked
                int parkedVehicles = thongKeBLL.GetVehiclesCurrentlyParked();
                guna2TextBox3.Text = parkedVehicles.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading parking status data: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Load data for the overview panels (customers, statistics, transactions)
        private void LoadOverviewData()
        {
            try
            {
                // Get total customers
                int totalCustomers = thongKeBLL.GetTotalCustomers();
                guna2TextBox4.Text = totalCustomers.ToString();
                
                // Get total revenue
                decimal totalRevenue = thongKeBLL.GetTotalRevenue();
                guna2TextBox5.Text = string.Format("{0:N0} VND", totalRevenue);
                
                // Get total transactions
                int totalTransactions = thongKeBLL.GetTotalTransactions();
                guna2TextBox6.Text = totalTransactions.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading overview data: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        // Handle the "Xem trạng thái bãi đỗ" button click
        private void Guna2Button1_Click(object sender, EventArgs e)
        {
            // Refresh the parking status data
            LoadParkingStatusData();
            MessageBox.Show("Đã cập nhật trạng thái bãi đỗ!", "Thông báo", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        // Public method to refresh all data from outside the control
        public void RefreshData()
        {
            LoadParkingStatusData();
            LoadOverviewData();
        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
