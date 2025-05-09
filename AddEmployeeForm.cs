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
    public partial class AddEmployeeForm: Form
    {
        public AddEmployeeForm()
        {
            InitializeComponent();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(NameE_txt.Text) || string.IsNullOrEmpty(NumE_txt.Text) || string.IsNullOrEmpty(Pos_cbB.Text) || string.IsNullOrEmpty(Salary_txt.Text) || string.IsNullOrEmpty(ParkID_cbB.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                    return;
                }
                else if (Pos_cbB.SelectedIndex == -1 || ParkID_cbB.SelectedIndex == -1)
                {
                    MessageBox.Show("Vui lòng chọn vị trí và bãi đỗ");
                    return;
                }
                else if (!decimal.TryParse(Salary_txt.Text, out decimal salary))
                {
                    MessageBox.Show("Lương không hợp lệ");
                    return;
                }
                else if (salary < 1000000)
                {
                    MessageBox.Show("Lương không Hợp lệ");
                    return;
                }
                else if (NumE_txt.Text.Length < 10)
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    return;
                }
                else if (!long.TryParse(NumE_txt.Text, out long phoneNumber))
                {
                    MessageBox.Show("Số điện thoại không hợp lệ");
                    return;
                }
                Employee employee = new Employee();
                employee.Name = NameE_txt.Text;
                employee.PhoneNumber = NumE_txt.Text;
                employee.Position = Pos_cbB.Items[Pos_cbB.SelectedIndex].ToString();
                employee.Salary = Convert.ToDecimal(Salary_txt.Text);
                employee.StartDate = DateTime.Now;
                employee.ParkingLotID = Convert.ToInt32(ParkID_cbB.Items[ParkID_cbB.SelectedIndex].ToString());
                EmployeeBLL employeeBLL = new EmployeeBLL();
                employeeBLL.AddEmployee(employee);
                MessageBox.Show("Thêm nhân viên thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            this.Close();
        }
    }
}
