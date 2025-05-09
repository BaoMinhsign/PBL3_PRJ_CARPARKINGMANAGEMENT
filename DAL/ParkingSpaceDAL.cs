using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAL
{
    public class ParkingSpaceDAL
    {
        DataAccessEntity db = new DataAccessEntity();
        public List<ParkingSpaceDetail> GetParkingSpacesWithDetails(string parkingLotName)
        {
            
                var query = from ps in db.ParkingSpaces
                            join pl in db.ParkingLots on ps.ParkingLotID equals pl.ParkingLotID
                            join v in db.Vehicles on ps.VehicleID equals v.VehicleID into vehicleJoin
                            from v in vehicleJoin.DefaultIfEmpty()
                            where pl.TenBaiXe.ToLower() == parkingLotName.ToLower()
                            select new ParkingSpaceDetail
                            {
                                ID = ps.ParkingSpaceID,
                                ParkingLot = pl.TenBaiXe,
                                Status = ps.Status,
                                LicensePlate = v != null ? v.LicensePlate : "Trống" 
                            };

                return query.ToList();
        }
        public List<Vehicle> GetVehicles()
        {
            List<Vehicle> vehicledata = new List<Vehicle>();
            var tb = from vehicle in db.Vehicles
                     select vehicle;
            foreach ( var item in tb )
            {
               Vehicle v = new Vehicle();
                v.ID_Ve = item.ID_Ve;
                v.LicensePlate = item.LicensePlate;
                v.VehicleID = item.VehicleID;
                v.ID_Khach = item.ID_Khach;
                v.VehicleType = item.VehicleType;
            }
            return vehicledata;
        }
        public bool AddvehicleToParkingSpace(Vehicle v, KHACHHANG kh, ParkingSpace parking, ParkingLot slot)
        {
            using (var db = new DataAccessEntity())
            {
                var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parking.ParkingSpaceID);

                if (parkingSpace == null)
                {
                    MessageBox.Show($"Không tìm thấy bãi đỗ với ID: {parking.ParkingSpaceID}");
                    return false;
                } else if (slot.TenBaiXe == "Bãi Xe máy" && parkingSpace.ParkingSpaceID > 20 )
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                } else if (slot.TenBaiXe == "Bãi Xe đạp" && (parkingSpace.ParkingSpaceID < 41 || parkingSpace.ParkingSpaceID > 60))
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                }
                else if (slot.TenBaiXe == "Bãi Oto" && (parkingSpace.ParkingSpaceID < 21 || parkingSpace.ParkingSpaceID > 40))
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                }

                if (parkingSpace.Status == "Có xe")
                {
                    MessageBox.Show("Bãi đỗ này đang được sử dụng.");
                    return false;
                }

                // Nếu là khách vãng lai thì kh là null
                string customerId = null;

                if (kh != null)
                {
                    // Kiểm tra xem khách đã tồn tại chưa (dựa theo số điện thoại)
                    var existingCustomer = db.KHACHHANGs.FirstOrDefault(c => c.Phone == kh.Phone);

                    if (existingCustomer == null)
                    {
                        db.KHACHHANGs.Add(kh);
                        db.SaveChanges(); // để có ID_khach
                        customerId = kh.ID_Khach.ToString();
                    }
                    else
                    {
                        customerId = existingCustomer.ID_Khach.ToString();
                    }
                }
                // Tạo mới Vehicle
                Vehicle vehicle = new Vehicle
                {
                    LicensePlate = v.LicensePlate,
                    VehicleType = v.VehicleType,
                    ID_Khach = Convert.ToInt32(customerId),
                    ID_Ve = v.ID_Ve // nếu bạn có loại vé (ngày, tháng) gán ở đây
                };

                db.Vehicles.Add(vehicle);
                db.SaveChanges(); // để có VehicleID

                // Cập nhật chỗ đỗ
                parkingSpace.VehicleID = vehicle.VehicleID;
                parkingSpace.Status = "Có xe";
                parkingSpace.ParkingLotID = slot.ParkingLotID;

                db.SaveChanges();
                return true;
            }
        }
        public bool ExitVehicleFromParkingSpace(int parkingSpaceId)
        {
            using (var db = new DataAccessEntity())
            {
                // Tìm chỗ đỗ xe theo ID
                var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parkingSpaceId);

                // Kiểm tra nếu chỗ đỗ không tồn tại hoặc không có xe
                if (parkingSpace == null || parkingSpace.VehicleID == null)
                {
                    return false;
                }

                // Lưu log giao dịch (nếu cần)
                var log = new TRANSACTION_LOG
                {
                    ParkingSpaceID = parkingSpace.ParkingSpaceID,
                    ExitTime = DateTime.Now, // Thời gian xe rời đi
                    //VehicleID = parkingSpace.VehicleID // Lưu thông tin xe
                };
                db.TRANSACTION_LOG.Add(log);

                // Cập nhật trạng thái chỗ đỗ
                parkingSpace.VehicleID = null; // Xóa xe khỏi chỗ đỗ
                parkingSpace.Status = "Trống"; // Đặt trạng thái chỗ đỗ là "Trống"

                // Lưu thay đổi vào cơ sở dữ liệu
                db.SaveChanges();
                return true;
            }
        }



    }
}
