using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerBLL
    {
        CustomerDAL customerDAL = new CustomerDAL();
        public List<KHACHHANG> GetAllCustomers()
        {
            return customerDAL.GetAllCustomers();
        }
        public void AddCustomer(KHACHHANG cus)
        {
            customerDAL.AddCustomer(cus);
        }
        public List<KHACHHANG> SearchCustomer(string name)
        {
            using (var db = new DataAccessEntity())
            {
                return db.KHACHHANGs
                         .Where(c => c.Name_Customer.ToLower().Contains(name))
                         .ToList();
            }
        }
        public bool DeleteCustomer(int id)
        {
            var customer = customerDAL.GetCustomerById(id);
            if (customer == null)
                return false;
            // Kiểm tra nếu khách có liên kết với xe thì không xóa
            if (customerDAL.CustomerHasVehicle(id))
                return false;

            return customerDAL.DeleteCustomer(customer);
        }
        public bool UpdateCustomer(KHACHHANG customer)
        {
            return customerDAL.UpdateCustomer(customer);
        }
    }
}
