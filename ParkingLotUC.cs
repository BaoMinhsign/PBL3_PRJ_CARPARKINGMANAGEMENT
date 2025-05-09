using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class ParkingLotUC : UserControl
    {
        public ParkingLotUC()
        {
            InitializeComponent();
            LoadCBBox();
        }

        private void LoadCBBox()
        {
            ParkingLotBLL bll = new ParkingLotBLL();
            List<string> parkingLotNames = bll.getParkingLot();
            foreach (var name in parkingLotNames)
            {
                guna2ComboBox1.Items.Add(name);
            }

            guna2ComboBox1.SelectedIndex = 0;
        }
        private void LoadData(string selectedLotName)
        {
            ParkingSpaceBLL spaceBLL = new ParkingSpaceBLL();
            List<ParkingSpaceDetail> spaceList = spaceBLL.GetParkingSpaceInfos(selectedLotName);

            dataGridView1.DataSource = spaceList;
        }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedLotName = guna2ComboBox1.SelectedItem.ToString();

            LoadData(selectedLotName);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddVehicleForm f = new AddVehicleForm();
            f.Show();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin chỗ đỗ từ hàng được chọn
                string parkingSpaceId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                // Xác nhận xóa
                DialogResult result = MessageBox.Show("Are you sure you want to delete this vehicle from the parking space?",
                                                      "Confirmation",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    //try
                    //{
                        // Gọi BLL để xóa phương tiện
                        ParkingSpaceBLL spaceBLL = new ParkingSpaceBLL();
                        bool isDeleted = spaceBLL.ExitVehicleFromParkingSpace(Convert.ToInt32(parkingSpaceId));

                        if (isDeleted)
                        {
                            MessageBox.Show("Vehicle removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Tải lại dữ liệu
                            string selectedLotName = guna2ComboBox1.SelectedItem.ToString();
                            LoadData(selectedLotName);
                        }
                        else
                        {
                            MessageBox.Show("Failed to remove the vehicle. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
            }
            else
            {
                MessageBox.Show("Please select a parking space to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Parking_StatusForm f = new Parking_StatusForm();
            f.ShowDialog();
        }
    }
}
