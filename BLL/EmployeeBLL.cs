using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class EmployeeBLL
    {
        EmployeeDAL employeeDAL = new EmployeeDAL();
        public List<Employee> GetAllEmployees()
        {
            return employeeDAL.GetAllEmployees();
        }
        public void AddEmployee(Employee employee)
        {
            employeeDAL.AddEmployee(employee);
        }
        public bool DeleteEmployee(int employeeID)
        {
            if (employeeDAL.DeleteEmployee(employeeID))
            {
                return true;
            };
            return false;
        }
        public void UpdateEmployee(Employee employee)
        {
            employeeDAL.UpdateEmployee(employee);
        }
        public List<Employee> SearchEmployee(string searchText)
        {
            return employeeDAL.SearchEmployee(searchText);
        }
    }
}
