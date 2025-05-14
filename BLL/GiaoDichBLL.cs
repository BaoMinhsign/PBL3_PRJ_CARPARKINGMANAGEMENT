using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class GiaoDichBLL
    {
        private GiaoDichDAL _dal;

        public GiaoDichBLL()
        {
            _dal = new GiaoDichDAL();
        }

        public List<TransactionDTO> SearchTransactions(string keyword, DateTime fromDate, DateTime toDate)
        {
            // Business logic validations can be added here if needed
            if (fromDate > toDate)
            {
                throw new ArgumentException("Start date cannot be after end date");
            }

            return _dal.GetTransactions(keyword, fromDate, toDate);
        }
        public List<TransactionDTO> SearchTransaction(string keyword, DateTime fromDate, DateTime toDate)
        {
            // Business logic validations can be added here if needed
            if (fromDate > toDate)
            {
                throw new ArgumentException("Start date cannot be after end date");
            }
            return _dal.SearchTransaction(keyword, fromDate, toDate);
        }
    }
}
