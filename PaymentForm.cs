using DAL;
using BLL;
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
    public partial class PaymentForm: Form
    { 
        public int vID { get; set; }
        bool isQrCode;
        bool isCash;
        public PaymentForm(int VehicleID)
        {
            InitializeComponent();
            vID = VehicleID;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            ParkingSpaceBLL bll = new ParkingSpaceBLL();
            MessageBox.Show($"debug : {vID}");
            Vehicle v = bll.GetVehicleByID(vID);
            if (v == null)
            {
                MessageBox.Show("Không tìm thấy xe trong bãi đỗ");
                return;
            }
            if (isQrCode && v.ID_Ve == 2)
            {
                bll.payment(v, "Chuyển khoản");
            }
            else if (isCash && v.ID_Ve == 2)
            {
                bll.payment(v, "Tiền mặt");
            }
            else if (isQrCode && v.ID_Ve == 1)
            {
                bll.payment(v, "Chuyển khoản");
            }
            else if (isCash && v.ID_Ve == 1)
            {
                bll.payment(v, "Tiền mặt");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán");
                return;
            }
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PaymentMethodPanel.Visible = false;
            QRpanel.Visible = true;
            isQrCode = true;
            isCash = false;
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PaymentMethodPanel.Visible = false;
            SubmitPanel.Visible = true;
            isCash = true;
            isQrCode = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            SubmitPanel.Visible = true;
            QRpanel.Visible = false;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
