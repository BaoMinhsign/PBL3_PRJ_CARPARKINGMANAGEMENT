using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ParkingSpaceBLL
    {
        private ParkingSpaceDAL dal = new ParkingSpaceDAL();

        public List<ParkingSpaceDetail> GetParkingSpaceInfos(string parkingLotName)
        {
            return dal.GetParkingSpacesWithDetails(parkingLotName);
        }
        public bool AddVehicleToParkingSpace(Vehicle v, KHACHHANG kh, ParkingSpace parking, ParkingLot slot)
        {
            return dal.AddvehicleToParkingSpace(v, kh, parking, slot);
        }
        public bool ExitVehicleFromParkingSpace(int parkingspaceID)
        {
            return dal.ExitVehicleFromParkingSpace(parkingspaceID);
        }
        public bool CheckExistingVehicle(string LicensePlate)
        {
            return dal.CheckExistingVehicle(LicensePlate);
        }
        //public int GetVehilceID(string LicensePlate)
        //{
        //    return dal.GetVehicleID(LicensePlate);
        //}
        public Vehicle GetVehicleByID(int vehicleID)
        {
            return dal.GetVehicleByID(vehicleID);
        }
        public Vehicle GetVehicleInfo(string LicensePlate)
        {
            return dal.GetVehicleInfo(LicensePlate);
        }
        public KHACHHANG GetCustomerInfo(Vehicle vehicle)
        {
            return dal.GetCustomerInfo(vehicle);
        }
        public ParkingSpace GetParkingSpaceInfo(Vehicle vehicle)
        {
            return dal.GetParkingSpaceInfo(vehicle);
        }
        public ParkingLot GetParkingLotInfo(Vehicle vehicle)
        {
            return dal.GetParkingLotInfo(vehicle);
        }   
        public Payment payment(Vehicle vehicle, string paymentmethod)
        {
            return dal.payment(vehicle, paymentmethod);
        }
    }
}
