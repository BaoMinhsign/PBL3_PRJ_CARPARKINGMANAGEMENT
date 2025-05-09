using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TransactionDTO
    {
        public int TransactionID { get; set; }
        public string LicensePlate { get; set; }
        public string CustomerName { get; set; }
        public string TicketType { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class GiaoDichDAL
    {
        public List<TransactionDTO> GetTransactions(string keyword, DateTime fromDate, DateTime toDate)
        {
            using (var context = new DataAccessEntity()) // Changed from CARPARKINGMANAGEMENTEntities to DataAccessEntity
            {
                var query = from t in context.TRANSACTION_LOG
                            join ps in context.ParkingSpaces on t.ParkingSpaceID equals ps.ParkingSpaceID
                            join v in context.Vehicles on ps.VehicleID equals v.VehicleID
                            join k in context.KHACHHANGs on v.ID_Khach equals k.ID_Khach
                            join l in context.LOAIVEs on v.ID_Ve equals l.ID_Ve
                            join p in context.Payments on t.TransactionID equals p.TransactionID into pay
                            from payment in pay.DefaultIfEmpty()
                            where
                                (string.IsNullOrEmpty(keyword) ||
                                 v.LicensePlate.Contains(keyword) ||
                                 k.Name_Customer.Contains(keyword))
                                && t.EntryTime >= fromDate && t.EntryTime <= toDate
                            orderby t.EntryTime descending
                            select new TransactionDTO
                            {
                                TransactionID = t.TransactionID,
                                LicensePlate = v.LicensePlate,
                                CustomerName = k.Name_Customer,
                                TicketType = l.TenVe,
                                EntryTime = t.EntryTime ?? DateTime.MinValue,
                                ExitTime = t.ExitTime ?? DateTime.MinValue,
                                Amount = payment != null ? (decimal)payment.Amount : 0,
                                PaymentMethod = payment != null ? payment.PaymentMethod : ""
                            };

                return query.ToList();
            }
        }
    }
}
