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
    }
}
