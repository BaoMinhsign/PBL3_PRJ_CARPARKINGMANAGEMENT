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
    public partial class EmployeeUC: UserControl
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
    }
}
