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
                ID_txt.Text = row.Cells["ID_Khach"].Value.ToString();
                Name_txt.Text = row.Cells["Name_Customer"].Value.ToString();
                Phone_txt.Text = row.Cells["Phone"].Value.ToString();
            }
        }

        private void CustomerDGV_SelectionChanged(object sender, EventArgs e)
        {
            DataBinding();
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            //string searchText = Search_txt.Text.ToLower();
            //var bll = new CustomerBLL();
            //var list = bll.SearchCustomerByName(searchText);
            //CustomerDgv.DataSource = list;
        }

        private void Add_btn_Click(object sender, EventArgs e)
        {
            //var f = new AddCustomerForm(); 
            //f.ShowDialog();
            //LoadData();
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        //    if (e.RowIndex >= 0)
        //    {
        //        string id = CustomerDgv.Rows[e.RowIndex].Cells["ID_Khach"].Value.ToString();

        //        if (CustomerDgv.Columns[e.ColumnIndex].Name == "clbtnEdit")
        //        {
        //            KHACHHANG kh = new KHACHHANG
        //            {
        //                ID_Khach = Convert.ToInt32(ID_txt.Text),
        //                Name_Customer = Name_txt.Text,
        //                Phone = Phone_txt.Text
        //            };
        //            var form = new AddCustomerForm(kh); // Form sửa
        //            form.ShowDialog();
        //            LoadData();
        //        }
        //        else if (CustomerDgv.Columns[e.ColumnIndex].Name == "clbtnDel")
        //        {
        //            DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo);
        //            if (result == DialogResult.Yes)
        //            {
        //                var bll = new CustomerBLL();
        //                if (bll.DeleteCustomer(Convert.ToInt32(id)))
        //                {
        //                    MessageBox.Show("Xoá thành công!");
        //                    LoadData();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Xoá thất bại!");
        //                }
        //            }
        //        }
        //    }
        }
    }
}
