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
        private void LoadData()
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();
            List<Employee> employees = employeeBLL.GetAllEmployees();
            Employeedgv.DataSource = employees;
        }

    }
}
