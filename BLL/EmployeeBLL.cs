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
        public void DeleteEmployee(int employeeID)
        {
            employeeDAL.DeleteEmployee(employeeID);
        }
        public void UpdateEmployee(Employee employee)
        {
            employeeDAL.UpdateEmployee(employee);
        }
    }
}
