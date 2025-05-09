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
using DAL;

namespace PBL3_CARPARKINGMANAGEMENT
{
    public partial class EmployeeUC : UserControl
    {
        public EmployeeUC()
        {
            InitializeComponent();
            LoadData();

        }

        private void Employeedgv_SelectionChanged(object sender, EventArgs e)
        {
            DataBinding();
        }

        private void LoadData()
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();
            List<Employee> employees = employeeBLL.GetAllEmployees();
            Employeedgv.DataSource = employees;
        }
        private void DataBinding()
        {
            if (Employeedgv.SelectedRows.Count > 0)
            {
                DataGridViewRow row = Employeedgv.SelectedRows[0];
                ID_txt.Text = row.Cells[0].Value.ToString();
                Name_txt.Text = row.Cells[1].Value.ToString();
                Phonenum_txt.Text = row.Cells[2].Value.ToString();
                Pos_txt.Text = row.Cells[3].Value.ToString();
                Salary_txt.Text = row.Cells[4].Value.ToString();
                ParkingID_txt.Text = row.Cells[5].Value.ToString();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddEmployeeForm f = new AddEmployeeForm();
            f.ShowDialog();
            LoadData();
        }
        private void Search_btn_Click(object sender, EventArgs e)
        {
            string searchText = Search_txt.Text.ToLower();
            EmployeeBLL employeeBLL = new EmployeeBLL();
            List<Employee> employees = employeeBLL.GetAllEmployees();
            var filteredEmployees = employees.Where(em => em.Name.ToLower().Contains(searchText)).ToList();
            Employeedgv.DataSource = filteredEmployees;
        }
        private void Employeedgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = Employeedgv.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Nút Sửa
                if (Employeedgv.Columns[e.ColumnIndex].Name == "clbtnEdit")
                {
                    Employee employee = new Employee
                    {
                        EmployeeID = Convert.ToInt32(ID_txt.Text),
                        Name = Name_txt.Text,
                        PhoneNumber = Phonenum_txt.Text,
                        Position = Pos_txt.Text,
                        Salary = Convert.ToDecimal(Salary_txt.Text),
                        ParkingLotID = Convert.ToInt32(ParkingID_txt.Text)
                    };

                    EmployeeBLL employeeBLL = new EmployeeBLL();
                    employeeBLL.UpdateEmployee(employee);
                    LoadData();
                }

                // Nút Xoá
                if (Employeedgv.Columns[e.ColumnIndex].Name == "clbtnDel")
                {
                    EmployeeBLL bll = new EmployeeBLL();
                    DialogResult result = MessageBox.Show("Bạn có chắc muốn xoá khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (bll.DeleteEmployee(Convert.ToInt32(id)))
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
