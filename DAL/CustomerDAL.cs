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
    }
}
