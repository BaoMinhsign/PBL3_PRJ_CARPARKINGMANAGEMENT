using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDAL
    {
        DataAccessEntity db = new DataAccessEntity();
        public List<KHACHHANG> GetAllCustomers()
        {
            List<KHACHHANG> customerData = new List<KHACHHANG>();
            var tb = from customer in db.KHACHHANGs
                     select customer;
            foreach (var item in tb)
            {
                KHACHHANG customer = new KHACHHANG();
                customer.ID_Khach = item.ID_Khach;
                customer.Name_Customer = item.Name_Customer;
                customer.Phone = item.Phone;
                customer.Vehicles = item.Vehicles;
                customer.IsLoyalty = item.IsLoyalty;
                customer.DiscountPercentage = item.DiscountPercentage;
                customer.DiscountStartDate = item.DiscountStartDate;
                customer.DiscountEndDate = item.DiscountEndDate;
                customerData.Add(customer);
            }
            return customerData;
        }
        public void AddCustomer(KHACHHANG cus)
        {
            db.KHACHHANGs.Add(cus);
            db.SaveChanges();
        }
        public KHACHHANG GetCustomerById(int id)
        {
            return db.KHACHHANGs.FirstOrDefault(c => c.ID_Khach == id);
        }

        public bool CustomerHasVehicle(int id)
        {
            return db.Vehicles.Any(v => v.ID_Khach == id);
        }

        public bool DeleteCustomer(KHACHHANG customer)
        {
            try
            {
                db.KHACHHANGs.Remove(customer);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateCustomer(KHACHHANG updatedCustomer)
        {
            try
            {
                var existing = db.KHACHHANGs.FirstOrDefault(c => c.ID_Khach == updatedCustomer.ID_Khach);
                if (existing != null)
                {
                    existing.Name_Customer = updatedCustomer.Name_Customer;
                    existing.Phone = updatedCustomer.Phone;
                    existing.IsLoyalty = updatedCustomer.IsLoyalty;
                    existing.DiscountPercentage = updatedCustomer.DiscountPercentage;
                    existing.DiscountStartDate = updatedCustomer.DiscountStartDate;
                    existing.DiscountEndDate = updatedCustomer.DiscountEndDate;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
