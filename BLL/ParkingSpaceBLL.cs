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
    }
}
