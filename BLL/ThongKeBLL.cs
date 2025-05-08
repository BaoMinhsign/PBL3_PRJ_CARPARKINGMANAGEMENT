using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data.Entity;

namespace BLL
{
    public class ThongKeBLL
    {
        // Lấy tổng số khách hàng
        public int GetTotalCustomers()
        {
            using (var db = new DataAccessEntity())
            {
                return db.KHACHHANGs.Count();
            }
        }

        // Lấy tổng số phương tiện
        public int GetTotalVehicles()
        {
            using (var db = new DataAccessEntity())
            {
                return db.Vehicles.Count();
            }
        }

        // Lấy số phương tiện đang đỗ
        public int GetVehiclesCurrentlyParked()
        {
            using (var db = new DataAccessEntity())
            {
                return db.ParkingSpaces.Count(ps => ps.Status == "Có xe");
            }
        }

        // Lấy số chỗ còn trống
        public int GetAvailableParkingSpaces()
        {
            using (var db = new DataAccessEntity())
            {
                return db.ParkingSpaces.Count(ps => ps.Status == "Trống");
            }
        }

        // Lấy tổng số giao dịch
        public int GetTotalTransactions()
        {
            using (var db = new DataAccessEntity())
            {
                return db.TRANSACTION_LOG.Count();
            }
        }

        // Lấy tổng doanh thu
        public decimal GetTotalRevenue()
        {
            using (var db = new DataAccessEntity())
            {
                // Không sử dụng HasValue và Value vì Amount có thể là kiểu decimal thông thường
                return db.Payments.Any() ? db.Payments.Sum(p => p.Amount) : 0;
            }
        }

        // Lấy dữ liệu doanh thu theo tháng để vẽ biểu đồ
        public List<RevenueByMonthDTO> GetRevenueByMonth()
        {
            using (var db = new DataAccessEntity())
            {
                var revenueByMonth = db.Payments
                    .GroupBy(p => new { Month = p.PaymentDate.Month, Year = p.PaymentDate.Year })
                    .Select(g => new RevenueByMonthDTO
                    {
                        Month = g.Key.Month,
                        Year = g.Key.Year,
                        Total = g.Sum(p => p.Amount)
                    })
                    .OrderBy(x => x.Year)
                    .ThenBy(x => x.Month)
                    .ToList();

                return revenueByMonth;
            }
        }

        // Lấy số lượng phương tiện theo loại
        public List<VehicleTypeDTO> GetVehicleTypeDistribution()
        {
            using (var db = new DataAccessEntity())
            {
                var vehicleTypes = db.Vehicles
                    .GroupBy(v => v.VehicleType)
                    .Select(g => new VehicleTypeDTO
                    {
                        Type = g.Key,
                        Count = g.Count()
                    })
                    .ToList();

                return vehicleTypes;
            }
        }

        // Lấy thông tin chi tiết về trạng thái của các bãi đỗ xe
        public List<ParkingLotStatusDTO> GetParkingLotStatus()
        {
            using (var db = new DataAccessEntity())
            {
                var parkingDetails = db.ParkingLots
                    .Select(pl => new ParkingLotStatusDTO
                    {
                        ParkingLotID = pl.ParkingLotID,
                        Name = pl.TenBaiXe,
                        Capacity = pl.Capacity,
                        UsedSpaces = pl.ParkingSpaces.Count(ps => ps.Status == "Có xe"),
                        AvailableSpaces = pl.ParkingSpaces.Count(ps => ps.Status == "Trống")
                    })
                    .ToList();

                return parkingDetails;
            }
        }

        // Lấy tất cả dữ liệu thống kê cho dashboard trong một lần truy vấn
        public DashboardStatsDTO GetDashboardStats()
        {
            using (var db = new DataAccessEntity())
            {
                var stats = new DashboardStatsDTO
                {
                    TotalCustomers = db.KHACHHANGs.Count(),
                    TotalVehicles = db.Vehicles.Count(),
                    VehiclesCurrentlyParked = db.ParkingSpaces.Count(ps => ps.Status == "Có xe"),
                    AvailableParkingSpaces = db.ParkingSpaces.Count(ps => ps.Status == "Trống"),
                    TotalTransactions = db.TRANSACTION_LOG.Count(),
                    TotalRevenue = db.Payments.Any() ? db.Payments.Sum(p => p.Amount) : 0,
                    ParkingLotStatus = db.ParkingLots
                        .Select(pl => new ParkingLotStatusDTO
                        {
                            ParkingLotID = pl.ParkingLotID,
                            Name = pl.TenBaiXe,
                            Capacity = pl.Capacity,
                            UsedSpaces = pl.ParkingSpaces.Count(ps => ps.Status == "Có xe"),
                            AvailableSpaces = pl.ParkingSpaces.Count(ps => ps.Status == "Trống")
                        })
                        .ToList()
                };

                return stats;
            }
        }
    }

    // Class DTO cho doanh thu theo tháng
    public class RevenueByMonthDTO
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal Total { get; set; }
        public string MonthName
        {
            get { return new DateTime(Year, Month, 1).ToString("MM/yyyy"); }
        }
    }

    // Class DTO cho phân bố loại phương tiện
    public class VehicleTypeDTO
    {
        public string Type { get; set; }
        public int Count { get; set; }
    }

    // Class DTO cho trạng thái bãi đỗ xe
    public class ParkingLotStatusDTO
    {
        public int ParkingLotID { get; set; }
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public int UsedSpaces { get; set; }
        public int AvailableSpaces { get; set; }
        public decimal OccupancyRate
        {
            get
            {
                if (Capacity.HasValue && Capacity.Value > 0)
                    return Math.Round(((decimal)UsedSpaces / Capacity.Value) * 100, 2);
                return 0;
            }
        }
    }

    // Class DTO cho dữ liệu thống kê tổng hợp cho dashboard
    public class DashboardStatsDTO
    {
        public int TotalCustomers { get; set; }
        public int TotalVehicles { get; set; }
        public int VehiclesCurrentlyParked { get; set; }
        public int AvailableParkingSpaces { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<ParkingLotStatusDTO> ParkingLotStatus { get; set; }
    }
}