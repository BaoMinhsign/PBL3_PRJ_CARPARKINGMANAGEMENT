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
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }
        private void Yes_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDiscountFields(Yes_rbtn.Checked);
        }

        private void No_rbtn_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDiscountFields(Yes_rbtn.Checked);
        }
        private void ToggleDiscountFields(bool isVisible)
        {
            Discount_txt.Enabled = isVisible;
            Start_txt.Enabled = isVisible;
            End_txt.Enabled = isVisible;

            Discount_txt.Visible = isVisible;
            Start_txt.Visible = isVisible;
            End_txt.Visible = isVisible;

            Discount_lbl.Visible = isVisible;
            Start_lbl.Visible = isVisible;
            End_lbl.Visible = isVisible;
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            bool isLoyalty = Yes_rbtn.Checked;
            decimal discountp = 0;
            DateTime? start = null;
            DateTime? end = null;

            try
            {
                if (string.IsNullOrEmpty(Name_txt.Text) || string.IsNullOrEmpty(Phone_txt.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tên và số điện thoại");
                    return;
                }

                if (!long.TryParse(Phone_txt.Text, out long phoneNumber) || Phone_txt.Text.Length < 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    return;
                }

                if (isLoyalty)
                {
                    if (!decimal.TryParse(Discount_txt.Text, out decimal discount) || discount < 0 || discount > 100)
                    {
                        MessageBox.Show("Discount không hợp lệ (0 - 100%)");
                        return;
                    }

                    if (!DateTime.TryParse(Start_txt.Text, out DateTime startDate) || !DateTime.TryParse(End_txt.Text, out DateTime endDate))
                    {
                        MessageBox.Show("Ngày giảm giá không hợp lệ");
                        return;
                    }

                    discountp = discount;
                    start = startDate;
                    end = endDate;
                }

                KHACHHANG cus = new KHACHHANG
                {
                    Name_Customer = Name_txt.Text,
                    Phone = Phone_txt.Text,
                    IsLoyalty = isLoyalty,
                    DiscountPercentage = isLoyalty ? discountp : (decimal?)null,
                    DiscountStartDate = isLoyalty ? start : null,
                    DiscountEndDate = isLoyalty ? end : null
                };

                CustomerBLL customerBLL = new CustomerBLL();
                customerBLL.AddCustomer(cus);
                MessageBox.Show("Thêm khách hàng thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                Exception inner = ex;
                while (inner.InnerException != null)
                {
                    inner = inner.InnerException;
                }
                MessageBox.Show("Lỗi chi tiết: " + inner.Message);
            }

        }


        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
