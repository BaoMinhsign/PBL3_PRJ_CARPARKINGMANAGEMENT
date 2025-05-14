using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                                VehicleID = v != null ? v.VehicleID : 0,
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
        //public bool AddvehicleToParkingSpace(Vehicle v, KHACHHANG kh, ParkingSpace parking, ParkingLot slot)
        //{
        //    using (var db = new DataAccessEntity())
        //    {
        //        var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parking.ParkingSpaceID);

        //        if (parkingSpace == null)
        //        {
        //            MessageBox.Show($"Không tìm thấy bãi đỗ với ID: {parking.ParkingSpaceID}");
        //            return false;
        //        } else if (slot.TenBaiXe == "Bãi Xe máy" && parkingSpace.ParkingSpaceID > 20 )
        //        {
        //            MessageBox.Show("Vị trí bãi xe không hợp lệ");
        //            return false;
        //        } else if (slot.TenBaiXe == "Bãi Xe đạp" && (parkingSpace.ParkingSpaceID < 41 || parkingSpace.ParkingSpaceID > 60))
        //        {
        //            MessageBox.Show("Vị trí bãi xe không hợp lệ");
        //            return false;
        //        }
        //        else if (slot.TenBaiXe == "Bãi Oto" && (parkingSpace.ParkingSpaceID < 21 || parkingSpace.ParkingSpaceID > 40))
        //        {
        //            MessageBox.Show("Vị trí bãi xe không hợp lệ");
        //            return false;
        //        }

        //        if (parkingSpace.Status == "Có xe")
        //        {
        //            MessageBox.Show("Bãi đỗ này đang được sử dụng.");
        //            return false;
        //        }

        //        string customerId = null;

        //        if (kh != null)
        //        {
        //            // Kiểm tra xem khách đã tồn tại chưa (dựa theo số điện thoại)
        //            var existingCustomer = db.KHACHHANGs.FirstOrDefault(c => c.Phone == kh.Phone);

        //            if (existingCustomer == null)
        //            {
        //                db.KHACHHANGs.Add(kh);
        //                db.SaveChanges(); // để có ID_khach
        //                customerId = kh.ID_Khach.ToString();
        //            }
        //            else
        //            {
        //                customerId = existingCustomer.ID_Khach.ToString();
        //            }
        //        }
        //        // Tạo mới Vehicle
        //        Vehicle vehicle = new Vehicle
        //        {
        //            LicensePlate = v.LicensePlate,
        //            VehicleType = v.VehicleType,
        //            ID_Khach = Convert.ToInt32(customerId),
        //            ID_Ve = v.ID_Ve // nếu bạn có loại vé (ngày, tháng) gán ở đây
        //        };

        //        db.Vehicles.Add(vehicle);
        //        db.SaveChanges(); // để có VehicleID

        //        // Cập nhật chỗ đỗ
        //        parkingSpace.VehicleID = vehicle.VehicleID;
        //        parkingSpace.Status = "Có xe";
        //        parkingSpace.ParkingLotID = slot.ParkingLotID;

        //        db.SaveChanges();
        //        return true;
        //    }
        //}

        public bool AddvehicleToParkingSpace(Vehicle v, KHACHHANG kh, ParkingSpace parking, ParkingLot slot)
        {
            using (var db = new DataAccessEntity())
            {
                var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parking.ParkingSpaceID);

                if (parkingSpace == null)
                {
                    MessageBox.Show($"Không tìm thấy bãi đỗ với ID: {parking.ParkingSpaceID}");
                    return false;
                }
                else if (slot.TenBaiXe == "Bãi Xe máy" && parkingSpace.ParkingSpaceID > 20)
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                }
                else if (slot.TenBaiXe == "Bãi Xe đạp" && (parkingSpace.ParkingSpaceID < 41 || parkingSpace.ParkingSpaceID > 60))
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                }
                else if (slot.TenBaiXe == "Bãi Oto" && (parkingSpace.ParkingSpaceID < 21 || parkingSpace.ParkingSpaceID > 40))
                {
                    MessageBox.Show("Vị trí bãi xe không hợp lệ");
                    return false;
                }
                if(parkingSpace.Status == "Có xe" || parkingSpace.VehicleID != null)
                {
                    if (parkingSpace.VehicleID != v.VehicleID) {
                        {
                            MessageBox.Show("Bãi đỗ này đang được sử dụng hoặc đã có xe.");
                            return false;
                        }
                    }
                    else if (parkingSpace.VehicleID == v.VehicleID)
                    {
                        if (parkingSpace.Status == "Có xe")
                        {
                            MessageBox.Show($"Xe đang ở trong bãi tại vị trí {parkingSpace.ParkingSpaceID}");
                            return false;
                        }
                    }
                }



                // Kiểm tra khách hàng
                string customerId = null;
                if (kh != null)
                {
                    var existingCustomer = db.KHACHHANGs.FirstOrDefault(c => c.Phone == kh.Phone);
                    if (existingCustomer == null)
                    {
                        try
                        {
                            db.KHACHHANGs.Add(kh);
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var validationErrors in ex.EntityValidationErrors)
                            {
                                foreach (var validationError in validationErrors.ValidationErrors)
                                {
                                    Console.WriteLine("Property: {0}, Error: {1}",
                                        validationError.PropertyName, validationError.ErrorMessage);

                                    // Hoặc dùng MessageBox để hiển thị trong WinForms
                                    MessageBox.Show($"Thuộc tính: {validationError.PropertyName}\nLỗi: {validationError.ErrorMessage}");
                                }
                            }
                        }

                        customerId = kh.ID_Khach.ToString();
                    }
                    else
                    {
                        customerId = existingCustomer.ID_Khach.ToString();
                    }
                }

                // Xác định loại vé
                bool isMonthly; // hoặc kiểm tra theo ID vé cụ thể
                if (v.ID_Ve == 2)
                {
                    isMonthly = true;
                } else
                {
                    isMonthly = false;
                }

                    var existingVehicle = db.Vehicles.FirstOrDefault(veh => veh.LicensePlate == v.LicensePlate);

                if (isMonthly)
                {
                    if (existingVehicle == null)
                    {
                        v.ID_Khach = Convert.ToInt32(customerId);
                        db.Vehicles.Add(v);
                        db.SaveChanges();
                        existingVehicle = v;
                    }

                    var existingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.VehicleID == existingVehicle.VehicleID);
                    if (existingSpace != null && existingSpace.ParkingSpaceID != parkingSpace.ParkingSpaceID)
                    {
                        MessageBox.Show("Xe đã đăng ký vé tháng và có chỗ đỗ cố định.");
                        return false;
                    }

                    parkingSpace.VehicleID = existingVehicle.VehicleID;
                    parkingSpace.Status = "Có xe";
                    parkingSpace.ParkingLotID = slot.ParkingLotID;
                    var log = new TRANSACTION_LOG
                    {
                        ParkingSpaceID = parkingSpace.ParkingSpaceID,
                        EntryTime = DateTime.Now,
                        VehicleID = v.VehicleID
                    };
                    db.TRANSACTION_LOG.Add(log);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    try
                    {
                        if(CheckExistingVehicle(v.LicensePlate))
                        {
                            MessageBox.Show("Xe đã có trong hệ thống.");
                            return false;
                        }
                        // Vé ngày: luôn tạo mới
                        Vehicle vehicle = new Vehicle
                        {
                            LicensePlate = v.LicensePlate,
                            VehicleType = v.VehicleType,
                            ID_Khach = Convert.ToInt32(customerId),
                            ID_Ve = v.ID_Ve
                        };

                        db.Vehicles.Add(vehicle);
                        db.SaveChanges();

                        parkingSpace.VehicleID = vehicle.VehicleID;
                        parkingSpace.Status = "Có xe";
                        parkingSpace.ParkingLotID = slot.ParkingLotID;
                        var log = new TRANSACTION_LOG
                        {
                            ParkingSpaceID = parkingSpace.ParkingSpaceID,
                            EntryTime = DateTime.Now,
                            VehicleID = vehicle.VehicleID
                        };
                        db.TRANSACTION_LOG.Add(log);

                        db.SaveChanges();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi thêm xe: {ex.Message}");
                        return false;
                    }
                }
            }
        }

        //public bool ExitVehicleFromParkingSpace(int parkingSpaceId)
        //{
        //    using (var db = new DataAccessEntity())
        //    {
        //        // Tìm chỗ đỗ xe theo ID
        //        var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parkingSpaceId);

        //        // Kiểm tra nếu chỗ đỗ không tồn tại hoặc không có xe
        //        if (parkingSpace == null || parkingSpace.VehicleID == null)
        //        {
        //            return false;
        //        }

        //        // Lưu log giao dịch (nếu cần)
        //        var log = new TRANSACTION_LOG
        //        {
        //            ParkingSpaceID = parkingSpace.ParkingSpaceID,
        //            ExitTime = DateTime.Now, // Thời gian xe rời đi
        //            //VehicleID = parkingSpace.VehicleID // Lưu thông tin xe
        //        };
        //        db.TRANSACTION_LOG.Add(log);

        //        // Cập nhật trạng thái chỗ đỗ
        //        parkingSpace.VehicleID = null; // Xóa xe khỏi chỗ đỗ
        //        parkingSpace.Status = "Trống"; // Đặt trạng thái chỗ đỗ là "Trống"

        //        // Lưu thay đổi vào cơ sở dữ liệu
        //        db.SaveChanges();
        //        return true;
        //    }
        //}
        public bool ExitVehicleFromParkingSpace(int parkingSpaceId)
        {
            Vehicle ve = GetVehicleByparkingplacesID(parkingSpaceId);
            if (ve.ID_Ve == 1) { 
            try { 
                    
                    var CheckIsPaid = db.TRANSACTION_LOG
                        .Where(t => t.ParkingSpaceID == parkingSpaceId && t.ExitTime == null)
                        .Select(t => new { t.IsPaid })
                        .FirstOrDefault();
                    if (CheckIsPaid.IsPaid == false)
                    {
                        MessageBox.Show("Xe chưa thanh toán. \nVui lòng thanh toán trước khi cho xe rời bãi");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi kiểm tra thanh toán: {ex.Message}");
                    return false;
                }
            }
            var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.ParkingSpaceID == parkingSpaceId);

                if (parkingSpace == null || parkingSpace.VehicleID == null)
                {
                    return false;
                }

                var vehicle = db.Vehicles.FirstOrDefault(v => v.VehicleID == parkingSpace.VehicleID);
                if (vehicle == null)
                {
                    return false;
                }

                bool isMonthly = vehicle.ID_Ve == 2;

                // Lưu log
                var existingLog = db.TRANSACTION_LOG.Where(TRANSACTION_LOG => TRANSACTION_LOG.ParkingSpaceID == parkingSpaceId && TRANSACTION_LOG.ExitTime == null).FirstOrDefault();
                if (existingLog != null)
                {
                    existingLog.ExitTime = DateTime.Now;
                }
                else
                {
                    var log = new TRANSACTION_LOG
                    {
                        ParkingSpaceID = parkingSpaceId,
                        VehicleID = vehicle.VehicleID,
                        ExitTime = DateTime.Now
                    };
                    db.TRANSACTION_LOG.Add(log);
                }

                if (!isMonthly)
                {
                    // Vé ngày: xóa xe khỏi chỗ đỗ
                    parkingSpace.VehicleID = null;
                    parkingSpace.Status = "Trống";
                    
                }
                else
                {
                    // Vé tháng: chỉ cập nhật trạng thái chỗ đỗ
                    parkingSpace.Status = "Trống";
                }

                db.SaveChanges();
                return true;
        }
        public bool CheckExistingVehicle(string LicensePlate)
        {
            var CheckexistingVehicle = db.Vehicles.FirstOrDefault(veh => veh.LicensePlate == LicensePlate);
            if (CheckexistingVehicle != null)
            {
                MessageBox.Show("Xe đã có trong hệ thống.");
                return true;
            }
            return false;
        }
        //public int GetVehicleID(string LicensePlate)
        //{
        //    var vehicle = db.Vehicles.FirstOrDefault(v => v.LicensePlate == LicensePlate);
        //    if (vehicle != null)
        //    {
        //        return vehicle.VehicleID;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không tìm thấy xe với biển số này.");
        //        return -1; // Hoặc một giá trị không hợp lệ khác
        //    }
        //}
        public Vehicle GetVehicleByID(int vehicleID)
        {
            try
            {
                var vehicle = db.Vehicles.FirstOrDefault(v => v.VehicleID == vehicleID);
                if (vehicle != null)
                {
                    return vehicle;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin xe: {e.Message}");
                return null;
            }
        }
        public Vehicle GetVehicleByparkingplacesID(int parkingplacesID)
        {
            var vehicle = db.ParkingSpaces.FirstOrDefault(v => v.ParkingSpaceID == parkingplacesID);
            if (vehicle != null)
            {
                return vehicle.Vehicle;
            }
            else
            {
                MessageBox.Show("Không tìm thấy xe với bãi đậu này.");
                return null; // Hoặc một giá trị không hợp lệ khác
            }
        }
        public Vehicle GetVehicleInfo(string LicensePlate)
        {
            var vehicle = db.Vehicles.FirstOrDefault(v => v.LicensePlate == LicensePlate);
            if (vehicle != null)
            {
                return vehicle;
            }
            else
            {
                MessageBox.Show("Không tìm thấy xe với biển số này.");
                return null; // Hoặc một giá trị không hợp lệ khác
            }
        }
        public KHACHHANG GetCustomerInfo(Vehicle vehicle)
        {
            var customer = db.KHACHHANGs.FirstOrDefault(c => c.ID_Khach == vehicle.ID_Khach);
            if (customer != null)
            {
                return customer;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin khách hàng.");
                return null; // Hoặc một giá trị không hợp lệ khác
            }
        }
        public ParkingLot GetParkingLotInfo(Vehicle vehicle)
        {
            var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.VehicleID == vehicle.VehicleID);
            if (parkingSpace != null)
            {
                var parkingLot = db.ParkingLots.FirstOrDefault(pl => pl.ParkingLotID == parkingSpace.ParkingLotID);
                return parkingLot;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin bãi đỗ.");
                return null; // Hoặc một giá trị không hợp lệ khác
            }
        }
        public ParkingSpace GetParkingSpaceInfo(Vehicle vehicle)
        {
            var parkingSpace = db.ParkingSpaces.FirstOrDefault(ps => ps.VehicleID == vehicle.VehicleID);
            if (parkingSpace != null)
            {
                return parkingSpace;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin chỗ đỗ.");
                return null; // Hoặc một giá trị không hợp lệ khác
            }
        }
        public Payment payment(Vehicle vehicle, string paymentMethod)

        {
            var amount = 0;
            var transaction = db.TRANSACTION_LOG.FirstOrDefault(t => t.VehicleID == vehicle.VehicleID);
            if (transaction.IsPaid == false)
            {
                if (vehicle.ID_Ve == 2)
                {
                    var kh = db.KHACHHANGs.FirstOrDefault(c => c.ID_Khach == vehicle.ID_Khach);
                    if (kh.IsLoyalty == true)
                    {
                     amount = 500000 * Convert.ToInt32(kh.DiscountPercentage); // Giảm giá cho khách hàng thân thiết
                    }
                } else
                {
                    amount = CalculateFeeParking(transaction.ParkingSpaceID);
                }
                    var payment = new Payment
                    {
                        TransactionID = transaction.TransactionID,
                        Amount = amount,
                        PaymentMethod = paymentMethod,
                        PaymentDate = DateTime.Now
                    };
                transaction.IsPaid = true;
                db.Payments.Add(payment);
                db.SaveChanges();
                return payment;
            }
            else if (transaction.IsPaid == true)
            {
                MessageBox.Show("Xe đã thanh toán.");
                return null;
            }
            else
            {
                MessageBox.Show("Không tìm thấy giao dịch cho xe này.");
                return null;
            }
        }
        public int CalculateFeeParking(int parkingSpaceId)
        {
            var transaction = db.TRANSACTION_LOG.FirstOrDefault(t => t.ParkingSpaceID == parkingSpaceId && t.ExitTime == null);
            if (transaction != null)
            {
                DateTime entryTime = transaction.EntryTime ?? DateTime.Now;
                TimeSpan duration = DateTime.Now - entryTime;
                int days = (int)Math.Ceiling(duration.TotalDays);
                int fee = days * 20000;
                return fee;
            }
            else
            {
                MessageBox.Show("Không tìm thấy giao dịch cho bãi đỗ này.");
                return 0;
            }
        }



    }
}
