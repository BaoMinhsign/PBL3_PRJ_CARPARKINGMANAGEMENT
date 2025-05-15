using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL;
using DAL;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class CustomerUC : UserControl
    {
        public CustomerUC()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var customerBLL = new CustomerBLL();
            var list = customerBLL.GetAllCustomers();
            CustomerDgv.DataSource = list;
        }

        private void DataBinding()
        {
            if (CustomerDgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = CustomerDgv.SelectedRows[0];
                ID_txt.Text = row.Cells[0].Value.ToString();
                Name_txt.Text = row.Cells[2].Value.ToString();
                Phone_txt.Text = row.Cells[1].Value.ToString();
                Discount_txt.Text = row.Cells[4]?.Value?.ToString() ?? "";

                var startValue = row.Cells[5]?.Value;
                var endValue = row.Cells[6]?.Value;

                start_txt.Text = startValue != null ? Convert.ToDateTime(startValue).ToString("yyyy-MM-dd") : "";
                end_txt.Text = endValue != null ? Convert.ToDateTime(endValue).ToString("yyyy-MM-dd") : "";
            }
        }

        private void CustomerDgv_SelectionChanged(object sender, EventArgs e)
        {
            DataBinding();
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            string searchText = Search_txt.Text.ToLower();
            var bll = new CustomerBLL();
            var list = bll.SearchCustomer(searchText);
            CustomerDgv.DataSource = list;
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            var f = new AddCustomerForm();
            f.ShowDialog();
            LoadData();
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = CustomerDgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (CustomerDgv.Columns[e.ColumnIndex].Name == "clbtnEdit")
                {
                    try
                    {
                        decimal discount = 0;
                        bool isLoyalty = !string.IsNullOrEmpty(Discount_txt.Text) &&
                        decimal.TryParse(Discount_txt.Text, out discount) &&
                        discount > 0;

                        KHACHHANG kh = new KHACHHANG
                        {
                            ID_Khach = Convert.ToInt32(ID_txt.Text),
                            Name_Customer = Name_txt.Text,
                            Phone = Phone_txt.Text,
                            IsLoyalty = isLoyalty,
                            DiscountPercentage = isLoyalty ? discount : (decimal?)null,
                            DiscountStartDate = isLoyalty ? Convert.ToDateTime(start_txt.Text) : (DateTime?)null,
                            DiscountEndDate = isLoyalty ? Convert.ToDateTime(end_txt.Text) : (DateTime?)null,
                        };
                        CustomerBLL customerBLL = new CustomerBLL();
                        customerBLL.UpdateCustomer(kh);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    LoadData();
                }
                else if (CustomerDgv.Columns[e.ColumnIndex].Name == "clbtnDel")
                {
                    DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        var bll = new CustomerBLL();
                        if (bll.DeleteCustomer(Convert.ToInt32(id)))
                        {
                            MessageBox.Show("Xoá thành công!");
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Xoá thất bại!");
                        }
                    }
                }
            }

        }
    }
}
