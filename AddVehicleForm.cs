using BLL;
using DAL;
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
    public partial class AddVehicleForm : Form
    {
        public AddVehicleForm()
        {
            InitializeComponent();
        }

        bool isVisitor;
        private void MonthTicket_btn_Click(object sender, EventArgs e)
        {
            CustomerType_panel.Visible = false;
            CustomerType_panel.Enabled = false;
            isVisitor = false;
            FormInformPanel.Visible = true;
        }

        private void Visitor_btn_Click(object sender, EventArgs e)
        {
            CustomerType_panel.Visible = false;
            CustomerType_panel.Enabled = false;
            isVisitor = true;
            FormInformPanel.Visible = true;
            NameCus_txt.Visible = false;
            NumCus_txt.Visible = false;
            NameLabel.Visible = false;
            Num_label.Visible = false;
            Upimg_btn.Visible = false;
            
        }
        private void submit_btn_Click(object sender, EventArgs e)
        {
            using (var db = new DataAccessEntity())
            {
                var selectedLot = db.ParkingLots.FirstOrDefault(l => l.TenBaiXe.ToLower() == PLotcbB.SelectedItem.ToString().ToLower());
                if (selectedLot == null)
                {
                    MessageBox.Show("Không tìm thấy bãi xe");
                    return;
                }

                // Tạo đối tượng Vehicle
                Vehicle vehicle = new Vehicle();
                vehicle.LicensePlate = License_txt.Text;
                vehicle.VehicleType = CartypecbB.Items[CartypecbB.SelectedIndex].ToString();
                if (TicketTypecbB.SelectedItem.ToString() == "Vé tháng")
                {
                    vehicle.ID_Ve = 1;
                }
                else if (TicketTypecbB.SelectedItem.ToString() == "Vé ngày")
                {
                    vehicle.ID_Ve = 2;
                }

                // Tạo đối tượng ParkingSpace
                ParkingSpace ps = new ParkingSpace();
                ps.ParkingSpaceID = Convert.ToInt32(ParkingspaceID_txt.Text);

                // Xử lý khách hàng
                KHACHHANG kh;
                if (!isVisitor)
                {
                    // Nếu là khách vãng lai thì nhập thông tin khách
                    kh = new KHACHHANG
                    {
                        Name_Customer = NameCus_txt.Text,
                        Phone = NumCus_txt.Text
                    };
                } else
                {
                    string khVl = "Khách hàng vãng lai";
                    kh = new KHACHHANG
                    {
                        Name_Customer = khVl
                    };
                }

                // Tạo đối tượng ParkingLot
                ParkingLot pl = new ParkingLot();
                pl.ParkingLotID = selectedLot.ParkingLotID;
                pl.TenBaiXe = selectedLot.TenBaiXe;

                // Gọi phương thức thêm xe vào bãi đỗ
                ParkingSpaceBLL psBll = new ParkingSpaceBLL();
                bool result = psBll.AddVehicleToParkingSpace(vehicle, kh, ps, pl);

                if (result)
                {
                    MessageBox.Show("Thêm xe thành công!");
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm xe.");
                }
            }
        }


        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
