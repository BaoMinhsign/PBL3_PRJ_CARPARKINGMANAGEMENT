using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmployeeDAL
    {
        DataAccessEntity db = new DataAccessEntity();
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeData = new List<Employee>();
            var tb = from employee in db.Employees
                     select employee;
            foreach (var item in tb)
            {
                Employee employee = new Employee();
                employee.EmployeeID = item.EmployeeID;
                employee.Name = item.Name;
                employee.PhoneNumber= item.PhoneNumber;
                employee.Position = item.Position;
                employee.Salary = item.Salary;
                employee.StartDate = item.StartDate;
                employee.ParkingLotID = item.ParkingLotID;
                employeeData.Add(employee);
            }
            return employeeData;
        }
        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }
        public bool DeleteEmployee(int employeeID)
        {
            var user = db.USERs.Where(u => u.EmployeeID == employeeID).ToList();
            db.USERs.RemoveRange(user);
            var employee = db.Employees.Find(employeeID);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public void UpdateEmployee(Employee employee)
        {
            var existingEmployee = db.Employees.Find(employee.EmployeeID);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.PhoneNumber = employee.PhoneNumber;
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;
                existingEmployee.StartDate = employee.StartDate;
                existingEmployee.ParkingLotID = employee.ParkingLotID;
                db.SaveChanges();
            }
        }
        public List<Employee> SearchEmployee(string searchTerm)
        {
            var searchResults = from employee in db.Employees
                                where employee.Name.Contains(searchTerm) || employee.PhoneNumber.Contains(searchTerm) || employee.Position.Contains(searchTerm) || employee.ParkingLotID.ToString().Contains(searchTerm) || employee.Salary.ToString().Contains(searchTerm)
                                select employee;
            List<Employee> resultList = new List<Employee>();
            foreach (var item in searchResults)
            {
                Employee employee = new Employee();
                employee.EmployeeID = item.EmployeeID;
                employee.Name = item.Name;
                employee.PhoneNumber = item.PhoneNumber;
                employee.Position = item.Position;
                employee.Salary = item.Salary;
                employee.StartDate = item.StartDate;
                employee.ParkingLotID = item.ParkingLotID;
                resultList.Add(employee);
            }
            return resultList;
        }
    }
}
